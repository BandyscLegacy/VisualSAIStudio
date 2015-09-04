using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI.Docking.Colors;
using WeifenLuo.WinFormsUI.Docking.Themes;

namespace VisualSAIStudio.SkinableControls
{
    class SkinableContextMenuStrip : ContextMenuStrip, IReloadable
    {
        public SkinableContextMenuStrip() : base()
        {
            ThemeMgr.Instance.RegisterControl(this);
            ReloadTheme();
        }

        public SkinableContextMenuStrip(System.ComponentModel.IContainer container) : base(container)
        {
            ThemeMgr.Instance.RegisterControl(this);
            ReloadTheme();
        }

        ~SkinableContextMenuStrip()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            this.Renderer = ThemeMgr.Instance.Renderer;
            this.BackColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormBackground);
            this.ForeColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormText);
        }
    }
}
