using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourcenManager.Core
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
                Value = values.Current.Value;
            }
        }
        public VSResxData(VSResxFile file, string name, string value)
            : base(file, name, value)
        {
        }
    }
}
