using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ResourcenManager.Core
{
    public class VSFileContainer
    {
        private DirectoryInfo directory;

        public VSFileContainer(VSProject project, VSFileContainer parent, string filepath)
        {
            this.Project = project;
            this.Parent = parent;
        }
        protected virtual void Init(string filepath)
        {
            directory = new DirectoryInfo(Path.Combine(Project.Solution.SolutionDirectory.FullName, filepath));

            VSResxFile resxfile = null;
            FileInfo[] files = directory.GetFiles("*.resx", SearchOption.TopDirectoryOnly);
            Files = new List<VSResxFile>();
            foreach (FileInfo file in files)
            {
                resxfile = new VSResxFile(this, file);

                if (!Project.ResxGroups.ContainsKey(resxfile.ID))
                    Project.ResxGroups.Add(resxfile.ID, new VSResxFileGroup(this, resxfile.Prefix, file.DirectoryName));

                Project.ResxGroups[resxfile.ID].Add(resxfile);
                Files.Add(resxfile);
            }

            Directories = new List<VSProjectFolder>();
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                if (dir.Name == "obj" || dir.Name == "bin")
                    continue;

                VSProjectFolder folder = new VSProjectFolder(Project, this, dir.FullName);

                if (folder.ContainsFiles())
                    Directories.Add(folder);
            }
        }
        protected string CleanDirectoryPath(string filepath)
        {
            string subdir = filepath.Replace(Path.GetFileName(filepath), "");
            if (subdir == "")
                subdir = filepath;
            return subdir;
        }

        public DirectoryInfo Directory
        {
            get { return directory; }
        }
        public List<VSResxFile> Files
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
