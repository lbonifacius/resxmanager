using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Linq;

namespace ResourceManager.Core
{
    public abstract class ResourceFileGroupBase : ResourceManager.Core.IResourceFileGroup
    {
        private Dictionary<CultureInfo, IResourceFile> files = new Dictionary<CultureInfo, IResourceFile>();
        private Dictionary<string, ResourceDataGroupBase> data = new Dictionary<string, ResourceDataGroupBase>();
        private string prefix;
        private string path;
        private VSFileContainer container;

        public ResourceFileGroupBase(VSFileContainer container, string prefix, string path)
        {
            this.prefix = prefix;
            this.container = container;
            this.path = path;
        }
        public void Add(IResourceFile file)
        {
            if (file.Culture != null)
            {
                files.Add(file.Culture, file);
            }
            else
            {
                container.Project.UnassignedFiles.Add(file);
            }

            file.SetFileGroup(this);

            if (file.Culture != null)
            {
                foreach (ResourceDataBase data in file.Data.Values)
                {
                    data.Reference();
                }
            }
        }
        public void SetResourceData(string key, string value, string comment, CultureInfo culture)
        {
            if (!Files.ContainsKey(culture))
            {
                IResourceFile file = CreateNewFile(culture);

                file.CreateResourceData(key, value, comment);
            }
            else
            {
                Files[culture].SetResourceData(key, value, comment);
            }
        }

        public Dictionary<CultureInfo, IResourceFile> Files
        {
            get { return files; }
        }
        public Dictionary<string, ResourceDataGroupBase> AllData
        {
            get { return data; }
        }
        public string Prefix
        {
            get { return prefix; }
        }
        public string FileGroupPath
        {
            get { return path; }
        }
        public string ID
        {
            get { return this.Container.ID + "_" + prefix; }
        }
        public VSFileContainer Container
        {
            get { return container; }
        }
        
        public void RegisterResourceData(ResourceDataBase data)
        {
            if (!this.data.ContainsKey(data.Name))
                this.data.Add(data.Name, CreateDataGroup(data.Name));

            this.data[data.Name].Add(data);
        }

        public abstract IResourceFile CreateNewFile(CultureInfo culture);

        public abstract ResourceDataGroupBase CreateDataGroup(string name);

        public void ChangeCulture(IResourceFile file, CultureInfo culture)
        {
            if (this.Files.ContainsKey(file.Culture))
                this.Files.Remove(file.Culture);

            this.Files.Add(culture, file);
        }
    }
}
