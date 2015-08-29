using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI.Docking.Themes;

namespace VisualSAIStudio.SkinableControls
{
    public partial class ToolBox : UserControl, IReloadable
    {
        public ObservableCollection<ToolBoxNode> Nodes { get; private set; }
        public int Indent {get; set;}
        public bool DrawTag { get; set; }
        public ToolBoxNode SelectedNode { get; set; }
        public Padding ItemPadding { get; set; }

        private Brush SelectionBGBrush;
        private Brush HoverBGBrush;
        private Brush SelectionForeColorBrush;
        private Brush ForeColorBrush;
        private string _Filter;
        public string Filter 
        {
            get
            {
                return _Filter;
            }
            set
            {
                _Filter = value;
                foreach (ToolBoxNode node in Nodes)
                    node.Filter(value);
                this.Refresh();
            }
        }

        private ToolBoxNode HoverNode { get; set; }
        private ToolTip toolTip;

        public ToolBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            Nodes = new ObservableCollection<ToolBoxNode>();
            Nodes.CollectionChanged += Nodes_CollectionChanged;
            Indent = 17;
            ItemPadding = new Padding(3);
            toolTip = new ToolTip();
            ThemeMgr.Instance.RegisterControl(this);
        }

        ~ToolBox()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        void Nodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Refresh();
        }

        private void ToolBox_Load(object sender, EventArgs e)
        {

        }

        protected virtual Rectangle OnItemDraw(ICollection parent, int level, Graphics graphics, int y)
        {
            int start = y;
            foreach (ToolBoxNode node in parent)
            {
                if (!node.IsVisible)
                    continue;

                int start_y = y;
                SizeF measure = graphics.MeasureString(node.Text, Font);
                measure.Height += ItemPadding.Vertical;

                if (node == SelectedNode)
                {
                    graphics.FillRectangle(SelectionBGBrush, 0, y, this.Width, measure.Height);
                }
                else if (node == HoverNode)
                {
                    graphics.FillRectangle(HoverBGBrush, 0, y, this.Width, measure.Height);
                }

                node.Level = Indent;
                y += ItemPadding.Top;
                graphics.DrawString(node.Text, Font, (node == SelectedNode)?SelectionForeColorBrush:ForeColorBrush, level * Indent+10, y);
                if (DrawTag && node.Tag!= null)
                    graphics.DrawString(node.Tag.ToString().Replace("SMART_","").ToLower(), Font, Brushes.Gray, level * Indent + 20 + measure.Width, y);
                

                if (node.Nodes.Count > 0)
                {
                    Pen pen = new Pen((node == SelectedNode) ? SelectionForeColorBrush : ForeColorBrush);
                    if (node.IsExpanded)
                    {
                        Point[] points = new Point[3];
                        points[0] = new Point(level * Indent + 3, y + 8);
                        points[1] = new Point(level * Indent +7, y + 8);
                        points[2] = new Point(level * Indent +7, y + 4);
                        graphics.DrawPolygon(pen, points);
                        graphics.FillPolygon((node == SelectedNode) ? SelectionForeColorBrush : ForeColorBrush, points);
                    }
                    else
                    {
                        Point[] points = new Point[3];
                        points[0] = new Point(level * Indent +4, y + 3);
                        points[1] = new Point(level * Indent +7, y + 6);
                        points[2] = new Point(level * Indent +4, y + 9);
                        graphics.DrawPolygon(pen, points);
                    }
                }
                
                
                y += (int)measure.Height-ItemPadding.Top;


                if (node.IsExpanded)
                    y+= OnItemDraw(node.Nodes, level + 1, graphics, y).Height;

                node.Bounds = new Rectangle(0, start_y, this.Width, y - start_y);
            }
            return new Rectangle(0, y, 0, y-start);
        }

        private void ToolBox_Paint(object sender, PaintEventArgs e)
        {
            int startY = 5 - vScroll.Value;
            int finishY = OnItemDraw(Nodes, 1, e.Graphics, startY).Top;


            vScroll.Maximum = Math.Max(0, finishY - this.Height + Math.Abs(startY) + 5);
            if (vScroll.Maximum == 0)
                vScroll.Visible = false;
            else
                vScroll.Visible = true;
        }

        public void SelectNodeAt(int x, int y)
        {
            SelectNodeAt(x,y, Nodes);
        }

        private void SelectNodeAt(int x, int y, ICollection nodes)
        {
            ToolBoxNode node = GetNodeAt(x, y, nodes);
            if (node == null)
                return;
            SelectedNode = node;
            this.Refresh();
        }

        public ToolBoxNode GetNodeAt(int x, int y)
        {
            return GetNodeAt(x, y, Nodes);
        }

        private ToolBoxNode GetNodeAt(int x, int y, ICollection nodes)
        {
            foreach (ToolBoxNode node in nodes)
            {
                if (node.Bounds.Contains(x, y) && node.IsVisible)
                {
                    ToolBoxNode potential_node = GetNodeAt(x, y, node.Nodes);
                    if (potential_node == null)
                        return node;
                    else
                        return potential_node;
                }
            }
            return null;
        }

        private void ToolBox_MouseDown(object sender, MouseEventArgs e)
        {
            SelectNodeAt(e.X, e.Y);
            if (SelectedNode == null)
                return;

            if (SelectedNode.Nodes.Count > 0 && (SelectedNode.AlwaysExpand || e.X < SelectedNode.Level * Indent - 10))
            {
                if (SelectedNode.IsExpanded)
                    SelectedNode.Collapse();
                else
                    SelectedNode.Expand();
            }
            this.Refresh();
        }

        public void ReloadTheme()
        {
            this.BackColor = ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.contentBackcolor);
            SelectionBGBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.ListSelectionBackColor));
            HoverBGBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.ListSelectionHoverColor));
            SelectionForeColorBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.ListSelectionForeColor));
            ForeColorBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.FormText));
        }

        private void ToolBox_MouseMove(object sender, MouseEventArgs e)
        {
            ToolBoxNode node = GetNodeAt(e.X, e.Y);
            if (node == null || node.Tag == null)
            {
                toolTip.Hide(this);
                HoverNode = node;
                this.Refresh();
                return;
            }

            if (HoverNode == node)
                return;

            HoverNode = node;
            this.Refresh();
            toolTip.ToolTipTitle = node.Text;
            toolTip.Show(node.Tag.ToString(), this, 0, node.Bounds.Bottom+15); 
        }

        private void ToolBox_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(this);
            HoverNode = null;
            this.Refresh();
        }

        private void vScroll_Scroll(object sender, ScrollEventArgs e)
        {
            this.Refresh();
        }

        private void vScroll_ValueChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

    }

    public class ToolBoxNode
    {
        public bool IsExpanded { get; set; }

        public bool AlwaysExpand { get; set; }

        public bool IsVisible { get; set; }

        public string Text { get; set; }

        public object Tag { get; set; }

        public Rectangle Bounds {get; set;}

        public ObservableCollection<ToolBoxNode> Nodes { get; private set; }

        public ToolBoxNode()
        {
            Nodes = new ObservableCollection<ToolBoxNode>();
            IsVisible = true;
        }

        public ToolBoxNode(String text) : this()
        {
            this.Text = text;
        }

        public void Collapse()
        {
            IsExpanded = false;
        }

        public void Expand()
        {
            IsExpanded = true;
        }

        public bool Filter(string filter)
        {
            if (filter!=null)
                filter = filter.ToLower();
            if (filter == null || this.Text.ToLower().Contains(filter) || (this.Tag != null && this.Tag.ToString().ToLower().Contains(filter)))
            {
                foreach (ToolBoxNode node in Nodes)
                    node.IsVisible = true;
                this.IsVisible = true;

                return true;
            }
            else
            {
                bool show = false;
                foreach (ToolBoxNode node in Nodes)
                    if (node.Filter(filter))
                        show = true;

                this.IsVisible = show;
                return show;
            }

        }


        public int Level { get; set; }
    }
}
