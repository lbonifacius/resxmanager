using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using ResourceManager.Exceptions;
using ResourceManager.Core.Configuration;

namespace ResourceManager.Core
{
    public class VSSolution
    {
        // We need this to surround the filepaths with quotes should they contain spaces
        const char quote = '"';

        private SortedDictionary<string, VSProject> projects = new SortedDictionary<string, VSProject>();
        private DirectoryInfo solutionDirectory;
        private Dictionary<CultureInfo, VSCulture> cultures = new Dictionary<CultureInfo, VSCulture>();

        public SortedDictionary<string, VSProject> Projects
        {
            get { return projects; }
            set { projects = value; }
        }

        public VSSolution(string filepath)
        {
            solutionDirectory = new DirectoryInfo(filepath.Replace(Path.GetFileName(filepath), ""));
            Name = Path.GetFileNameWithoutExtension(filepath);

            string configPath = Path.Combine(SolutionDirectory.FullName, Path.GetFileName(filepath) + ResxClientConfigurationBase.RESXCLIENTPROJECTFILE_EXTENSION);
            Configuration = new SolutionConfiguration(configPath);

            VSSolutionFileParser parser = new VSSolutionFileParser(filepath, this);
        }
        public string Name
        {
            get;
            private set;
        }
        public DirectoryInfo SolutionDirectory
        {
            get { return solutionDirectory; }
        }
        public Dictionary<CultureInfo, VSCulture> Cultures
        {
            get { return cultures; }
        }
        public bool HasChanged
        {
            get { return projects.Any(p => p.Value.HasChanged); }
        }
        public SolutionConfiguration Configuration
        {
            get;
            private set;
        }

        public bool SkipProject(string name)
        {
            foreach (var m in Configuration.SkipProjects)
            {
                if (m.IsMatch(name))
                    return true;
            }
            return false;
        }
        public bool SkipFile(string name)
        {
            foreach (var m in Configuration.SkipFiles)
            {
                if (m.IsMatch(name))
                    return true;
            }
            return false;
        }
        public bool SkipDirectory(string name)
        {
            foreach (var m in Configuration.SkipDirectories)
            {
                if (m.IsMatch(name))
                    return true;
            }
            return false;
        }
        public bool SkipGroup(string name)
        {
            foreach (var m in Configuration.SkipGroups)
            {
                if (m.IsMatch(name))
                    return true;
            }
            return false;
        }

        public void AddCultureFile(ResourceFileBase file)
        {
            if (!cultures.ContainsKey(file.Culture))
                cultures.Add(file.Culture, new VSCulture(file.Culture));

            cultures[file.Culture].Files.Add(file);
        }

        internal void ChangeCulture(ResourceFileBase resourceFile, CultureInfo newCulture)
        {
            if (!cultures.ContainsKey(newCulture))
                cultures.Add(newCulture, new VSCulture(newCulture));

            if (cultures[resourceFile.Culture].Files.Contains(resourceFile))
                cultures[resourceFile.Culture].Files.Remove(resourceFile);

            cultures[newCulture].Files.Add(resourceFile);
        }

        public void RemoveUnusedCultures()
        {
            var rm = cultures.Where(c => c.Value.Files.Count == 0)
                .Select(c => c.Key).ToList();

            foreach (var r in rm)
                cultures.Remove(r);
        }

