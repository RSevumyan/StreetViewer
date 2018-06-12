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
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
