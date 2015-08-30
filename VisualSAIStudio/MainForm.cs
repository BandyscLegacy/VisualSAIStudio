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
using Newtonsoft.Json;
using WeifenLuo.WinFormsUI.Docking.Themes;
using VisualSAIStudio.History;
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio
{
    public partial class MainForm : MetroForm.MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private DeserializeDockContent m_deserializeDockContent;
        PropertyWindow properties;
        ScratchWindow scratch;
        StartPage startPage;
        ToolWindow targets;
        ToolWindow actions;
        ToolWindow conditions;
        ToolWindow events;
        ErrorsWindow errors;

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            CreateWindows();
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (File.Exists("data/layout.xml"))
                dockPanel1.LoadFromXml("data/layout.xml", m_deserializeDockContent);
            else
                PlaceWindows();

            startPage = new StartPage();
            startPage.Show(dockPanel1);
            startPage.LoadRequest += startPage_LoadRequest;
            startPage.LoadDialogRequest += startPage_LoadDialogRequest;

            //only with new lib!
            ThemeMgr.Instance.SetColorTable(new WeifenLuo.WinFormsUI.Docking.Colors.Light());
            this.dockPanel1.Theme = ThemeMgr.Instance.DockPanelTheme;
            this.dockPanel1.Skin = ThemePanel.CreatePanelThemeValues();
            this.menuStrip1.Renderer = ThemeMgr.Instance.Renderer;
            this.menuStrip1.BackColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormBackground);
            this.menuStrip1.ForeColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormText);
            //no commit yet

            foreach(StorageType type in Enum.GetValues(typeof(StorageType)).Cast<StorageType>())
            {
                ToolStripMenuItem item = new ToolStripMenuItem(type.ToString());
                item.Tag = type;
                item.Click += item_Click;
                dBCToolStripMenuItem.DropDownItems.Add(item);
            }

            foreach(SAIType type in Enum.GetValues(typeof(SAIType)).Cast<SAIType>())
            {
                bool unsupported = Extensions.GetAttribute<UnsupportedAttribute>(type) != null;
                ToolStripMenuItem item = new ToolStripMenuItem(type.ToString() + (unsupported?" (unsupported)":""));
                item.Tag = type;
                item.Click += saiType_Click;
                setSAITypeToolStripMenuItem.DropDownItems.Add(item);
            }

            this.Location = Properties.Settings.Default.LastPos;
            this.Size =Properties.Settings.Default.LastSize;
        }

        private void saiType_Click(object sender, EventArgs e)
        {
            if (scratch!=null)
            {
                scratch.Type = (SAIType)((ToolStripMenuItem)sender).Tag;
                events.SetSAIType(scratch.Type);
            }
        }



        void startPage_LoadDialogRequest(object sender, EventArgs e)
        {
            ShowOpenDialog(((LoadRequestEventArgs)e).type);
        }

        void item_Click(object sender, EventArgs e)
        {
            ChooseRowFromDBForm db_window = new ChooseRowFromDBForm(((StorageType)((ToolStripMenuItem)sender).Tag));
            db_window.Show();
        }

        private void startPage_LoadRequest(object sender, EventArgs e)
        {
            NewSAIWindow(((LoadRequestEventArgs)e).entry, ((LoadRequestEventArgs)e).type);
        }

        private void NewSAIWindow(int entryorguid, SmartScripts.SAIType type)
        {
            scratch = new ScratchWindow();
            scratch.Show(dockPanel1);
            scratch.ElementSelected += this_callback;
            scratch.RequestWarnings += this_RequestWarnings;
            scratch.RequestNewSAIWindow += scratch_RequestNewSAIWindow;
            scratch.Type = type;
            if (entryorguid > 0)
            {
                scratch.LoadFromDB(entryorguid);
            }
        }

        void scratch_RequestNewSAIWindow(object sender, EventArgs e)
        {
            EventArgsRequestNewSAIWindow args = (EventArgsRequestNewSAIWindow)e;
            NewSAIWindow(args.entryorguid, args.type);
        }

        private void CreateWindows()
        {
            events = new ToolWindow("data/events.txt", "Events", SmartScripts.SmartType.SMART_EVENT);
            conditions = new ToolWindow("data/conditions.txt", "Conditions", SmartScripts.SmartType.SMART_CONDITION);
            targets = new ToolWindow("data/targets.txt", "Targets", SmartScripts.SmartType.SMART_TARGET);
            actions = new ToolWindow("data/actions.txt", "Actions", SmartScripts.SmartType.SMART_ACTION);
            properties = new PropertyWindow();
            errors = new ErrorsWindow();
            errors.WarningSelected += this_warningSelected;
        }

        private void PlaceWindows()
        {
            events.Show(dockPanel1, DockState.DockLeft);
            conditions.Show(events.Pane, DockAlignment.Bottom, 0.5);
            targets.Show(conditions.Pane, DockAlignment.Bottom, 0.5);
            actions.Show(dockPanel1, DockState.DockRight);
            properties.Show(actions.Pane, DockAlignment.Bottom, 0.6);
            errors.Show(dockPanel1, DockState.DockBottom);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(ErrorsWindow).ToString())
                return errors;
            else if (persistString == typeof(PropertyWindow).ToString())
                return properties;
            else if (persistString.Contains(typeof(ToolWindow).ToString()))
            {
                string[] parsedStrings = persistString.Split(new char[] { '/' });
                switch (parsedStrings[1])
                {
                    case "Events":
                        return events;
                    case "Actions":
                        return actions;
                    case "Targets":
                        return targets;
                    case "Conditions":
                        return conditions;
                }
            }
            return null;
        }

        private void this_RequestWarnings(object sender, EventArgs e)
        {
            errors.Clear();
            foreach (SmartEvent ev in scratch.GetEvents())
            {
                errors.AddWarnings(ev.Validate());
            }
        }

        private void this_warningSelected(object sender, EventArgs e)
        {

            if (!(e is WarningSelectedEventArgs))
                return;

            SmartElement elem = ((WarningSelectedEventArgs)e).element;

            if (elem is SmartAction || elem is SmartCondition)
            {
                elem.parent.setSelected(true);
                scratch.EnsureVisible(elem.parent);
            } 
            else
            {
                elem.setSelected(true);
                scratch.EnsureVisible(elem);
            }

        }

        private void this_callback(object sender, EventArgs e)
        {
            if (scratch.Selected().GetSelectedAction() != null)
            {
                properties.SetObject(new SmartScripts.SmartActionProperty(scratch.Selected().GetSelectedAction()));
            }
            else if (scratch.Selected().GetSelectedCondition() != null)
            {
                properties.SetObject(new VisualSAIStudio.SmartScripts.SmartConditionProperty(scratch.Selected().GetSelectedCondition()));
            }
            else
                properties.SetObject(new VisualSAIStudio.SmartScripts.SmartEventProperty(scratch.Selected()));
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            GenerateSQLOutput();   
        }

        public void GenerateSQLOutput()
        {
            scratch.GenerateSQLOutput();
        }

        private void loadFromDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOpenDialog(SmartScripts.SAIType.Creature);
        }

        private void ShowOpenDialog(SmartScripts.SAIType type)
        {
            SelectSAI c = new SelectSAI();
            c.ShowDialog();
            if (c.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                NewSAIWindow(c.Value, type);
                OpenedHistory.Instance.insert(new OpenedHistoryAction(type, c.Value, DB.GetInstance().GetString(StorageType.Creature, c.Value)));
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog(this);
        }

        private void eventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            events.Show(dockPanel1, DockState.DockLeft);
        }

        private void conditionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conditions.Show(dockPanel1, DockState.DockLeft);
        }

        private void actionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actions.Show(dockPanel1, DockState.DockRight);
        }

        private void targetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            targets.Show(dockPanel1, DockState.DockRight);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            properties.Show(dockPanel1, DockState.DockRight);
        }

        private void errorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errors.Show(dockPanel1, DockState.DockBottom);
        }
 
        private void detectConflictsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scratch.DetectConflicts();
        }

        private void validateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errors.Clear();
            foreach (SmartEvent ev in scratch.GetEvents())
                errors.AddWarnings(ev.Validate());
        }

        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveDocument == null)
                return;

            if (dockPanel1.ActiveDocument.DockHandler.Form is ScratchWindow)
            {
                scratch = (ScratchWindow)dockPanel1.ActiveDocument.DockHandler.Form;
                events.SetSAIType(scratch.Type);
            }
            
            errors.Clear();
            if (scratch == null)
                return;

            foreach (SmartEvent ev in scratch.GetEvents())
                errors.AddWarnings(ev.Validate());           
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dockPanel1.SaveAsXml("data/layout.xml");
            Properties.Settings.Default.LastPos = this.Location;
            Properties.Settings.Default.LastSize = this.Size;
            Properties.Settings.Default.Save();
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemeMgr.Instance.SetColorTable(new WeifenLuo.WinFormsUI.Docking.Colors.Light());
            this.BackColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormBackground);
            this.dockPanel1.Theme = ThemeMgr.Instance.DockPanelTheme;
            this.dockPanel1.Skin = ThemePanel.CreatePanelThemeValues();
            this.menuStrip1.Renderer = ThemeMgr.Instance.Renderer;
            this.menuStrip1.BackColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormBackground);
            this.menuStrip1.ForeColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormText);
        }

        private void darkToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ThemeMgr.Instance.SetColorTable(new WeifenLuo.WinFormsUI.Docking.Colors.Dark());
            this.BackColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormBackground);
            this.dockPanel1.Theme = ThemeMgr.Instance.DockPanelTheme;
            this.dockPanel1.Skin = ThemePanel.CreatePanelThemeValues();
            this.menuStrip1.Renderer = ThemeMgr.Instance.Renderer;
            this.menuStrip1.BackColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormBackground);
            this.menuStrip1.ForeColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormText);
        }

        private void generateSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.CodePreview code = new Forms.CodePreview(scratch.GenerateSQLOutput(), FastColoredTextBoxNS.Language.SQL);
            code.Show(dockPanel1);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



    }
}
