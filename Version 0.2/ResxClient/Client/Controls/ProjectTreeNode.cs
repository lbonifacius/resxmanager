using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;

namespace ResourceManager.Client.Controls
{
    public class ProjectTreeNode : TreeNode
    {
        private VSProject project;

        public VSProject Project
        {
            get { return project; }
            set 
            {
                project = value;
                this.Text = project.Name;
            }
        }	
    }
}
