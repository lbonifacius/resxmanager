using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceManager.Core;

namespace ResourceManager.Converter
{
    public abstract class ConverterBase
    { 
        protected ConverterBase(VSSolution solution)
        {
            this.Solution = solution;
        }
        protected ConverterBase(IEnumerable<VSProject> projects)
        {
            this.Projects = projects;
            this.Solution = projects.First().Solution;
        }
        protected ConverterBase(VSProject project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            var list = new List<VSProject>(1);
            list.Add(project);
            this.Projects = list;
            this.Solution = project.Solution;
        }
        protected ConverterBase(IEnumerable<IResourceFileGroup> fileGroups, VSSolution solution)
        {
            this.FileGroups = fileGroups;
            this.Solution = solution;
        }

        public IEnumerable<IResourceFileGroup> FileGroups
        {
            get;
            private set;
        }
        public IEnumerable<VSProject> Projects
        {
            get;
            private set;
        }
        public IEnumerable<VSCulture> Cultures
        {
            get;
            set;
        }
        public VSSolution Solution
        {
            get;
            private set;
        }
        public bool ExportDiff
        {
            get;
            set;
        }
        public bool ExportComments
        {
            get;
            set;
        }

	     public bool IncludeProjectsWithoutTranslations
	     { 
		      get; 
			   set;
	     }
         public bool AutoAdjustLayout
         {
             get;
             set;
         }
         public bool IgnoreInternalResources
         {
             get;
             set;
         }
    }
}
