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
using VisualSAIStudio.Forms;
using WeifenLuo.WinFormsUI.Docking.Themes;
using VisualSAIStudio.Properties;

namespace VisualSAIStudio
{
    public partial class StartPage : DockContent, IReloadable
    {

        public event EventHandler LoadDialogRequest = delegate { };
        public event EventHandler LoadRequest = delegate { };
        private Font headerFont;
        private Font titleFont;
        private Icon icon48;
        private Brush headerBrush;
        private Brush titleBrush;
        private Rectangle iconRect;

        public StartPage()
        {
            InitializeComponent();
            headerFont = new Font("Calibri Light", 20, FontStyle.Regular);
            titleFont = new Font("Tahoma", 32, FontStyle.Regular);
            icon48 = new Icon(Resources.sai_icon, 48, 48);
            iconRect = new Rectangle(37, 40, 48, 48);
            ThemeMgr.Instance.RegisterControl(this);
        }

        ~StartPage()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            this.BackColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.contentBackcolor);
            this.titleBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.ListSelectionForeColor));
            this.headerBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.ListSelectionBackColor));
        }

        private void StartPage_Load(object sender, EventArgs e)
        {
            OpenedHistory.Instance.InvokeMethod(new OpenedHistory.DelgateMethod(AddRecent));
        }

        private void AddRecent(OpenedHistoryAction action, int num)
        {
            LinkLabel ll = new LinkLabel();
            ll.AutoSize = true;
            ll.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            ll.LinkColor = System.Drawing.Color.FromArgb(41, 90, 160);
            ll.Location = new System.Drawing.Point(59, 260 + num * 24);
            ll.Text = action.type.ToString() + "  - " + action.name + " (" + action.entry + ")";
            ll.Tag = action;
            ll.Click += ll_Click;
            this.Controls.Add(ll);
        }

        void ll_Click(object sender, EventArgs e)
        {
            LoadRequest(this, new LoadRequestEventArgs((OpenedHistoryAction)((LinkLabel)sender).Tag));
        }

        private void LoadCreature_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadDialogRequest(this, new LoadRequestEventArgs(SmartScripts.SAIType.Creature));
        }

        private void LoadGameObject_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadDialogRequest(this, new LoadRequestEventArgs(SmartScripts.SAIType.Gameobject));
        }

        private void StartPage_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(headerBrush, 0, 0, this.Width, 100);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawString("Start", headerFont, Brushes.DarkGray, 37, 115);
            //e.Graphics.DrawString("Discover", headerFont, Brushes.DarkGray, 375, 115);
            e.Graphics.DrawString("Recent", headerFont, Brushes.DarkGray, 37, 247);
            e.Graphics.DrawIcon(icon48, iconRect);
            e.Graphics.DrawString("sAI studio 2015", titleFont, titleBrush, 104, 45);
        }

    }

    public class LoadRequestEventArgs : EventArgs
    {
        public SmartScripts.SAIType type {get; set;}
        public int entry { get; set; }
        public LoadRequestEventArgs(SmartScripts.SAIType type)
        {
            this.type = type;
        }
        public LoadRequestEventArgs(OpenedHistoryAction action)
        {
            type = action.type;
            entry = action.entry;
        }
    }
}

