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
    public class SkinableTextBox : System.Windows.Forms.TextBox, WeifenLuo.WinFormsUI.Docking.IReloadable
    {
        public SkinableTextBox() : base()
        {
            ThemeMgr.Instance.RegisterControl(this);
            NormalColor = Color.Black;
            PlaceholderColor = Color.Gray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GotFocus +=SkinableTextBox_GotFocus;
            this.LostFocus += SkinableTextBox_LostFocus;
        }

        private void SkinableTextBox_LostFocus(object sender, EventArgs e)
        {
            if (base.Text == "")
            {
                base.Text = Placeholder;
                this.ForeColor = PlaceholderColor;
            }
        }

        private void SkinableTextBox_GotFocus(object sender, EventArgs e)
        {
            if (base.Text == Placeholder)
            {
                Text = " ";
                Text = "";
                this.ForeColor = NormalColor;
            }
        }

        ~SkinableTextBox()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            this.BackColor = ThemeMgr.Instance.getColor(IKnownColors.FormBackground);
            this.NormalColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);

            if (this.Focused)
                this.ForeColor = NormalColor;
            else
                this.ForeColor = PlaceholderColor;

            this.Refresh();
        }
        //
        // Summary:
        //     Gets or sets the current text in the System.Windows.Forms.TextBox.
        //
        // Returns:
        //     The text displayed in the control.
        public override string Text 
        {
            get
            {
                if (base.Text == Placeholder)
                    return "";
                return base.Text;
            }
            set
            {
                base.Text = value;
            } 
        }
        private string _Placeholder;
        public string Placeholder
        {
            get
            {
                return _Placeholder;
            }
            set
            {
                _Placeholder = value;
                if (base.Text == "")
                {
                    base.Text = Placeholder;
                    this.ForeColor = PlaceholderColor;
                }
            }
        }
        public Color PlaceholderColor { get; set; }
        public Color NormalColor { get; set; }
    }
}
