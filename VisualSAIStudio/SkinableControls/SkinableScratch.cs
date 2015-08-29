using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking.Colors;
using WeifenLuo.WinFormsUI.Docking.Themes;

namespace VisualSAIStudio.SkinableControls
{
    class SkinableScratch : Scratch , WeifenLuo.WinFormsUI.Docking.IReloadable
    {
        public SkinableScratch() : base()
        {
            ThemeMgr.Instance.RegisterControl(this);
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        ~SkinableScratch()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            this.BackColor = ThemeMgr.Instance.getColor(IKnownColors.contentBackcolor);
            this.ForeColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);
            this.brush = new SolidBrush(ThemeMgr.Instance.getColor(IKnownColors.FormText));
            this.Invalidate();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SkinableScratch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "SkinableScratch";
            this.Load += new System.EventHandler(this.SkinableScratch_Load);
            this.ResumeLayout(false);

        }

        private void SkinableScratch_Load(object sender, EventArgs e)
        {

        }
    }
}
