using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Reflection;

namespace ResourcenManager.Core
{
    public class VSResxFileGroup
    {
        private Dictionary<CultureInfo, VSResxFile> files = new Dictionary<CultureInfo,VSResxFile>();
        private Dictionary<string, VSResxDataGroup> data = new Dictionary<string, VSResxDataGroup>();
        private string prefix;
        private string path;
        private VSFileContainer container;

        public VSResxFileGroup(VSFileContainer container, string prefix, string path)
        {
            this.prefix = prefix;
            this.container = container;
            this.path = path;
        }
        public void Add(VSResxFile file)
        {
            if (file.Culture != null)
            {
                files.Add(file.Culture, file);
            }
            else
            {
                container.Project.UnassignedFiles.Add(file);
            }

            file.setFileGroup(this);

            if (file.Culture != null)
            {
                foreach (VSResxData data in file.Data.Values)
                {
                    data.Reference();
                }
            }
        }

        public Dictionary<CultureInfo, VSResxFile> ResxFiles
        {
            get { return files; }
        }
        public Dictionary<string, VSResxDataGroup> AllData
        {
            get { return data; }
        }
        public string Prefix
        {
            get { return prefix; }
        }
        public string ID
        {
            get { return this.Container.ID + "_" + prefix; }
        }
        public VSFileContainer Container
        {
            get { return container; }
        }
        
        public void RegisterResxData(VSResxData data)
        {
            if (!this.data.ContainsKey(data.Name))
                this.data.Add(data.Name, new VSResxDataGroup(data.Name));

            this.data[data.Name].Add(data);
        }

        public VSResxFile CreateNewFile(CultureInfo culture)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ResxClient.Templates.EmptyResxFile.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(stream);

            string filepath = Path.Combine(path, prefix + "." + culture.Name + ".resx");
            xml.Save(filepath);

            VSResxFile vsfile = new VSResxFile(this.Container, new FileInfo(filepath));
            this.Add(vsfile);

            return vsfile;
        }
    }
}
