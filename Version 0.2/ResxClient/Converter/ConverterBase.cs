using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceManager.Core;

namespace ResourceManager.Converter
{
    public abstract class ConverterBase
    { 
        public ConverterBase(VSSolution solution)
        {
            this.Solution = solution;
        }
        public ConverterBase(IEnumerable<VSProject> projects)
        {
            this.Projects = projects;
            this.Solution = projects.First().Solution;
        }
        public ConverterBase(VSProject project)
        {
            var list = new List<VSProject>(1);
            list.Add(project);
            this.Projects = list;
            this.Solution = project.Solution;
        }
        public ConverterBase(IEnumerable<IResourceFileGroup> fileGroups, VSSolution solution)
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
