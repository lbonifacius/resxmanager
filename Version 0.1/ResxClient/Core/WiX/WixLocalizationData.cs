using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourcenManager.Core
{
    public class WixLocalizationData : ResourceDataBase
    {
        public WixLocalizationData(WixLocalizationFile file, XPathNavigator nav)
            : base(file)
        {
            Name = nav.GetAttribute("Id", "");
            Value = nav.Value;
        }
        public WixLocalizationData(WixLocalizationFile file, string name, string value)
            : base(file, name, value)
        {
        }
    }
}
