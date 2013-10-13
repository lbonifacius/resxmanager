using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ResourceManager.Client
{
    public class ExceptionHandling
    {
        public static void ShowErrorDialog(Exception e)
        {
            MessageBox.Show(String.Format(Properties.Resources.ErrorTextDefault, e.Message), Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
