using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;

namespace ResourceManager.Client.Controls
{
    public class CultureAnalysisResultTreeNode : TreeNode
    {
        public CultureAnalysisResultTreeNode(VSCulture sourceCulture, VSCulture targetCulture) : base()
        {
            this.ImageIndex = 5;
            this.SelectedImageIndex = 5;
            this.TargetCulture = targetCulture;
            this.SourceCulture = sourceCulture;
        }

        public VSCulture TargetCulture
        {
            get;
            private set;
        }
        public VSCulture SourceCulture
        {
            get;
            private set;
        } 
    }
}
