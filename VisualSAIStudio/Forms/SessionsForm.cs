using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio.Forms
{
    public partial class SessionsForm : MetroForm.MetroForm
    {
        public SessionsForm()
        {
            InitializeComponent();
            ReloadTheme();
            toolBox4.ReloadTheme();
            contextMenuStrip.ReloadTheme();
        }

        private void SessionsForm_Load(object sender, EventArgs e)
        {
            foreach (Sessions.Session session in Sessions.SessionManager.GetInstance())
            {
                SkinableControls.ToolBoxNode session_node = new SkinableControls.ToolBoxNode(session.title);
                session_node.Tag = session;
                foreach (Sessions.OpenedScratch scratch in session.list)
                {
                    String name = scratch.type.ToString() + " " + DB.GetInstance().GetString(DB.GetInstance().StorageForType(scratch.type, scratch.entry < 0), scratch.entry);
                    SkinableControls.ToolBoxNode item = new SkinableControls.ToolBoxNode(name);
                    item.Tag = session;
                    item.Tag2 = scratch;
                    session_node.Nodes.Add(item);
                }
                toolBox4.Nodes.Add(session_node);
            }
        }

        private void toolBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (toolBox4.SelectedNode.Nodes.Count == 0)
                    renameToolStripMenuItem.Enabled = false;
                else
                    renameToolStripMenuItem.Enabled = true;
                contextMenuStrip.Show(toolBox4.PointToScreen(new Point(e.X, e.Y)));
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolBox4.SelectedNode == null)
                return;

            Sessions.Session session = (Sessions.Session)toolBox4.SelectedNode.Tag;
            if (toolBox4.SelectedNode.Tag2 == null)
            {
                Sessions.SessionManager.GetInstance().Remove(session);
                toolBox4.Nodes.Remove(toolBox4.SelectedNode);
            }
            else
            {
                Sessions.SessionManager.GetInstance().RemoveSingle(session, (Sessions.OpenedScratch)toolBox4.SelectedNode.Tag2);
                toolBox4.SelectedNode.Parent.Nodes.Remove(toolBox4.SelectedNode);
            }

        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sessions.Session session = (Sessions.Session)toolBox4.SelectedNode.Tag;
            string input = Microsoft.VisualBasic.Interaction.InputBox("New name", "New name", session.title, -1, -1);
            if (!String.IsNullOrEmpty(input))
            {
                toolBox4.SelectedNode.Text = input;
                toolBox4.Refresh();
                Sessions.SessionManager.GetInstance().Rename(session, input);
            }
        }
    }
}
