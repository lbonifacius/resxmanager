using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ResourceManager.Core.Configuration
{
    public class SolutionConfiguration : ResxClientConfigurationBase
    {
        public SolutionConfiguration(string url)
            : base(url)
        {
        }
        protected override void Initialize(XmlElement root)
        {
            if (root.FirstChild == null)
                root.AppendChild(root.OwnerDocument.CreateElement("solution"));

            base.Initialize(root);

            SkipProjects = getSkipEntities("//projects/exclude");
        }

        public List<EntityNameMatcher> SkipProjects
        {
            get;
            private set;
        }
    }

}
