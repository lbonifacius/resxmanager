using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ResourcenManager.Core;

namespace ResourcenManager.Client.Controls
{
    public class ResxFileTreeNode : TreeNode
    {
        private VSResxFile file;

        public VSResxFile ResxFile
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
