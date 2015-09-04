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
    class SkinableWebBrowser : WebBrowser, IReloadable
    {
        public SkinableWebBrowser() : base()
        {
            ThemeMgr.Instance.RegisterControl(this);
            this.DocumentCompleted += SkinableWebBrowser_DocumentCompleted;
        }

        void SkinableWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ReloadTheme();
        }

        ~SkinableWebBrowser()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            if (this.Document != null)
            {
                this.Document.BackColor = ThemeMgr.Instance.getColor(IKnownColors.contentBackcolor);
                this.Document.ForeColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);
            }
        }
    }
}
