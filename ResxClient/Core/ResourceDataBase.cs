using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourceManager.Core
{
    public class ResourceDataBase
    {
        private string name;
        protected string xmlValue;
        protected string comment;
        private IResourceFile file;

        public ResourceDataBase(IResourceFile file)
        {
            this.file = file;
        }
        public ResourceDataBase(IResourceFile file, string name, string value, string comment)
        {
            this.file = file;
            this.name = name;
            this.xmlValue = cleanValue(value);
            this.comment = comment;
        }
        public ResourceDataBase(IResourceFile file, string name)
        {
            this.file = file;
            this.name = name;
        }
        public void Reference()
        {
            file.FileGroup.RegisterResourceData(this);
        }

        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }
        public IResourceFile ResxFile
        {
            get { return file; }
        } 
        public string Value
        {
            get { return xmlValue; }
            set 
            {
                if (xmlValue != value)
                {
                    HasChanged = true;
                    xmlValue = cleanValue(value);
                }
            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    HasChanged = true;
                    comment = value;
                }
            }
        }
        public bool HasChanged
        {
            get;
            internal set;
        }


        private string cleanValue(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            return s.Replace(Convert.ToString((char)(0x1F)), String.Empty);
        }
    }
}
