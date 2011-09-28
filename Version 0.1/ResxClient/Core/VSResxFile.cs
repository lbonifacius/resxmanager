using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;

namespace ResourcenManager.Core
{
    public class VSResxFile : IProjectFileSettings
    {
        private FileInfo file;
        private Dictionary<string, VSResxData> data = new Dictionary<string,VSResxData>();
        private CultureInfo culture;
        private bool cultureInfoFromFilename;
        private string prefix;
        private VSResxFileGroup group;
        private VSFileContainer folder;
        private XmlDocument xml;

        public VSResxFile(VSFileContainer folder, FileInfo file)
        {
            this.file = file;
            this.folder = folder;

            xml = new XmlDocument();
            xml.Load(file.FullName);
            XPathNavigator nav = xml.CreateNavigator();
            XPathNodeIterator nodes = nav.Select("/root/data");
            while (nodes.MoveNext())
            { 
                VSResxData d = new VSResxData(this, nodes.Current);

                if(!data.ContainsKey(d.Name))
                    data.Add(d.Name, d);
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
                    culture = CultureInfo.GetCultureInfo(parts[posCultureInfo]);
                    cultureInfoFromFilename = true;
                    prefix = file.Name.Substring(0, file.Name.LastIndexOf(parts[posCultureInfo]));

                    if (prefix.LastIndexOf('.') == prefix.Length - 1)
                        prefix = prefix.Substring(0, prefix.Length - 1);
                }
                catch 
                {
                    culture = CultureInfo.InvariantCulture;
                }

                folder.Project.ResxProjectFile.LoadFile(this);
            }

            if (prefix == null || prefix == "")
            {
                prefix = Path.GetFileNameWithoutExtension(file.Name);
            }            
        }
        internal void setFileGroup(VSResxFileGroup group)
        {
            this.group = group;

            if(culture != null)
                group.Container.Project.Solution.AddCultureFile(this);
        }
        public FileInfo File
        {
            get { return file; }
        }
        public Dictionary<string, VSResxData> Data
        {
            get { return data; }
            set { data = value; }
        }
        public string Prefix
        {
            get
            {
                return prefix;
            }
        }
        public string ID
        {
            get
            {
                return folder.ID + "_" + prefix;
            }
        }
        public CultureInfo Culture
        {
            get { return culture; }
            set 
            {
                if (culture != value)
                {
                    this.cultureInfoFromFilename = false;
                    culture = value;

                    if (folder.Project.UnassignedFiles.Contains(this))
                        folder.Project.UnassignedFiles.Remove(this);
                }
            }
        }
        public bool CultureInfoFromFilename
        {
            get { return cultureInfoFromFilename; }
        }
        public VSResxFileGroup ResxFileGroup
        {
            get { return group; }
        }

        public void LoadSettings(XPathNavigator nav)
        {
            culture = CultureInfo.GetCultureInfo(nav.GetAttribute("Culture", ""));
        }
        public void SaveSettings(XmlElement node)
        {
            if (!CultureInfoFromFilename && culture != null)
            {
                node.SetAttribute("Culture", culture.Name);
            }
        }

        public void CreateVSResxData(string name, string value)
        {
            XmlElement element = xml.CreateElement("data");
            element.SetAttribute("name", name);
            XmlElement dataElement = xml.CreateElement("value");
            dataElement.InnerText = value;
            element.AppendChild(dataElement);

            xml.DocumentElement.AppendChild(element);

            VSResxData resxData = new VSResxData(this, name, value);
            data.Add(name, resxData);
            this.ResxFileGroup.AllData[name].Add(resxData);
        }
        internal void SetVsResxData(VSResxData data)
        {
            XmlElement valNode = xml.DocumentElement.SelectSingleNode("data[@name = '" + data.Name + "']/value") as XmlElement;
            valNode.InnerText = data.Value;
        }

        public void Save()
        {
            bool isReadOnly = file.IsReadOnly;

            if (isReadOnly)
                SetReadOnlyAttribute(file, false);

            xml.Save(file.FullName);

            if(isReadOnly)
                SetReadOnlyAttribute(file, true);
        }

        /// <summary>
        /// Sets the read only attribute.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        private static void SetReadOnlyAttribute(FileInfo fileInfo, bool readOnly)
        {
            FileAttributes attribute;
            if (readOnly)
                attribute = fileInfo.Attributes | FileAttributes.ReadOnly;
            else
                attribute = (FileAttributes)(fileInfo.Attributes - FileAttributes.ReadOnly);

            System.IO.File.SetAttributes(fileInfo.FullName, attribute);
        }
    }
}
