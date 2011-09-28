using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace ResourcenManager.Core
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

            VSSolutionFileParser parser = new VSSolutionFileParser(filepath, this);
        }
        public string Name
        {
            get { return "Solution";  }
        }
        public DirectoryInfo SolutionDirectory
        {
            get { return solutionDirectory; }
        }
        public Dictionary<CultureInfo, VSCulture> Cultures
        {
            get { return cultures; }
        }

        public void AddCultureFile(VSResxFile file)
        {
            if (!cultures.ContainsKey(file.Culture))
                cultures.Add(file.Culture, new VSCulture(file.Culture));

            cultures[file.Culture].Files.Add(file);
        }
    }
}
