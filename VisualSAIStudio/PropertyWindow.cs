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

namespace VisualSAIStudio
{
    public partial class PropertyWindow : DockContent
    {
        public PropertyWindow()
        {
            InitializeComponent();
        }

        private void PropertyWindow_Load(object sender, EventArgs e)
        {

        }

        public void SetObject(object ob)
        {
            propertyGrid1.SelectedObject = ob;
        }
    }
}
