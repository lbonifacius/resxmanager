using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml;

namespace ResourceManager.Core
{
    public class VSProject : VSFileContainer
    {
        private Dictionary<string, IResourceFileGroup> cultures = new Dictionary<string, IResourceFileGroup>();
        private List<IResourceFile> unassignedFiles = new List<IResourceFile>();

        
        
        public VSProject(VSSolution solution, string filepath, string name) : base(null, null, filepath)
        {
            this.Solution = solution;

            string configPath = Path.Combine(Path.Combine(Solution.SolutionDirectory.FullName, CleanDirectoryPath(filepath)), Path.GetFileNameWithoutExtension(filepath) + ResxClientProjectFile.RESXCLIENTPROJECTFILE_EXTENSION);
            this.ResxProjectFile = new ResxClientProjectFile(configPath);

            this.FilePath = filepath;
            this.Name = name;
            this.Type = resolveProjectType(filepath);

            base.Init(CleanDirectoryPath(filepath));
        }
        protected static VSProjectTypes resolveProjectType(string filepath)
        {
            switch (Path.GetExtension(filepath).ToLower())
            { 
                case ".csproj":
                    return VSProjectTypes.CSharp;
                case ".vbproj":
                    return VSProjectTypes.VB;
                case ".fsproj":
                    return VSProjectTypes.FSharp;
            }

            return VSProjectTypes.Other;
        }

        public string FilePath
        {
            get;
            private set;
        }
        public string Name
        {
            get;
            private set;
        }
        public VSProjectTypes Type
        {
            get;
            private set;
        }
        public bool HasChanged
        {
            get
            {
                return cultures.Any(p => p.Value.HasChanged);
            }
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
            get;
            private set;
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

        private const string defaultnamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        private XmlDocument projectXml  = null;
        private XmlNamespaceManager namespaceManager = null;
        internal void AddResourceFileToProjectFile(IResourceFile file, VSProjectFileTypes filetype)
        {
            try
            {
                if (file.FileGroup.Files.Count > 0)
                {
                    if (projectXml == null)
                    {
                        projectXml = new XmlDocument();
                        projectXml.Load(Path.Combine(this.Directory.FullName, this.FilePath));

                        namespaceManager = new XmlNamespaceManager(projectXml.NameTable);
                        namespaceManager.AddNamespace("n", defaultnamespace);
                    }

                    string filename = getProjectRelativeFileName(file);
                    var node = projectXml.SelectSingleNode(String.Format("/n:Project/n:ItemGroup/n:{1}[@Include = '{0}']", filename, filetype), namespaceManager);
                    if (node == null)
                    {
                        foreach (var otherfile in file.FileGroup.Files.Values)
                        {
                            filename = getProjectRelativeFileName(otherfile);
                            node = projectXml.SelectSingleNode(String.Format("/n:Project/n:ItemGroup[n:{1}/@Include = '{0}']", filename, filetype), namespaceManager);

                            if (node != null)
                                break;
                        }

                        if (node == null)
                        {
                            log4net.ILog log = log4net.LogManager.GetLogger(typeof(VSProject));
                            log.WarnFormat("Resource file '{0}' could not be added to project '{1}', because no existing file found in project.", file.File.Name, file.FileGroup.Container.Project.Name);
                        }
                        else
                        {
                            var embeddedResource = projectXml.CreateElement(filetype.ToString(), defaultnamespace);
                            embeddedResource.SetAttribute("Include", getProjectRelativeFileName(file));
                            node.AppendChild(embeddedResource);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(typeof(VSProject));
                log.Error("Resource file could not be added to the project.", e);
            }
        }
        private string getProjectRelativeFileName(IResourceFile file)
        {
            var s1 = Path.Combine(file.FileGroup.Container.Project.Solution.SolutionDirectory.FullName, file.FileGroup.Container.Project.Directory.FullName);
            return file.File.FullName.Substring(s1.Length);
        }
        internal void SaveProjectFile()
        {
            if (projectXml != null)
            {
                var file = new FileInfo(Path.Combine(this.Directory.FullName, this.FilePath));
                bool isReadOnly = file.IsReadOnly;

                if (isReadOnly)
                    ResourceFileBase.SetReadOnlyAttribute(file, false);

                projectXml.Save(file.FullName);

                if (isReadOnly)
                    ResourceFileBase.SetReadOnlyAttribute(file, true);

                projectXml = null;
                namespaceManager = null;
            }
        }
    }
}
