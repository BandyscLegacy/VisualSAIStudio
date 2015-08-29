using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            new MyApp().Run(args);
        }
    }

    class MyApp : WindowsFormsApplicationBase
    {
        protected override void OnCreateSplashScreen()
        {
            this.SplashScreen = new SplashScreen();
            DB.GetInstance().FinishedLoading += MyApp_FinishedLoading;
        }

        void MyApp_FinishedLoading(object sender, EventArgs e)
        {

        }
        protected override void OnCreateMainForm()
        {

            // Then create the main form, the splash screen will close automatically
            this.MainForm = new MainForm();
        }
    }
}
