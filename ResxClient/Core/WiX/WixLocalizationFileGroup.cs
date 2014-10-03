using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Reflection;

namespace ResourceManager.Core
{
    public class WixLocalizationFileGroup : ResourceFileGroupBase
    {
        public WixLocalizationFileGroup(VSFileContainer container, string prefix, string path) 
            : base(container, prefix, path)
        {
        }

        public override IResourceFile CreateNewFile(CultureInfo culture)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ResourceManager.Core.WiX.WixLocalizationTemplate.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(stream);
            xml.DocumentElement.Attributes["Culture"].Value = culture.Name;

            string filepath = Path.Combine(this.FileGroupPath, this.Prefix + "." + culture.Name + ".wxl");
            xml.Save(filepath);

            WixLocalizationFile vsfile = new WixLocalizationFile(this.Container, new FileInfo(filepath));
            this.Add(vsfile);

            return vsfile;
        }
        public override ResourceDataGroupBase CreateDataGroup(string name)
        {
            return new WixLocalizationDataGroup(name, this);
        }
    }
}
