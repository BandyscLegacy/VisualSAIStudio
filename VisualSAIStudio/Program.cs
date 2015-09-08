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
            Loader.LoadManager.CheckForDBCSettings();
            Loader.LoadManager.CheckForDB();
            this.SplashScreen = new SplashScreen();
        }

        protected override void OnCreateMainForm()
        {
            // Then create the main form, the splash screen will close automatically
            DB.GetInstance().LoadSmarts();
            this.MainForm = new MainForm();
        }
    }
}
