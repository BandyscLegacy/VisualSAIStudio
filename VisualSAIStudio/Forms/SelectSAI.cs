using BrightIdeasSoftware;
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
    public partial class SelectSAI : Form
    {
        public int Value { get; private set; }

        public SelectSAI()
        {
            InitializeComponent();
        }

        private void SelectSAI_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> data = DB.GetInstance().GetStringDictionary(StorageType.Creature);



            listView.ClearObjects();
            List<EntryNameSAI> list = new List<EntryNameSAI>();
            foreach (int id in data.Keys)
            {
                list.Add(new EntryNameSAI(id, data[id], DB.GetInstance().Contains(StorageType.CreatureEntryWithAI, id)?"Yes":""));
            }

            listView.AddObjects(list);
            listView.UseFiltering = true;
            listView.UseFilterIndicator = true;
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listView.ModelFilter = TextMatchFilter.Contains(listView, txtSearch.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (listView.SelectedObject == null)
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            else
            {
                Value = ((EntryNameSAI)listView.SelectedObject).entry;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            this.Close();
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(sender, e);
        }
    }

    public struct EntryNameSAI
    {
        public int entry { get; set; }
        public string name { get; set; }
        public string sai {get; set; }
        public EntryNameSAI(int entry, string name, string sai) : this()
        {
            this.entry = entry;
            this.name = name;
            this.sai = sai;
        }
    }

}