        public bool Save()
        {
            // Get a list of files that require TFS checkouts
            var filesToCheckout = getTFSFiles();
            var checkedout = DialogResult.None;

            // If we have files to checkout, perform checkout
            if (filesToCheckout != null && filesToCheckout.Count() > 0)
                checkedout = this.tryTFSCheckout(filesToCheckout.ToArray());

            if (checkedout == DialogResult.None || checkedout == DialogResult.OK || checkedout == DialogResult.Ignore)
            {
                // Loop through the project resource files and save the changes to disk
                foreach (VSProject project in Projects.Values)
                {
                    foreach (IResourceFileGroup fileGroup in project.ResxGroups.Values)
                    {
                        foreach (IResourceFile file in fileGroup.Files.Values)
                        {
                            if (file.HasChanged)
                                file.IncludeInProjectFile();

                            file.Save();
                        }
                    }

                    project.SaveProjectFile();
                }
            }

            // If we have files that we checked out, perform checkin
            if (checkedout == DialogResult.OK && filesToCheckout != null && filesToCheckout.Count() > 0)
            {
                // Ask the user to confirm the checkin action, he/she might not want this 
                if (MessageBox.Show(Properties.Resources.SaveResources_TryCheckin_Message,
                        Properties.Resources.SaveResources_TryCheckin_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.tryTFSCheckin(filesToCheckout.ToArray());
                }
            }

            return checkedout == DialogResult.None || checkedout == DialogResult.OK || checkedout == DialogResult.Ignore;
        }

        #region TFS integration

        /// <summary>
        /// Returns a list of project files that require TFS checkouts
        /// - These files are identified via the Readonly flag for changed files and if the project seems to be under source control.
        /// </summary>
        /// <returns></returns>
        private List<string> getTFSFiles()
        {
            if (!isTfAvailable())
                return null;

            // Flag that indicates if the program has asked the user to checkout
            bool hasAskedForTFSCheckout = false;

            // Flag that indicates if the user wants to checkout
            bool tryTFSCheckout = false;

            // List of files to return
            var filesToCheckout = new List<string>();
                        
            // Loop through the projects resource files
            foreach (VSProject project in Projects.Values.Where(p => p.UnderTfsControl == true))
            {
                foreach (IResourceFileGroup fileGroup in project.ResxGroups.Values)
                {
                    foreach (IResourceFile file in fileGroup.Files.Values)
                    {
                        // Check if the file have changes 
                        // Check if the file is readonly
                        if (file.HasChanged && file.File.IsReadOnly && !hasAskedForTFSCheckout)
                        {
                            // Prompt the user to confirm wanting to checkout and store the result 
                            tryTFSCheckout = MessageBox.Show(Properties.Resources.SaveResources_TryCheckout_Message,
                                Properties.Resources.SaveResources_TryCheckout_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                            // Update the flag for the confirm prompt
                            hasAskedForTFSCheckout = true;
                        }

                        // If the user wants to checkout, return the file
                        if (file.HasChanged && file.File.IsReadOnly && tryTFSCheckout)
                            filesToCheckout.Add(file.File.FullName);
                    }
                }
            }

            return filesToCheckout;
        }

        /// <summary>
        /// Checks out a list of file in a single call to TF.exe
        /// </summary>
        /// <param name="filePaths"></param>
        private DialogResult tryTFSCheckout(string[] filePaths)
        {
            // Arguments for the StartProcess
            var arguments = new List<string>(){
                "checkout"
            };

            // Add each file as an argument for TF.exe
            foreach (var item in filePaths)
                arguments.Add(quote + item + quote);

            // Call ProcessStart
            return RunProcessStart(getPathToTfExe(), arguments.ToArray(), true);
        }

        /// <summary>
        /// Checks in a list of file in a single call to TF.exe
        /// </summary>
        /// <param name="filePaths"></param>
        private void tryTFSCheckin(string[] filePaths)
        {
            // Arguments for the StartProcess
            var arguments = new List<string>(){
                "checkin"
            };

            // Add each file as an argument for TF.exe
            foreach (var item in filePaths)
                arguments.Add(quote + item + quote);

            // Add a comment 
            arguments.Add("/comment:" + quote + "ResXManager: " + this.Name + quote);

            // Call ProcessStart
            RunProcessStart(getPathToTfExe(), arguments.ToArray(), false);
        }
        private bool isTfAvailable()
        {
            return !String.IsNullOrEmpty(getPathToTfExe()) && File.Exists(getPathToTfExe());
        }
        private string getPathToTfExe()
        {
            return System.Configuration.ConfigurationManager.AppSettings["PathToTFExe"];
        }

        private DialogResult RunProcessStart(string fileName, string[] arguments, bool showIgnoreButton)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            // Create the startinfo
            var startInfo = new ProcessStartInfo()
            {
                FileName = fileName,
                Arguments = string.Join(" ", arguments),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            DialogResult dialog = DialogResult.Retry;
            while (dialog == DialogResult.Retry)
            {
                // Start the process
                using (var process = Process.Start(startInfo))
                {
                    // Wait for the process to finish in the background
                    process.WaitForExit();

                    // Check for errors and display
                    var errorOutput = process.StandardError.ReadToEnd();
                    if (!string.IsNullOrEmpty(errorOutput))
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
                        if (showIgnoreButton)
                            buttons = MessageBoxButtons.AbortRetryIgnore;

                        dialog = MessageBox.Show(errorOutput, Properties.Resources.Error, buttons, MessageBoxIcon.Error);

                        var log = log4net.LogManager.GetLogger(typeof(VSSolution));
                        if (log.IsErrorEnabled)
                            log.Error(new TfsException(errorOutput));
                    }
                    else
                        dialog = DialogResult.OK;
                }
            }

            return dialog;
        }

        #endregion
    }
}
