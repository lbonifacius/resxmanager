using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace ResourceManager.Core.Configuration
{
    public class ResxClientConfigurationBase
    {
        public const string RESXCLIENTPROJECTFILE_EXTENSION = ".resxm";

        protected XmlDocument xml;
        protected XPathNavigator nav;
        private string url;

        public ResxClientConfigurationBase(string url)
        {
            this.url = url;
            xml = new XmlDocument();

            if(File.Exists(url))
                xml.Load(url);
            nav = xml.CreateNavigator();

            if (xml.DocumentElement == null)
                xml.AppendChild(xml.CreateElement("resxClient"));

            Initialize(xml.DocumentElement);
        }

        protected virtual void Initialize(XmlElement root)
        {
            SkipGroups = getSkipEntities("//groups/exclude");
            SkipFiles = getSkipEntities("//files/exclude");
            SkipDirectories = getSkipEntities("//directories/exclude");
        }
        public List<EntityNameMatcher> SkipGroups
        {
            get;
            private set;
        }
        public List<EntityNameMatcher> SkipFiles
        {
            get;
            private set;
        }
        public List<EntityNameMatcher> SkipDirectories
        {
            get;
            private set;
        }

        protected List<EntityNameMatcher> getSkipEntities(string xpath)
        {
            var skips = nav.Select(xpath);
            var items = new List<EntityNameMatcher>(skips.Count);

            while (skips.MoveNext())
                items.Add(new EntityNameMatcher(skips.Current));

            return items;
        }

        public void Save()
        {
            xml.Save(url);
        }
    }
}