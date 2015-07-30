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
        public SelectSAI()
        {
            InitializeComponent();
        }

        private void SelectSAI_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> data = StringsDB.GetInstance().GetDictionary(StorageType.Creature);
            Dictionary<int, List<GuidEntry>> data2 = StringsDB.GetInstance().EntryToGuidCreature;
            List<GuidEntry> asa = new List<GuidEntry>();
            foreach (int key in data.Keys)
            {
                //if (key > 5)
                ///    break;
                GuidEntry xax = new GuidEntry();
                xax.id = key;
                xax.name = data[key];
                asa.Add(xax);
            }
            treeView1.Roots = asa;
            this.treeView1.CanExpandGetter = delegate(object x)
            {
                return (((GuidEntry)x).name!=null) && data2.ContainsKey(((GuidEntry)x).id);
            };
            this.treeView1.ChildrenGetter = delegate(object x)
            {
                GuidEntry o = (GuidEntry)x;
                return data2[o.id];
            };
            
            /*Func<int, int> getId = (x => x);//x.EmployeeId);
            Func<int, int?> getParentId = (x => (int?)null);//x.ManagerId);
            Func<int, string> getDisplayName = (x => data[x]);

            // Load items into TreeViewFast
            treeView1.LoadItems(data.Keys.ToList(), getId, getParentId, getDisplayName);


            Dictionary<int, List<int>> data2 = StringsDB.GetInstance().EntryToGuidCreature;
            foreach (int key in data2.Keys)
            {
                Func<int, int> getId2 = (x => x);//x.EmployeeId);
                Func<int, int?> getParentId2 = (x => key);//x.ManagerId);
                Func<int, string> getDisplayName2 = (x => x.ToString());

                // Load items into TreeViewFast
                treeView1.LoadItems<int>(data2[key], getId2, getParentId2, getDisplayName2);
            }*/


        }

        private void treeView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            int o;
            treeView1.ResetColumnFiltering();
            //treeView1.ModelFilter = TextMatchFilter.Contains(treeView1, "6548159");

            
            switch (cmbType.SelectedIndex)
            {
                case 0:
                    treeView1.ModelFilter = new FilterByName(txtSearch.Text.ToLower());
                    break;
                case 1:
                    if (int.TryParse(txtSearch.Text, out o))
                     treeView1.ModelFilter = new FilterByEntry(o);
                    
                    break;
                case 2:
                    if (int.TryParse(txtSearch.Text, out o))
                        treeView1.ModelFilter = new FilterByGUID(o);
                    
                    break;
            }
        }
    }

    public class FilterByName : IModelFilter
    {
        private string p;

        public FilterByName(string p)
        {
            this.p = p;
        }
        public bool Filter(object modelObject)
        {
            GuidEntry g = ((GuidEntry)modelObject);
            if (g.name==null)
                return false;
            return g.name.ToLower().Contains(p);
        }
    }

    public class FilterByEntry : IModelFilter
    {
        private int entry;
        public FilterByEntry(int entry)
        {
            this.entry = entry;
        }
        public bool Filter(object modelObject)
        {
            GuidEntry g = ((GuidEntry)modelObject);
            if (g.name == null)
                return true;
            return g.id == entry;
        }
    }
    public class FilterByGUID : IModelFilter
    {
        private int entry;
        public FilterByGUID(int entry)
        {
            this.entry = entry;
        }
        public bool Filter(object modelObject)
        {
            GuidEntry g = ((GuidEntry)modelObject);
            return g.id == entry;
        }
    }

    public class XYZZ : IModelFilter
    {
        public bool Filter(object modelObject)
        {
            GuidEntry g = ((GuidEntry)modelObject);
            return g.id == 6548159;
        }
    }
}
