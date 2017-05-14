using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;

namespace ResourceManager.Client.Controls
{
    public class ResourceFileTreeNode : TreeNode
    {
        private IResourceFile file;
        private const string displayText = "{0} [{1}]";


        public ResourceFileTreeNode(IResourceFile f)
        {
            this.file = f;

            setText();
        }
        public IResourceFile File
        {
            get { return file; }
        }
        private void setText()
        {
            this.Text = String.Format(displayText, file.File.Name, file.Culture.Name);
        }
        public void Refresh()
        {
            setText();
        }
    }
}
