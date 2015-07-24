using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisualSAIStudio
{
    public partial class StartPage : DockContent
    {
        List<LinkLabel> recents = new List<LinkLabel>();


        public StartPage()
        {
            InitializeComponent();
        }

        private void StartPage_Load(object sender, EventArgs e)
        {
        }

        public void AddRecent(RecentType type, string name, int id)
        {
            LinkLabel ll = new LinkLabel();
            ll.AutoSize = true;
            ll.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            ll.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            ll.Location = new System.Drawing.Point(59, 284+recents.Count*24);
            ll.Text = type.ToString() + "  - " + name + " (" + id + ")";
            this.Controls.Add(ll);
            recents.Add(ll);
        }
    }

    public enum RecentType
    {
        Creature,
        Gameobject,
    }
}
