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
        private const string displayText = "{0} [{1}]";

        public IResourceFile ResxFile
        {
            get { return file; }
            set 
            {                 
                file = value;
                this.Text = String.Format(displayText, file.File.Name, file.Culture.Name);
            }
        }
        public void Refresh()
        {
            this.Text = String.Format(displayText, file.File.Name, file.Culture.Name);
        }
    }
}
