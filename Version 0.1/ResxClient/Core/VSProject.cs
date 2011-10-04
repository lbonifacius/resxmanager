using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace ResourcenManager.Core
{
    public class VSProject : VSFileContainer
    {
        private Dictionary<string, IResourceFileGroup> cultures = new Dictionary<string, IResourceFileGroup>();
        private ResxClientProjectFile resxProjectFile;
        private List<IResourceFile> unassignedFiles = new List<IResourceFile>();

        
        public VSProject(VSSolution solution, string filepath, string name) : base(null, null, filepath)
        {
            this.Solution = solution;

            string configPath = Path.Combine(Path.Combine(Solution.SolutionDirectory.FullName, CleanDirectoryPath(filepath)), Path.GetFileNameWithoutExtension(filepath) + ResxClientProjectFile.RESXCLIENTPROJECTFILE_EXTENSION);
            resxProjectFile = new ResxClientProjectFile(configPath);

            this.name = name;

            base.Init(CleanDirectoryPath(filepath));
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        public Dictionary<string, IResourceFileGroup> ResxGroups
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
        public List<IResourceFile> UnassignedFiles
        {
            get { return unassignedFiles; }
        }
        public IList<ResourceDataGroupBase> GetUncompleteDataGroups()
        {
            List<ResourceDataGroupBase> list = new List<ResourceDataGroupBase>();
            foreach (IResourceFileGroup fileGroup in cultures.Values)
            {
                list.AddRange(from resxDataGroup in fileGroup.AllData.Values where !resxDataGroup.IsComplete(Solution.Cultures.Keys) select resxDataGroup);
            }
            return list;
        }
    }
}
