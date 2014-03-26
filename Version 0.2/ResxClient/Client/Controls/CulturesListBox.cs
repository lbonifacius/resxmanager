using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;

namespace ResourceManager.Client.Controls
{
    public class CulturesListBox : ListBox
    {
        private VSSolution solution = null;

        public void LoadCultures(VSSolution s, IEnumerable<VSCulture> selectedCultures)
        {
            solution = s;

            int i = 0;
            foreach (var culture in solution.Cultures.Values)
            {
                var item = new CultureListBoxEntry(culture);
                this.Items.Add(item);

                if (selectedCultures.Contains(culture))
                    this.SetSelected(i, true);

                i++;
            }
        }

        public List<VSCulture> GetSelectedCultures()
        {
            var list = new List<VSCulture>();

            foreach (var item in this.SelectedItems)
            {
                list.Add(((CultureListBoxEntry)item).Culture);
            }
            return list;
        }
    }

    public class CultureListBoxEntry
    {
        public VSCulture Culture
        {
            get;
            private set;
        }

        public CultureListBoxEntry(VSCulture c)
        {
            this.Culture = c;
        }

        public override string ToString()
        {
            return Culture.Culture.DisplayName;
        }
    }
}
