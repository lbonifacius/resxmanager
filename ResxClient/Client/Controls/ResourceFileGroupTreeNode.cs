using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;

namespace ResourceManager.Client.Controls
{
    public class ResourceFileGroupTreeNode : TreeNode
    {
        private IResourceFileGroup fileGroup;
        private const string displayText = "{0}";

        public ResourceFileGroupTreeNode(IResourceFileGroup group)
        {
            this.fileGroup = group;

            setText();
        }

        private void setText()
        {
            this.Text = String.Format(displayText, fileGroup.Prefix);
        }
        public IResourceFileGroup FileGroup
        {
            get { return fileGroup; }
        }
        public void Refresh()
        {
            setText();
        }
    }
}
