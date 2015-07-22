using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
        }

        bool painted = false;
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            if (painted) return;
            e.Graphics.DrawImage(BackgroundImage, 0, 0, 599, 360);
            e.Graphics.DrawString("Loading dbc", new Font("Tahoma", 8.25f), Brushes.White, 340, 205);
            painted = true;
        }
    }
}
