using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;
using System.Linq;

namespace ResourceManager.Core
{
    public abstract class ResourceFileBase : IProjectFileSettings, IResourceFile
    {
        private FileInfo file;
        private Dictionary<string, ResourceDataBase> data = new Dictionary<string, ResourceDataBase>();
        private CultureInfo culture;
        private bool cultureInfoFromFilename;
        private string prefix;
        private IResourceFileGroup group;
        private VSFileContainer folder;

        public ResourceFileBase(VSFileContainer folder, FileInfo file)
        {
            this.file = file;
            this.folder = folder;
        }
        public void SetFileGroup(IResourceFileGroup group)
        {
            this.group = group;

            if(culture != null)
                group.Container.Project.Solution.AddCultureFile(this);
        }
        public FileInfo File
        {
            get { return file; }
        }
        public Dictionary<string, ResourceDataBase> Data
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
            protected set
            {
                prefix = value;
            }
        }
        public string ID
        {
            get
            {
                return folder.ID + "_" + file.Name;
            }
        }
        public CultureInfo Culture
        {
            get { return culture; }
            set 
            {
                if (culture != value)
                {
                    if (culture != null)
                    {
                        // Refresh collections with new Culture.
                        foreach (var dataGroup in this.FileGroup.AllData.Values)
                        {
                            var list = (from dataEntry in dataGroup.ResxData where dataEntry.Key == culture && this.Data.ContainsValue(dataEntry.Value) select dataEntry).ToArray();

                            foreach (var pair in list)
                            {
                                dataGroup.ResxData.Remove(pair.Key);
                                dataGroup.ResxData.Add(value, pair.Value);
                            }
                        }

                        folder.Project.Solution.ChangeCulture(this, value);

                        FileGroup.ChangeCulture(this, value);
                    }

                    this.cultureInfoFromFilename = false;
                    culture = value;

                    if (folder.Project.UnassignedFiles.Contains(this))
                        folder.Project.UnassignedFiles.Remove(this);
                }
            }
        }
        public bool IsCultureAutoDetected
        {
            get { return cultureInfoFromFilename; }
            protected set { cultureInfoFromFilename = value; }
        }
        public IResourceFileGroup FileGroup
        {
            get { return group; }
        }

        public void LoadSettings(XPathNavigator nav)
        {
            culture = CultureInfo.GetCultureInfo(nav.GetAttribute("Culture", ""));
        }
        public void SaveSettings(XmlElement node)
        {
            if (!IsCultureAutoDetected && culture != null)
            {
                node.SetAttribute("Culture", culture.Name);
            }
        }

        public abstract void CreateResourceData(string name, string value);
        public abstract void CreateResourceDataComment(string name, string comment);

        public void SetResourceData(string name, string value)
        {
            if (Data.ContainsKey(name))
            {
                Data[name].Value = value;
            }
            else
            {
                CreateResourceData(name, value);
            }
        }
        public void SetResourceDataComment(string name, string comment)
        {
            if (Data.ContainsKey(name))
            {
                Data[name].Comment = comment;
            }
            else
            {
                CreateResourceDataComment(name, comment);
            }
        }

        public abstract void Save();

        /// <summary>
        /// Sets the read only attribute.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        protected static void SetReadOnlyAttribute(FileInfo fileInfo, bool readOnly)
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
