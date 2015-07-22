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
        bool painted = false;
        private string loading;

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            StringsDB.GetInstance().CurrentAction += this_eventHandler;
        }

        private void this_eventHandler(object sender, EventArgs e)
        {
            loading = ((LoadingEventArgs)e).loading;
            this.Invoke(new MethodInvoker(delegate
            {
                this.Refresh();
            }));
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            if (!painted) 
                e.Graphics.DrawImage(BackgroundImage, 0, 0, 599, 360);
            e.Graphics.DrawImage(BackgroundImage, new Rectangle(27, 92, 560, 184), new Rectangle(27, 92, 560, 184), GraphicsUnit.Pixel);
            
            if (loading != null)
                e.Graphics.DrawString("Loading " + loading, new Font("Tahoma", 10), Brushes.Aqua, 340, 204);
            painted = true;
        }

    }
}
