using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio
{
    public partial class ErrorsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public ErrorsWindow()
        {
            InitializeComponent();
        }

        int index = 1;
        public EventHandler WarningSelected = delegate { }; 

        private void ErrorsWindow_Load(object sender, EventArgs e)
        {

        }

        public void SetWarnings(List<Warning> warnings)
        {
            Clear();
            AddWarnings(warnings);
        }

        public void AddWarnings(List<Warning> warnings)
        {
            foreach (Warning warning in warnings)
            {
                ListViewItem lvi = new ListViewItem(index.ToString());
                lvi.SubItems.Add(GetWarningLocale(warning.warning));
                lvi.SubItems.Add(warning.description);
                lvi.SubItems.Add(warning.element.ToString());
                lvi.SubItems.Add(warning.element.name);
                lvi.Tag = warning.element;
                listView1.Items.Add(lvi);
                index++;
            }
            this.Text = "Warning List (" + listView1.Items.Count + ")";
        }

        private string GetWarningLocale(WarningType warningType)
        {
            switch (warningType)
            {
                case WarningType.INVALID_TARGET:
                    return "Invalid target";
                case WarningType.INVALID_PARAMETER:
                    return "Invalid parameter";
                case WarningType.INVALID_VALUE:
                    return "Invalid parameter value";
            }
            return "Other error "+warningType.ToString();
        }

        public void Clear()
        {
            listView1.Items.Clear();
            index = 1;
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                WarningSelectedEventArgs wsea = new WarningSelectedEventArgs();
                wsea.element = (SmartElement)listView1.SelectedItems[0].Tag;
                WarningSelected(sender, wsea);
            }

        }
    }
    public class WarningSelectedEventArgs : EventArgs
    {
        public SmartElement element { get; set; }
    }
}
