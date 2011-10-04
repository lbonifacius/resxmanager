using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourcenManager.Core
{
    public class ResourceDataBase
    {
        private string name;
        private string xmlValue;
        private IResourceFile file;

        public ResourceDataBase(IResourceFile file)
        {
            this.file = file;
        }
        public ResourceDataBase(IResourceFile file, string name, string value)
        {
            this.file = file;
            this.name = name;
            this.xmlValue = value;
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
                xmlValue = value;
            }
        }
    }
}
