using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ResourceManager.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var log = log4net.LogManager.GetLogger(typeof(Program));
            if (log.IsErrorEnabled)
                log.Error("Error while executing RESX manager.", e.Exception);

            ExceptionHandling.ShowErrorDialog(e.Exception);
        }
    }
}