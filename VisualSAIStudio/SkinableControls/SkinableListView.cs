using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI.Docking.Colors;
using WeifenLuo.WinFormsUI.Docking.Themes;

namespace VisualSAIStudio.SkinableControls
{
    class SkinableListView : ListView, IReloadable
    {
        public SkinableListView()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            ThemeMgr.Instance.RegisterControl(this);
           // this.OwnerDraw = true;
           // this.DrawColumnHeader += SkinableListView_DrawColumnHeader;

        }

        void SkinableListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds.X, 0, this.Width, e.Bounds.Height);
            using (var headerFont = new Font(this.Font.Name, 9, FontStyle.Regular))
            {
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, 255, 255, 255))), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height-1);
                e.Graphics.DrawString(e.Header.Text, headerFont,
                    new SolidBrush(this.ForeColor),  e.Bounds.X+3, e.Bounds.Y+4);
            }
        }

        ~SkinableListView()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            this.BackColor = ThemeMgr.Instance.getColor(IKnownColors.contentBackcolor);
            this.ForeColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);
        }
    }
}
