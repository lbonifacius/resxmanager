using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;

namespace ResourceManager.Client.Controls
{
    public class ResxFileTreeNode : TreeNode
    {
        private IResourceFile file;

        public IResourceFile ResxFile
        {
            get { return file; }
            set 
            {                 
                file = value;
                this.Text = file.File.Name;
            }
        }	
    }
}
