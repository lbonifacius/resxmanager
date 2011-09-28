using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourcenManager.Properties;

namespace ResourcenManager.Converter.Exceptions
{
    public class ProjectUnknownException : Exception
    {
        public ProjectUnknownException(string projectName)
            : base(String.Format(Resources.ProjectUnknownException, projectName))
        { }
    }
}
