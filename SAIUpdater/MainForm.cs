using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAIUpdater
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri("http://saistudio.tk/app/sai.zip"), @"sai.zip");

        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
 	        progressBar1.Value =e.ProgressPercentage;
            label.Text = "Downloading new version... ("+(e.BytesReceived/1024)+" / "+(e.TotalBytesToReceive/1024)+" kb)";
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            label.Text = "Extracing";
            using (var fileStream = new FileStream("sai.zip", FileMode.Open))
            {
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
                {
                    archive.ExtractToDirectory(Application.StartupPath, true);
                }
            }
            MessageBox.Show("Updated!");
            Process.Start("VisualSAIEditor.exe");
            Application.Exit();
        }
    }

    public static class ZipArchiveExtensions
    {
        public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectoryName, file.FullName);
                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }
    }
}
