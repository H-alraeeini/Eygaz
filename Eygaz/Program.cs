
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eygaz
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Func.dbname = Application.StartupPath + @"\EygazDb.db";
            DatabaseMigrator.EnsureMigrated(Func.dbname);

            using (FrmLogin login = new FrmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;
            }

            Application.Run(new Main());
        }
    }
}
