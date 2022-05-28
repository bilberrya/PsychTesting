using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PsychTesting
{
    static class Program
    {
        public static Form MainForm { get; set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new Autorization();

            while (MainForm != null)
            {
                Application.Run(MainForm);
            }
            Application.Exit();
        }

        public static int id = 0;
        public static string fio = null;
        public static string post = null;
    }
}

