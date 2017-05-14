using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourceManager.Core
{
    public class VSResxData : ResourceDataBase
    {
        public VSResxData(VSResxFile file, XPathNavigator nav)
            : base(file)
        {
            Name = nav.GetAttribute("name", "");

            XPathNodeIterator values = nav.Select("value");
            if (values.MoveNext())
            {
                xmlValue = values.Current.Value;
            }
            values = nav.Select("comment");
            if (values.MoveNext())
            {
                comment = values.Current.Value;
            }
        }
        public VSResxData(VSResxFile file, string name)
            : base(file, name)
        {
        }
    }
}
