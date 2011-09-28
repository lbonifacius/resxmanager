using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourcenManager.Core
{
    interface IProjectFileSettings
    {
        void LoadSettings(XPathNavigator nav);
        void SaveSettings(XmlElement node);
    }
}
