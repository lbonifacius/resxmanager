using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ResourceManager.Core
{
    public class WixLocalizationFile : ResourceFileBase, IProjectFileSettings
    {
        private const string NS_WIX_2006 = "http://schemas.microsoft.com/wix/2006/localization";

        public WixLocalizationFile(VSFileContainer folder, FileInfo file) 
            : base(folder, file)
        {
            var r = new Regex(".([a-zA-Z]{2,2}([-][a-zA-Z]{2,2})*).wxl");
            var m = r.Match(file.Name);

            if (m.Success)
                Prefix = file.Name.Substring(0, file.Name.Length - m.Value.Length);
            else
                Prefix = Path.GetFileNameWithoutExtension(file.Name);

            using (XmlReader reader = new XmlTextReader(file.FullName))
            {
                XPathDocument xml = new XPathDocument(reader);

                XPathNavigator nav = xml.CreateNavigator();
                XmlNamespaceManager manager = new XmlNamespaceManager(nav.NameTable);
                manager.AddNamespace("wix", NS_WIX_2006);

                //XPathExpression expr = XPathExpression.Compile("/wix:WixLocalization");
                //expr.SetContext(manager);

                XPathNavigator cult = nav.SelectSingleNode("/wix:WixLocalization", manager);
                if (cult != null && !String.IsNullOrEmpty(cult.GetAttribute("Culture", "")))
                {
                    try
                    {
                        Culture = CultureInfo.GetCultureInfo(cult.GetAttribute("Culture", ""));
                        IsCultureAutoDetected = true;
                    }
                    catch
                    {
                        Culture = CultureInfo.InvariantCulture;
                    }
                }
                else
                    Culture = CultureInfo.InvariantCulture;

                folder.Project.ResxProjectFile.LoadFile(this);

                //expr = XPathExpression.Compile("/wix:WixLocalization/wix:String");
                //expr.SetContext(manager);

                XPathNodeIterator nodes = nav.Select("/wix:WixLocalization/wix:String", manager);
                while (nodes.MoveNext())
                {
                    WixLocalizationData d = new WixLocalizationData(this, nodes.Current);

                    if (!Data.ContainsKey(d.Name))
                        Data.Add(d.Name, d);
                }
            }
        }        

        public override void CreateResourceData(string name, string value)
        {
            WixLocalizationData resxData = new WixLocalizationData(this, name);
            resxData.Value = value;
            Data.Add(name, resxData);
            this.FileGroup.AllData[name].Add(resxData);
        }
        public override void CreateResourceDataComment(string name, string comment)
        {
            WixLocalizationData resxData = new WixLocalizationData(this, name);
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

                XmlNamespaceManager manager = new XmlNamespaceManager(xml.NameTable);
                manager.AddNamespace("wix", NS_WIX_2006);

                foreach (ResourceDataBase data in Data.Values)
                {
                    SetResourceData(xml, manager, data);
                }

                xml.Save(File.FullName);

                if (isReadOnly)
                    SetReadOnlyAttribute(File, true);

                SetSaved();
            }
        }
        public override void IncludeInProjectFile()
        {
            this.FileGroup.Container.Project.AddResourceFileToProjectFile(this, VSProjectFileTypes.EmbeddedResource);
        }
        private void SetResourceData(XmlDocument xml, XmlNamespaceManager manager, ResourceDataBase data)
        {
            XmlElement valNode = xml.DocumentElement.SelectSingleNode("wix:String[@Id = '" + data.Name + "']", manager) as XmlElement;
            if (valNode != null)
                valNode.InnerText = data.Value;
            else 
            {
                XmlElement element = xml.CreateElement("String", NS_WIX_2006);
                element.SetAttribute("Id", data.Name);
                element.InnerText = data.Value;
                xml.DocumentElement.AppendChild(element);
            }
        }
    }
}
