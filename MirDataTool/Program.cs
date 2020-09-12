using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MirDataTool
{
    static class Program
    {
        /// <summary>
        /// If we found the config file upon launching
        /// </summary>
        public static bool firstRun = true;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //  Check if the config file exists, we'll use this for the users to setup the Paths.
            if (File.Exists(@".\Config.ini"))
                firstRun = false;
            Settings.Load();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MirDataTool());
        }
    }
}
