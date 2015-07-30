using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSAIStudio.SmartScripts;
using WeifenLuo.WinFormsUI.Docking;

namespace VisualSAIStudio
{
    public partial class ToolWindow : DockContent
    {
        TreeNode collection = new TreeNode();

        SAIType saitype;

        public ToolWindow(String file, String title)
        {
            InitializeComponent();
            this.Text = title;
            this.HideOnClose = true;
            LoadSmartFromFile(file);
            Reload();
            saitype = SAIType.Gameobject;
        }
        public void SetSAIType(SAIType type)
        {
            this.saitype = type;
            Reload();
        }

        protected override string GetPersistString()
        {
            return this.GetType() + "/" + this.Text;
        }



        //@TODO: to rewrite >.<
        private void Reload()
        {
            List<string> expanded = new List<string>();
            foreach (TreeNode p in treeView.Nodes)
            {
                if (p.IsExpanded)
                    expanded.Add(p.Text);
            }
            treeView.Nodes.Clear();
            foreach (TreeNode parent in collection.Nodes)
            {
                bool add = false;
                TreeNode parent2 = new TreeNode();
                parent2.Text = parent.Text;
                parent2.Tag = parent.Tag;
                foreach (TreeNode child in parent.Nodes)
                {
                    if ((child.Text.ToLower().Contains(textBox.Text.ToLower()) ||
                       ( child.Tag != null && child.Tag.ToString().ToLower().Contains(textBox.Text.ToLower()))) &&
                            SmartFactory.GetInstance().IsEventValidType(child.Tag.ToString(), saitype))
                    {
                        add = true;
                        parent2.Nodes.Add(child);
                    }
                }

                if ((parent2.Text.ToLower().Contains(textBox.Text.ToLower()) && parent2.Nodes.Count > 0) ||
                    (parent2.Tag!= null &&
                    parent2.Tag.ToString().ToLower().Contains(textBox.Text.ToLower()) &&
                    SmartFactory.GetInstance().IsEventValidType(parent2.Tag.ToString(), saitype)
                    ))
                {
                    add = true;
                }

                if (add)
                {
                    treeView.Nodes.Add(parent2);
                    if (expanded.Contains(parent.Text))
                        parent2.Expand();
                }
            }
        }

        private void LoadSmartFromFile(string file)
        {
            TreeNode parentNode = null;
            foreach (string line in File.ReadLines(file))
            {
                TreeNode new_node = GetNewChildNode(line);
                if (line.IndexOf(" ") == 0)
                    parentNode.Nodes.Add(new_node);
                else
                {
                    parentNode = new_node;
                    collection.Nodes.Add(parentNode);
                }
            }
        }

        private TreeNode GetNewChildNode(string line)
        {
            int comma = line.IndexOf(",");
            TreeNode child = null;
            if (comma > 0)
            {
                child = new TreeNode(line.Substring(0, comma));
                child.Tag = line.Substring(comma + 1);
            }
            else
                child = new TreeNode(line);

            return child;
        }

        private void ToolWindow_Load(object sender, EventArgs e)
        {

        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            treeView.SelectedNode = treeView.GetNodeAt(e.X, e.Y);
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (treeView.SelectedNode != null && ((string)treeView.SelectedNode.Tag) != null)
                DoDragDrop((string)treeView.SelectedNode.Tag, DragDropEffects.All);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            Reload();
        }
    }
}
