using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourceManager.Core
{
    public class WixLocalizationData : ResourceDataBase
    {
        public WixLocalizationData(WixLocalizationFile file, XPathNavigator nav)
            : base(file)
        {
            Name = nav.GetAttribute("Id", "");
            xmlValue = nav.Value;
        }
        public WixLocalizationData(WixLocalizationFile file, string name)
            : base(file, name)
        {
        }
    }
}
