using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RocksmithToolkitUpdater {
    static class Program 
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutoUpdaterForm(args));
        }
    }
}
