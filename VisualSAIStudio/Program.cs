using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSAIStudio.Updater;

namespace VisualSAIStudio
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (String.IsNullOrEmpty(Properties.Settings.Default.UID))
            {
                Properties.Settings.Default.UID = Extensions.MD5(new Random().Next(Int32.MaxValue).ToString());
                Properties.Settings.Default.Save();
            }
            new MyApp().Run(args);
        }
    }

    class MyApp : WindowsFormsApplicationBase
    {
        protected override void OnCreateSplashScreen()
        {
            Update updater = Update.GetInstance();
            CheckForDBCSettings();
            CheckForDB();
            this.SplashScreen = new SplashScreen();
        }

        private void CheckForDB()
        {
            if (String.IsNullOrEmpty(Properties.Settings.Default.DBUser))
            {
                Forms.DatabaseConnectForm db = new Forms.DatabaseConnectForm();
                db.ShowDialog();
            }
        }

        private void CheckForDBCSettings()
        {
            if ((Properties.Settings.Default.DBC == "" || !System.IO.Directory.Exists(Properties.Settings.Default.DBC))
                            && !Properties.Settings.Default.IgnoreMissingDBC)
            {
                DialogResult res =
                  PSTaskDialog.cTaskDialog.ShowTaskDialogBox("DBC",
                                            "Missing DBC",
                                            "Visual SAI Studio makes big use of DBC (client database). If you locate dbc folder more features will be avaliable. ",
                                            "Note: currently only DBC from WoW 3.3.5 and 4.3.4. 6.x and 5.4.x will be added in the next update",
                                            "",
                                            "Don't check for DBC any more",
                                            "",
                                            "Locate WoW DBC Folder|Ignore for now",
                                            PSTaskDialog.eTaskDialogButtons.Cancel,
                                            PSTaskDialog.eSysIcons.Question, PSTaskDialog.eSysIcons.Information);
                if (PSTaskDialog.cTaskDialog.VerificationChecked)
                    Properties.Settings.Default.IgnoreMissingDBC = true;
                if (PSTaskDialog.cTaskDialog.CommandButtonResult == 0)
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        Properties.Settings.Default.DBC = fbd.SelectedPath;
                    }
                }
                Properties.Settings.Default.Save();
            }
        }

        protected override void OnCreateMainForm()
        {
            // Then create the main form, the splash screen will close automatically
            DB.GetInstance().LoadSmarts();
            this.MainForm = new MainForm();
        }
    }
}
