using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisualSAIStudio
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        PropertyWindow properties;
        ScratchWindow scratch;
        ToolWindow targets;
        ToolWindow actions;
        ToolWindow conditions;
        ToolWindow events;

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            LoadCustomEventsAndActions();
            dockPanel1.Theme = new WeifenLuo.WinFormsUI.Docking.VS2012LightTheme();
            //DBCFind dbcFind = new DBCFind(StringsDB.GetInstance().movies);
            scratch = new ScratchWindow();
            scratch.Show(dockPanel1);

            events = new ToolWindow("data/events.txt", "Events");
            events.Show(dockPanel1, DockState.DockLeft);

            conditions = new ToolWindow("data/conditions.txt", "Conditions");
            conditions.Show(events.Pane, DockAlignment.Bottom, 0.5);

            actions = new ToolWindow("data/actions.txt", "Actions");
            actions.Show(dockPanel1, DockState.DockRight);

            targets = new ToolWindow("data/targets.txt", "Targets");
            targets.Show(actions.Pane, DockAlignment.Bottom, 0.8);

            properties = new PropertyWindow();
            properties.Show(targets.Pane, DockAlignment.Bottom, 0.6);

            scratch.ElementSelected += this_callback;
        }

        private void this_callback(object sender, EventArgs e)
        {
            if (scratch.Selected().GetSelectedAction() != null)
            {
                properties.SetObject(new VisualSAIStudio.SmartScripts.SmartActionProperty(scratch.Selected().GetSelectedAction()));
            }
            else
                properties.SetObject(new VisualSAIStudio.SmartScripts.SmartEventProperty(scratch.Selected()));
        }

        private void LoadCustomEventsAndActions()
        {
            string data;
            if (File.Exists("data/custom_actions.json"))
            {
                data = File.ReadAllText("data/custom_actions.json");
                List<SmartGenericJSONData> smart_generics = JsonConvert.DeserializeObject<List<SmartGenericJSONData>>(data);
                smart_generics.ForEach(e => ExtendedFactories.AddAction(e));
            }
            if (File.Exists("data/custom_events.json"))
            {
                data = File.ReadAllText("data/custom_events.json");
                List<SmartGenericJSONData> smart_generics = JsonConvert.DeserializeObject<List<SmartGenericJSONData>>(data);
                smart_generics.ForEach(e => ExtendedFactories.AddEvent(e));
            }
        }


        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            GenerateSQLOutput();   
        }

        public void GenerateSQLOutput()
        {
            txtSQL.Text = scratch.GenerateSQLOutput();
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == Keys.Return)
               scratch.LoadFromDB(Convert.ToInt32(textBox8.Text));
        }

        private void loadFromDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScratchWindow w = new ScratchWindow();
            w.Show(dockPanel1);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog(this);
        }

        private void eventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            events = new ToolWindow("data/events.txt", "Events");
            events.Show(dockPanel1, DockState.DockLeft);
        }

        private void conditionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conditions = new ToolWindow("data/conditions.txt", "Conditions");
            conditions.Show(dockPanel1, DockState.DockLeft);
        }

        private void actionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actions = new ToolWindow("data/actions.txt", "Actions");
            actions.Show(dockPanel1, DockState.DockRight);
        }

        private void targetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            targets = new ToolWindow("data/targets.txt", "Targets");
            targets.Show(dockPanel1, DockState.DockRight);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            properties = new PropertyWindow();
            properties.Show(dockPanel1, DockState.DockRight);
        }
    }
}
