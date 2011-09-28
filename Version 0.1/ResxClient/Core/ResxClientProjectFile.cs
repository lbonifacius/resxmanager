using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace ResourcenManager.Core
{
    public class ResxClientProjectFile
    {
        private XmlDocument xml;
        private XPathNavigator nav;
        private string url;

        public ResxClientProjectFile(string url)
        {
            this.url = url;
            xml = new XmlDocument();

            if(File.Exists(url))
                xml.Load(url);
            nav = xml.CreateNavigator();

            if (xml.DocumentElement == null)
                xml.AppendChild(xml.CreateElement("resxClient"));

            if(xml.DocumentElement.FirstChild == null)
                xml.DocumentElement.AppendChild(xml.CreateElement("project"));
        }
        public void Save()
        {
            xml.Save(url);
        }

        public void LoadProject(VSProject project)
        {
            foreach (VSResxFileGroup group in project.ResxGroups.Values)
            {
                foreach (VSResxFile file in group.ResxFiles.Values)
                {
                    LoadFile(file);
                }
            }
        }
        public void LoadFile(VSResxFile file)
        {
            XPathNodeIterator nodes = nav.Select("/resxClient/project/resxFile[@FileName = '" + file.File.FullName + "']");
            if (nodes.MoveNext())
            {
                LoadFile(file, nodes.Current);
            }
        }
        public void LoadFile(VSResxFile file, XPathNavigator nav)
        {
            file.LoadSettings(nav);
        }

        public void SaveFile(VSResxFile file)
        {
            XmlElement element = (XmlElement)xml.SelectSingleNode("/resxClient/project/resxFile[@FileName = '" + file.File.FullName + "']");
            if (element == null)
            {
                element = xml.CreateElement("resxFile");
                element.SetAttribute("FileName", file.File.FullName);
                xml.DocumentElement.FirstChild.AppendChild(element);
            }

            file.SaveSettings(element);
        }
    }
}