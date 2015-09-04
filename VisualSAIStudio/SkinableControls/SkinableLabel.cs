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
    class SkinableLabel : Label, IReloadable
    {
        public SkinableLabel() : base()
        {
            this.BackColor = Color.Transparent;
            ThemeMgr.Instance.RegisterControl(this);
        }

        ~SkinableLabel()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            this.ForeColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);
        }
    }
}
