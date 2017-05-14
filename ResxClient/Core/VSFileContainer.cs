using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ResourceManager.Core
{
    public class VSFileContainer
    {
        private DirectoryInfo directory;

        public VSFileContainer(VSProject project, VSFileContainer parent, string filepath)
        {
            Project = project;
            Parent = parent;
        }
        protected virtual void Init(string filepath)
        {
            directory = new DirectoryInfo(Path.Combine(Project.Solution.SolutionDirectory.FullName, filepath));

            IResourceFile resxfile = null;
            FileInfo[] files = directory.GetFiles("*.resx", SearchOption.TopDirectoryOnly);
            Files = new List<IResourceFile>();
            foreach (FileInfo file in files)
            {
                if (Project.SkipFile(file.Name))
                    continue;

                resxfile = new VSResxFile(this, file);

                string fileGroupId = this.ID + "_" + resxfile.Prefix;

                if (!Project.ResxGroups.ContainsKey(fileGroupId))
                    Project.ResxGroups.Add(fileGroupId, new VSResxFileGroup(this, resxfile.Prefix, file.DirectoryName));

                Project.ResxGroups[fileGroupId].Add(resxfile);
                Files.Add(resxfile);
            }

            // Localization file for Windows Installer XML toolkit (WiX)
            files = directory.GetFiles("*.wxl", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                if (Project.SkipFile(file.Name))
                    continue;

                resxfile = new WixLocalizationFile(this, file);

                string fileGroupId = this.ID + "_" + resxfile.Prefix;
                if (!Project.ResxGroups.ContainsKey(fileGroupId))
                    Project.ResxGroups.Add(fileGroupId, new VSResxFileGroup(this, resxfile.Prefix, file.DirectoryName));

                Project.ResxGroups[fileGroupId].Add(resxfile);
                Files.Add(resxfile);
            }

            Directories = new List<VSProjectFolder>();
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                if (dir.Name == "obj" || dir.Name == "bin")
                    continue;
                if (Project.SkipDirectory(dir.Name))
                    continue;

                var folder = new VSProjectFolder(Project, this, dir.FullName);

                if (folder.ContainsFiles())
                    Directories.Add(folder);
            }
        }
        protected string CleanDirectoryPath(string filepath)
        {
            return filepath.Replace(Path.GetFileName(filepath), "");
        }

        public DirectoryInfo Directory
        {
            get { return directory; }
        }
        public List<IResourceFile> Files
        {
            get;
            private set;
        }

        public string ID
        {
            get
            {
                if (Parent != null)
                    return Parent.ID + "_" + directory.Name;
                else
                    return directory.Name;
            }
        }

        private VSProject project = null;
        public VSProject Project
        {
            get
            {
                if (project == null && this.GetType() == typeof(VSProject))
                    return this as VSProject;
                else
                    return project;
            }
            private set
            {
                project = value;
            }
        }

        public VSFileContainer Parent
        {
            get;
            private set;
        }
        public List<VSProjectFolder> Directories
        {
            get;
            private set;
        }
        
        //public List<VSResxFile> GetResourceFiles()
        //{
        //    List<VSResxFile> resxFiles = new List<VSResxFile>();

        //    FileInfo[] files = directory.GetFiles("*.resx", SearchOption.TopDirectoryOnly);
        //    foreach (FileInfo file in files)
        //    {
        //        resxFiles.Add(new VSResxFile(this, file));
        //    }

        //    return resxFiles;
        //}
    }
}
