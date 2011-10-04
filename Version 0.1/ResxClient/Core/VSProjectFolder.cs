using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ResourceManager.Core
{
    public class VSProjectFolder : VSFileContainer
    {
        public VSProjectFolder(VSProject project, VSFileContainer parent, string filepath) 
            : base(project, parent, filepath)
        {
            base.Init(filepath);
        }

        public bool ContainsFiles()
        {
            if (this.Files.Count > 0)
                return true;
            else
            {
                foreach (VSProjectFolder folder in Directories)
                {
                    if (folder.ContainsFiles())
                        return true;
                }

                return false;
            }
        }
    }
}
