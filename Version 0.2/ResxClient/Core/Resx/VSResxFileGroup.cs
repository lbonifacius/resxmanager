using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Reflection;

namespace ResourceManager.Core
{
    public class VSResxFileGroup : ResourceFileGroupBase
    {
        public VSResxFileGroup(VSFileContainer container, string prefix, string path) 
            : base(container, prefix, path)
        {
        }

        public override IResourceFile CreateNewFile(CultureInfo culture)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ResourceManager.Templates.EmptyResxFile.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(stream);

            string filepath = Path.Combine(this.FileGroupPath, this.Prefix + "." + culture.Name + ".resx");
            xml.Save(filepath);

            VSResxFile vsfile = new VSResxFile(this.Container, new FileInfo(filepath));
            this.Add(vsfile);

            return vsfile;
        }
        public override ResourceDataGroupBase CreateDataGroup(string name)
        {
            return new VSResxDataGroup(name);
        }
    }
}
