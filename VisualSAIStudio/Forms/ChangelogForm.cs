using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio.Forms
{
    public partial class ChangelogForm : MetroForm.MetroForm
    {
        public ChangelogForm()
        {
            InitializeComponent();
            ReloadTheme();
        }

        private void ChangelogForm_Load(object sender, EventArgs e)
        {
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                string htmlCode = client.DownloadString("http://saistudio.tk/changelog/has");
                if (htmlCode.Equals("Y"))
                    this.webBrowser1.Url = new System.Uri("http://saistudio.tk/changelog/changelog.html", System.UriKind.Absolute);
            }
        }

    }
}
