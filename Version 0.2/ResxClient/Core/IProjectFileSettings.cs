using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourceManager.Core
{
    public interface IProjectFileSettings
    {
        void LoadSettings(XPathNavigator nav);
        void SaveSettings(XmlElement node);
    }
}
