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
    public partial class ChooseRowFromDBForm : Form
    {
        public int Value;
        private Dictionary<int, string> data;
        public ChooseRowFromDBForm(StorageType storage)
        {
            InitializeComponent();
            data = DB.GetInstance().GetStringDictionary(storage);
            this.Text = "Select " + storage.ToString().ToLower();
        }

        private void ChooseRowFromDBForm_Load(object sender, EventArgs e)
        {
            LoadItems(null);
        }

        private void LoadItemsInt(string search)
        {
            items.ClearObjects();
            List<KeyValue> list = new List<KeyValue>();
            foreach (int id in data.Keys)
            {
                if (id.ToString().Contains(search))
                    list.Add(new KeyValue(id, data[id]));
            }

            items.AddObjects(list);
        }

        private void LoadItems(string search)
        {
            items.ClearObjects();
            List<KeyValue> list = new List<KeyValue>();
            foreach (int id in data.Keys)
            {
                if (search == null || data[id].ToLower().Contains(search))
                    list.Add(new KeyValue(id, data[id]));
            }

            items.AddObjects(list);
        }

        private void Filter()
        {
            int id;
            if (int.TryParse(txtSearch.Text, out id))
                LoadItemsInt(txtSearch.Text);
            else
                LoadItems(txtSearch.Text.ToLower());
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (items.SelectedObject==null)
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            else
            {
                Value = ((KeyValue)items.SelectedObject).key;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            this.Close();
        }

        private void items_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(sender, e);
        }
    }

    public struct KeyValue
    {
        public int key { get; set; }
        public string value { get; set; }
        public KeyValue(int key, string value)
            : this()
        {
            this.key = key;
            this.value = value;
        }
    }
}
