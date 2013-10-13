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
                XPathNodeIterator nodes = nav.Select("/root/data[count(@type) = 0 and count(@mimetype) = 0]");
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
        public override void CreateResourceData(string name, string value)
        {
            VSResxData resxData = new VSResxData(this, name);
            resxData.Value = value;
            Data.Add(name, resxData);
            this.FileGroup.AllData[name].Add(resxData);
        }
        public override void CreateResourceDataComment(string name, string comment)
        {
            VSResxData resxData = new VSResxData(this, name);
            resxData.Comment = comment;
            Data.Add(name, resxData);
            this.FileGroup.AllData[name].Add(resxData);
        }

        public override void Save()
        {
            if (HasChanged)
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

                if (isReadOnly)
                    SetReadOnlyAttribute(File, true);

                SetSaved();
            }
        }

        private void SetResourceData(XmlDocument xml, ResourceDataBase data)
        {
            XmlElement dataNode = xml.DocumentElement.SelectSingleNode("data[@name = '" + data.Name + "']") as XmlElement;
            if (dataNode == null)
            {
                dataNode = xml.CreateElement("data");
                dataNode.SetAttribute("name", data.Name);

                xml.DocumentElement.AppendChild(dataNode);
            }

            XmlElement valNode = dataNode.SelectSingleNode("value") as XmlElement;
            if(valNode == null)
            {
                valNode = xml.CreateElement("value");
                valNode.InnerText = data.Value;
                dataNode.AppendChild(valNode);
            }
            else
                valNode.InnerText = data.Value;

            XmlElement commentNode = dataNode.SelectSingleNode("comment") as XmlElement;
            if (commentNode == null)
            {
                if (!String.IsNullOrEmpty(data.Comment))
                {
                    commentNode = xml.CreateElement("comment");
                    commentNode.InnerText = data.Comment;
                    dataNode.AppendChild(commentNode);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(data.Comment))
                {
                    commentNode.InnerText = data.Comment;
                }
                else
                {
                    dataNode.RemoveChild(commentNode);
                }
            }
        }
    }
}
