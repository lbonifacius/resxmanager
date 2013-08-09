using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;

namespace ResourceManager.Core
{
    public class VSResxFile : ResourceFileBase, IProjectFileSettings
    {
        public VSResxFile(VSFileContainer folder, FileInfo file) 
            : base(folder, file)
        {
            using (XmlReader reader = new XmlTextReader(file.FullName))
            {
                XPathDocument xml = new XPathDocument(reader);
                XPathNavigator nav = xml.CreateNavigator();
                XPathNodeIterator nodes = nav.Select("/root/data");
                while (nodes.MoveNext())
                {
                    VSResxData d = new VSResxData(this, nodes.Current);

                    if (!Data.ContainsKey(d.Name))
                        Data.Add(d.Name, d);
                }
            }

            string[] parts = file.Name.Split('.');
            if (parts.Length >= 2)
            {
                int posCultureInfo = parts.Length - 2;
                if (parts[posCultureInfo] == "asax" ||
                    parts[posCultureInfo] == "aspx" ||
                    parts[posCultureInfo] == "ascx")
                {
                    posCultureInfo--;
                }

                try
                {
                    Culture = CultureInfo.GetCultureInfo(parts[posCultureInfo]);
                    IsCultureAutoDetected = true;
                    Prefix = buildPrefix(parts, posCultureInfo);

                    if (Prefix.LastIndexOf('.') == Prefix.Length - 1)
                        Prefix = Prefix.Substring(0, Prefix.Length - 1);
                }
                catch 
                {
                    Culture = CultureInfo.InvariantCulture;
                }                
            }

            if (Prefix == null || Prefix == "")
            {
                Prefix = Path.GetFileNameWithoutExtension(file.Name);
            }

            folder.Project.ResxProjectFile.LoadFile(this);
        }
        private string buildPrefix(string[] parts, int posCulutureInfo)
        {
            string s = parts[0];
            for (int i = 1; i < posCulutureInfo; i++)
            { 
                s += "." + parts[i];
            }
            return s;
        }
        public override void CreateResourceData(string name, string value, string comment)
        {
            VSResxData resxData = new VSResxData(this, name, value, comment);
            Data.Add(name, resxData);
            this.FileGroup.AllData[name].Add(resxData);
        }

        public override void Save()
        {
            bool isReadOnly = File.IsReadOnly;

            if (isReadOnly)
                SetReadOnlyAttribute(File, false);

            XmlDocument xml = new XmlDocument();
            xml.Load(File.FullName);

            foreach (ResourceDataBase data in Data.Values)
            {
                SetResourceData(xml, data);
            }

            xml.Save(File.FullName);

            if(isReadOnly)
                SetReadOnlyAttribute(File, true);
        }

        private void SetResourceData(XmlDocument xml, ResourceDataBase data)
        {
            XmlElement valNode = xml.DocumentElement.SelectSingleNode("data[@name = '" + data.Name + "']/value") as XmlElement;
            if (valNode != null)
                valNode.InnerText = data.Value;
            else
            {
                XmlElement element = xml.CreateElement("data");
                element.SetAttribute("name", data.Name);
                XmlElement dataElement = xml.CreateElement("value");
                dataElement.InnerText = data.Value;
                element.AppendChild(dataElement);

                xml.DocumentElement.AppendChild(element);
            }
        }
    }
}
