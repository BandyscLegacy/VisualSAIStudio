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
using VisualSAIStudio.History;

namespace VisualSAIStudio
{
    public partial class StartPage : DockContent
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void StartPage_Load(object sender, EventArgs e)
        {
            OpenedHistory.Instance.InvokeMethod(new OpenedHistory.DelgateMethod(DrawRecent));
        }

        private void DrawRecent(OpenedHistoryAction action, int num)
        {
            LinkLabel ll = new LinkLabel();
            ll.AutoSize = true;
            ll.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            ll.LinkColor = System.Drawing.Color.FromArgb(41, 90, 160);
            ll.Location = new System.Drawing.Point(59, 330 + num * 24);
            ll.Text = action.type.ToString() + "  - " + action.name + " (" + action.entry + ")";
            this.Controls.Add(ll);
        }
    }
}
