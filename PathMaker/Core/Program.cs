using System;
using System.Windows.Forms;
using PathFinder.Interface;

namespace PathFinder.Core
{
    /// <summary>
    /// MainClass приложения.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Метод Main.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            System.IO.Directory.CreateDirectory("Data");
            System.IO.Directory.CreateDirectory("Data\\Database");
            System.IO.Directory.CreateDirectory("Data\\Chunks");
            System.IO.Directory.CreateDirectory("plugins");
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
