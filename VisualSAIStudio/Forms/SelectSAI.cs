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
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio
{
    public partial class SelectSAI : Form
    {
        public int Value { get; private set; }

        private SAIType type;

        public SelectSAI(SAIType saitype)
        {
            this.type = saitype;
            InitializeComponent();
        }

        private StorageType StorageWithSAIForType()
        {
            switch (type)
            {
                case SAIType.Creature:
                    if (SearchGuid())
                        return StorageType.CreatureGuidWithSAI;
                    return StorageType.CreatureEntryWithAI;
                case SAIType.Gameobject:
                    if (SearchGuid())
                        return StorageType.GameObjectGuidWithAI;
                    return StorageType.GameObjectEntryWithAI;
                case SAIType.AreaTrigger:
                    return StorageType.AreaTriggerWithSAI;
            }
            return StorageType.CreatureEntryWithAI;
        }

        private void SelectSAI_Load(object sender, EventArgs e)
        {
            LoadSAI();
            listView.UseFiltering = true;
            listView.UseFilterIndicator = true;
        }

        private void LoadSAI()
        {
            Dictionary<int, string> data = DB.GetInstance().GetStringDictionary(DB.GetInstance().StorageForType(type, SearchGuid()));
            listView.ClearObjects();
            List<EntryNameSAI> list = new List<EntryNameSAI>();
            foreach (int id in data.Keys)
            {
                list.Add(new EntryNameSAI(id, data[id], GetHasSai(id)));
            }
            listView.AddObjects(list);
        }

        private string GetHasSai(int id)
        {
            if (type == SAIType.TimedActionList)
                return "Yes";
            return DB.GetInstance().Contains(StorageWithSAIForType(), id) ? "Yes" : "";
        }

        private bool SearchGuid()
        {
            return cmbType.SelectedIndex ==1;
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
                if (SearchGuid())
                    Value = -Value;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            this.Close();
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(sender, e);
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSAI();
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
