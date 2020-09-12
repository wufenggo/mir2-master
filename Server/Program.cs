using System;
using System.IO;
using System.Windows.Forms;

namespace Server
{
    static class Program
    {

        public static string LoginNoticeDir = @".\LoginNotice.txt";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Packet.IsServer = true;

            try
            {
                if (!File.Exists(LoginNoticeDir))
                {
                    FileStream fs = File.Create(LoginNoticeDir);
                    fs.Close();
                }
                Settings.Load();
                //Resource.Load();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SMain());

                Settings.Save();
            }
            catch(Exception ex)
            {
                File.AppendAllText(Settings.LogPath + "Error Log (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                                           String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, ex.ToString()));
            }
        }
    }
}
