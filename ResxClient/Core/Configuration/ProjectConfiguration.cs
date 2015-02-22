using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourceManager.Core.Configuration
{
    public class ProjectConfiguration : ResxClientConfigurationBase
    {
        public ProjectConfiguration(string configFile)
            : base(configFile)
        {
        }
        protected override void Initialize(XmlElement root)
        {
            if (root.FirstChild == null)
                root.AppendChild(root.OwnerDocument.CreateElement("project"));

            base.Initialize(root);
        }

        public void LoadFile(IResourceFile file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            XPathNodeIterator nodes = nav.Select("/resxClient/project/resourceFile[@FileName = '" + file.ID + "']");
            if (nodes.MoveNext())
            {
                LoadFile(file, nodes.Current);
            }
        }
        protected void LoadFile(IResourceFile file, XPathNavigator nav)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            file.LoadSettings(nav);
        }

        public void SaveFile(IResourceFile file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

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
