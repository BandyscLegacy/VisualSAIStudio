using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio.Loader
{
    class LoadManager
    {

        public static void CheckForDB()
        {
            if (String.IsNullOrEmpty(Properties.Settings.Default.DBUser))
            {
                Forms.DatabaseConnectForm db = new Forms.DatabaseConnectForm();
                db.ShowDialog();
            }
        }

        public static bool CheckForDBCSettings(bool force = false)
        {
            if (force || (Properties.Settings.Default.DBC == "" || !System.IO.Directory.Exists(Properties.Settings.Default.DBC))
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
                    Properties.Settings.Default.Save();
                    return true;
                }
                Properties.Settings.Default.Save();
                return false;
            }
            return false;
        }
    }
}
