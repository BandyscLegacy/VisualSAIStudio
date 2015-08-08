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
    class SkinablePropertyGrid : PropertyGrid, IReloadable
    {

        public SkinablePropertyGrid()
        {
            ThemeMgr.Instance.RegisterControl(this);
        }

        ~SkinablePropertyGrid()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            this.BackColor = ThemeMgr.Instance.getColor(IKnownColors.FormBackground);
            this.CategorySplitterColor = ThemeMgr.Instance.getColor(IKnownColors.FormBackground);
            this.CategoryForeColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);


            this.HelpBackColor = ThemeMgr.Instance.getColor(IKnownColors.contentBackcolor);
            this.HelpBorderColor = ThemeMgr.Instance.getColor(IKnownColors.contentBackcolor);
            this.HelpForeColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);


            this.LineColor = ThemeMgr.Instance.getColor(IKnownColors.FormBackground);

            this.ViewBackColor = ThemeMgr.Instance.getColor(IKnownColors.contentBackcolor);
            this.ViewBorderColor = ThemeMgr.Instance.getColor(IKnownColors.contentBackcolor);
            this.ViewForeColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);

        }
    }
}
