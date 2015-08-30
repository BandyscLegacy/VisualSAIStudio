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
using VisualSAIStudio.SkinableControls;
using VisualSAIStudio.SmartScripts;
using WeifenLuo.WinFormsUI.Docking;

namespace VisualSAIStudio
{
    public partial class ToolWindow : DockContent
    {
        SmartType type;

        private StringFilter stringFilter;
        private SAITypeFilter saiTypeFilter;
        public ToolWindow(String file, String title, SmartType type)
        {
            stringFilter = new StringFilter();
            saiTypeFilter = new SAITypeFilter();
            InitializeComponent();
            this.Text = title;
            this.textBox.Placeholder = "Search " + title;
            this.HideOnClose = true;
            this.type = type;
            label1.Filters.Add(stringFilter);
            label1.Filters.Add(saiTypeFilter);
            LoadSmartFromFile(file);
        }

        public void SetSAIType(SAIType type)
        {
            saiTypeFilter.Type = type;
        }

        protected override string GetPersistString()
        {
            return this.GetType() + "/" + this.Text;
        }

        private void LoadSmartFromFile(string file)
        {
            ToolBoxNode parentNode = null;
            foreach (string line in File.ReadLines(file))
            {
                ToolBoxNode new_node = GetNewToolChildNode(line);
                if (line.IndexOf(" ") == 0)
                    parentNode.Nodes.Add(new_node);
                else
                {
                    parentNode = new_node;
                    label1.Nodes.Add(parentNode);
                }
            }
        }

        private ToolBoxNode GetNewToolChildNode(string line)
        {
            int comma = line.IndexOf(",");
            ToolBoxNode child = null;
            if (comma > 0)
            {
                child = new ToolBoxNode(line.Substring(0, comma));
                child.Tag = line.Substring(comma + 1);
            }
            else
            {
                child = new ToolBoxNode(line);
                child.AlwaysExpand = true;
            }

            return child;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            stringFilter.Text = textBox.Text;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (label1.SelectedNode != null && ((string)label1.SelectedNode.Tag) != null)
                DoDragDrop((string)label1.SelectedNode.Tag, DragDropEffects.All);
        }

        private void ToolWindow_Load(object sender, EventArgs e)
        {

        }

    }

    public class SAITypeFilter : IFilter
    {
        private SAIType _type;
        public SAIType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                Request();
            }
        }
        public override bool Show(ToolBoxNode node)
        {
            return (node.Tag != null && SmartFactory.GetInstance().IsEventValidType(node.Tag.ToString(), Type));
        }
    }
}
