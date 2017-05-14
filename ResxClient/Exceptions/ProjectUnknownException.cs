using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceManager.Properties;

namespace ResourceManager.Exceptions
{
    public class ProjectUnknownException : Exception
    {
        public ProjectUnknownException(string projectName)
            : base(String.Format(Resources.ProjectUnknownException, projectName))
        { }
    }
}
