using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI.Docking.Colors;
using WeifenLuo.WinFormsUI.Docking.Themes;

namespace VisualSAIStudio.SkinableControls
{
    class SkinableFastColoredTextBox : FastColoredTextBoxNS.FastColoredTextBox, IReloadable
    {
        public SkinableFastColoredTextBox() : base()
        {
            ThemeMgr.Instance.RegisterControl(this);
            ReloadTheme();
        }

        ~SkinableFastColoredTextBox()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            this.BackColor = ThemeMgr.Instance.getColor(IKnownColors.contentBackcolor);
            this.IndentBackColor = ThemeMgr.Instance.getColor(IKnownColors.FormBackground);
            this.ForeColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);
            this.LineNumberColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // SkinableFastColoredTextBox
            // 
            this.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Name = "SkinableFastColoredTextBox";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
