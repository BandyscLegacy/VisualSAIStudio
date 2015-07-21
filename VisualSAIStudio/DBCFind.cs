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
    public partial class DBCFind : DockContent
    {

        Dictionary<int, string> data;
        public DBCFind(Dictionary<int, string> data)
        {
            InitializeComponent();
            this.data = data;
            
        }

        private void DBCFind_Load(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void RefreshAll()
        {

            lstView.Items.Clear();
            List<ListViewItem> items = new List<ListViewItem>();
            foreach (int key in data.Keys)
            {
                if (data[key].ToLower().Contains(textBox1.Text.ToLower()))
                {
                    ListViewItem lvi = new ListViewItem(Convert.ToString(key));
                    lvi.SubItems.Add(data[key]);
                    items.Add(lvi);
                }

            }
            lstView.Hide();
            lstView.BeginUpdate();
            lstView.Items.AddRange(items.ToArray());
            lstView.EndUpdate();
            lstView.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            RefreshAll();


            /*
            return;
                foreach (int key in data.Keys)
                {
                    if (data[key].ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        lvi = new ListViewItem(Convert.ToString(key));
                        lvi.SubItems.Add(data[key]);
                        lstView.Items.Add(lvi);
                    }

                }*/
        }
    }
}
