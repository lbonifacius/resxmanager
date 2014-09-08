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
            string msg = "";
            if(!e.Message.TrimEnd(' ').EndsWith("."))
                msg = String.Format(Properties.Resources.ErrorTextDefault, e.Message.TrimEnd(' ') + ". ");
            else
                msg = String.Format(Properties.Resources.ErrorTextDefault, e.Message);

            MessageBox.Show(msg, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
