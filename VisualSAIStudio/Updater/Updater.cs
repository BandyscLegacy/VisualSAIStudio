using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio.Updater
{
    class Update
    {
        public Update()
        {
            if (Properties.Settings.Default.AcceptedUpdates == false)
            {

                    DialogResult res =
                      PSTaskDialog.cTaskDialog.ShowTaskDialogBox("Updates",
                                                "Updates",
                                                "Visual SAI Studio connectes to the internet in order to check for updates. Do you agree?",
                                                "Along with current version SAI Studio sends random installation ID (random number), it is for statistics, there is no way your pc can be identified with it (if you do not believe it, check sources at github.com/bandysc/visualsaistudio)",
                                                "",
                                                "",
                                                "",
                                                "I accept to check for updates|I disagree",
                                                PSTaskDialog.eTaskDialogButtons.Cancel,
                                                PSTaskDialog.eSysIcons.Question, PSTaskDialog.eSysIcons.Information);


                    if (PSTaskDialog.cTaskDialog.CommandButtonResult == 1 || res == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                    Properties.Settings.Default.AcceptedUpdates = true;                    
                    Properties.Settings.Default.Save();
            }
        }

        public async void AsyncCheckForUpdates()
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                   { "uid", Properties.Settings.Default.UID },
                   { "version", Application.ProductVersion.Replace(".","") }
                };

                var content = new FormUrlEncodedContent(values);

                try
                {
                    var response = await client.PostAsync("http://saistudio.tk/version.php", content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    UpdateData result = JsonConvert.DeserializeObject<UpdateData>(responseString);

                    if (result.result == UpdateResult.NewVersion)
                    {
                        DialogResult res =
                          PSTaskDialog.cTaskDialog.ShowTaskDialogBox("Update",
                                                    "New version",
                                                    "New Visual SAI Studio is avaliable. Do you want to download it now? SAI will close.",
                                                    "",
                                                    "",
                                                    "",
                                                    "",
                                                    "Download now|Show changelog|Download later",
                                                    PSTaskDialog.eTaskDialogButtons.Cancel,
                                                    PSTaskDialog.eSysIcons.Question, PSTaskDialog.eSysIcons.Information);


                        if (PSTaskDialog.cTaskDialog.CommandButtonResult == 0)
                        {
                            if (System.IO.File.Exists("SAIUpdater.exe"))
                            {
                                Process.Start("SAIUpdater.exe");
                                Application.Exit();
                            }
                            else
                            {
                                MessageBox.Show("Updater not found!");
                            }
                        } else if (PSTaskDialog.cTaskDialog.CommandButtonResult == 1)
                        {
                            Forms.ChangelogForm changelog = new Forms.ChangelogForm();
                            changelog.Show();
                        }
                    }

                }
                catch (Exception)
                {
                    
                    // no internet
                }



            }
        }
        private static Update instance;
        public static Update GetInstance()
        {
            if (instance == null)
                instance = new Update();
            return instance;
        }
    }

}
