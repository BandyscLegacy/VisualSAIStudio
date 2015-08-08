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

namespace VisualSAIStudio.Forms
{
    public partial class CodePreview : DockContent
    {
        public CodePreview(string code, FastColoredTextBoxNS.Language lang)
        {
            InitializeComponent();
            text.Text = code;
            text.Language = lang;
        }

        private void SQLPreview_Load(object sender, EventArgs e)
        {

        }
    }
}
