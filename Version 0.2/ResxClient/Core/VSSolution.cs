using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Linq;

namespace ResourceManager.Core
{
    public class VSSolution
    {
        private SortedDictionary<string, VSProject> projects = new SortedDictionary<string,VSProject>();
        private DirectoryInfo solutionDirectory;
        private Dictionary<CultureInfo, VSCulture> cultures = new Dictionary<CultureInfo,VSCulture>();

        public SortedDictionary<string, VSProject> Projects
        {
            get { return projects; }
            set { projects = value; }
        }	

        public VSSolution(string filepath)
        {
            solutionDirectory = new DirectoryInfo(filepath.Replace(Path.GetFileName(filepath), ""));
            this.Name = Path.GetFileNameWithoutExtension(filepath);

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

            foreach(var r in rm)
                cultures.Remove(r);
        }

        public void Save()
        {
            foreach (VSProject project in Projects.Values)
            {
                foreach (IResourceFileGroup fileGroup in project.ResxGroups.Values)
                {
                    foreach (IResourceFile file in fileGroup.Files.Values)
                    {
                        file.Save();
                    }
                }
            }
        }
    }
}
