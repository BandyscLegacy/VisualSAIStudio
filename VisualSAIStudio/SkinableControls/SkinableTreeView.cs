using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking.Colors;
using WeifenLuo.WinFormsUI.Docking.Themes;

namespace VisualSAIStudio.SkinableControls
{

    class SkinableTreeView : System.Windows.Forms.TreeView, WeifenLuo.WinFormsUI.Docking.IReloadable
    {
          // Event declaration
          public delegate void ScrollEventHandler(object sender, ScrollEventArgs e);
          public event ScrollEventHandler Scroll;
          private TreeNode mLastTop;
          // WM_VSCROLL message constants
          private const int WM_VSCROLL = 0x0115;
          private const int SB_THUMBTRACK = 5;
          private const int SB_ENDSCROLL = 8;

          protected override void WndProc(ref Message m) {
            // Trap the WM_VSCROLL message to generate the Scroll event
            base.WndProc(ref m);
            if (Scroll != null && m.Msg == WM_VSCROLL && this.TopNode != mLastTop) {
              int nfy = m.WParam.ToInt32() & 0xFFFF;
              if (nfy == SB_THUMBTRACK || nfy == SB_ENDSCROLL) {
                Scroll.Invoke(this, new ScrollEventArgs(this.TopNode, nfy == SB_THUMBTRACK));
                mLastTop = this.TopNode;
              }
            }
          }


        public class ScrollEventArgs {
            // Scroll event argument
            private TreeNode mTop;
            private bool mTracking;
            public ScrollEventArgs(TreeNode top, bool tracking) 
            {
                mTop = top;
                mTracking = tracking;
            }
            public TreeNode Top 
            {
                get { return mTop; }
            }
            public bool Tracking 
            {
                get { return mTracking; }
            }
        }
        
        public SkinableTreeView() : base()
        {
            ThemeMgr.Instance.RegisterControl(this);
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        ~SkinableTreeView()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        public void ReloadTheme()
        {
            this.BackColor = ThemeMgr.Instance.getColor(IKnownColors.contentBackcolor);
            this.ForeColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);
            this.Refresh();
        }
    }
}
