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
        public ConverterBase(VSProject project)
        {
            this.Project = project;
            this.Solution = project.Solution;
        }

        public VSProject Project
        {
            get;
            private set;
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
    }
}
