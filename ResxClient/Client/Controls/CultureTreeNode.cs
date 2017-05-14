using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;

namespace ResourceManager.Client.Controls
{
    [Serializable]
    public class CultureTreeNode : TreeNode
    {
        private VSCulture culture;
        private const string displayText = "{0}";

        public CultureTreeNode(VSCulture culture)
        {
            this.culture = culture;

            setText();
        }

        private void setText()
        {
            this.Text = String.Format(CultureInfo.CurrentCulture, displayText, culture.Culture.DisplayName);
        }
        public VSCulture Culture
        {
            get { return culture; }
        }
        public void Refresh()
        {
            setText();
        }
    }
}
