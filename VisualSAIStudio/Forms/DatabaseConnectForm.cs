using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio.Forms
{
    public partial class DatabaseConnectForm : Form
    {
        public DatabaseConnectForm()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DatabaseConnectForm_Load(object sender, EventArgs e)
        {

            txtHost.Text = Properties.Settings.Default.DBHost;
            txtPort.Text = Properties.Settings.Default.DBPort;
            txtUser.Text = Properties.Settings.Default.DBUser;
            txtPassword.Text = Properties.Settings.Default.DBPass.DecryptString().ToInsecureString();
            txtDatabse.Text = Properties.Settings.Default.DBBase;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DBHost = txtHost.Text;
            Properties.Settings.Default.DBPort = txtPort.Text;
            Properties.Settings.Default.DBUser = txtUser.Text;
            Properties.Settings.Default.DBPass = txtPassword.Text.ToSecureString().EncryptString();
            Properties.Settings.Default.DBBase = txtDatabse.Text;

            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
