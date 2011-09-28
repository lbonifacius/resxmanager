using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace ResourcenManager.Core
{
    public class VSProject : VSFileContainer
    {
        private Dictionary<string, VSResxFileGroup> cultures = new Dictionary<string, VSResxFileGroup>();
        private ResxClientProjectFile resxProjectFile;
        private List<VSResxFile> unassignedFiles = new List<VSResxFile>();

        
        public VSProject(VSSolution solution, string filepath, string name) : base(null, null, filepath)
        {
            this.Solution = solution;

            string configPath = Path.Combine(Path.Combine(Solution.SolutionDirectory.FullName, CleanDirectoryPath(filepath)), Path.GetFileNameWithoutExtension(filepath) + ".xml");
            resxProjectFile = new ResxClientProjectFile(configPath);

            this.name = name;

            base.Init(CleanDirectoryPath(filepath));
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        public Dictionary<string, VSResxFileGroup> ResxGroups
        {
            get { return cultures; }
        }
        public VSSolution Solution
        {
            get;
            private set;
        }
        public ResxClientProjectFile ResxProjectFile
        {
            get { return resxProjectFile; }
            set { resxProjectFile = value; }
        }
        public List<VSResxFile> UnassignedFiles
        {
            get { return unassignedFiles; }
        }
        public IList<VSResxDataGroup> GetUncompleteDataGroups()
        {
            List<VSResxDataGroup> list = new List<VSResxDataGroup>();
            foreach (VSResxFileGroup fileGroup in cultures.Values)
            {
                list.AddRange(from resxDataGroup in fileGroup.AllData.Values where !resxDataGroup.IsComplete(Solution.Cultures.Keys) select resxDataGroup);
            }
            return list;
        }
    }
}
