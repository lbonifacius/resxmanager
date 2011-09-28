using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourcenManager.Core
{
    public class VSResxData
    {
        private string name;
        private string xmlValue;
        private VSResxFile file;

        public VSResxData(VSResxFile file, XPathNavigator nav)
        {
            this.file = file;
            name = nav.GetAttribute("name", "");

            XPathNodeIterator values = nav.Select("value");
            if (values.MoveNext())
            {
                xmlValue = values.Current.Value;
            }            
        }
        public VSResxData(VSResxFile file, string name, string value)
        {
            this.file = file;
            this.name = name;
            this.xmlValue = value;
        }
        public void Reference()
        {
            file.ResxFileGroup.RegisterResxData(this);
        }

        public string Name
        {
            get { return name; }
        }
        public VSResxFile ResxFile
        {
            get { return file; }
        } 
        public string Value
        {
            get { return xmlValue; }
            set 
            { 
                xmlValue = value;

                file.SetVsResxData(this);
            }
        }
	
	
    }
}
