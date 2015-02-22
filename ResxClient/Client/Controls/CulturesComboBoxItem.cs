using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ResourceManager.Client.Controls
{
    public class CulturesComboBoxItem
    {
        public CulturesComboBoxItem(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException("culture");

            this.DisplayName = culture.DisplayName;
            this.Name = culture.Name;
        }

        public string Name
        { 
            get; private set; 
        }

        public string DisplayName
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
