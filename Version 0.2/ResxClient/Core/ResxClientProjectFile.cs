using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace ResourceManager.Core
{
    public class ResxClientProjectFile
    {
        public const string RESXCLIENTPROJECTFILE_EXTENSION = ".resxm";

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
                foreach (VSResxFile file in group.Files.Values)
                {
                    LoadFile(file);
                }
            }
        }
        public void LoadFile(IResourceFile file)
        {
            XPathNodeIterator nodes = nav.Select("/resxClient/project/resourceFile[@FileName = '" + file.ID + "']");
            if (nodes.MoveNext())
            {
                LoadFile(file, nodes.Current);
            }
        }
        public void LoadFile(IResourceFile file, XPathNavigator nav)
        {
            file.LoadSettings(nav);
        }

        public void SaveFile(IResourceFile file)
        {
            XmlElement element = (XmlElement)xml.SelectSingleNode("/resxClient/project/resourceFile[@FileName = '" + file.ID + "']");
            if (element == null)
            {
                element = xml.CreateElement("resourceFile");
                element.SetAttribute("FileName", file.ID);
                xml.DocumentElement.FirstChild.AppendChild(element);
            }

            file.SaveSettings(element);
        }
    }
}