using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio
{
    public partial class Scratch : UserControl
    {
        public event EventHandler ElementSelected;

        public DrawableElement selectedElement;

        private Point mouse;
        private Point mouse_start;
        private bool mousedown;
        private bool dragging;

        private Brush brush;
        private Pen pen;
        private Font font;


        public DrawableElementsCollection elements;

        public float scale = 1;

        public Scratch()
        {
            InitializeComponent();

            brush = Brushes.Black;
            pen = new Pen(brush);
            font = new Font("Tahoma", 9);

            SetElements(new DrawableElementsCollection());
            this.MouseWheel += new MouseEventHandler(this.mouse_wheel_event);
        }

        public void SetElements(DrawableElementsCollection collection)
        {
            this.elements = collection;
            collection.ElementsChanged += collection_ElementsChanged;
        }

        void collection_ElementsChanged(object sender, EventArgs e)
        {
            this.Refresh();
            foreach (DrawableElement element in elements)
            {
                element.RequestUpdate -= element.RequestUpdate;
                element.RequestUpdate += ObjectRequestedUpdate;
            }
        }

        private void ObjectRequestedUpdate(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void mouse_wheel_event(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                vScrollBar.Value = Math.Min(vScrollBar.Maximum, vScrollBar.Value + 30);
            else
                vScrollBar.Value = Math.Max(vScrollBar.Minimum, vScrollBar.Value - 30);
        }

        private void Scratch_Paint(object sender, PaintEventArgs e)
        {
            DrawElements(e.Graphics);
        }

        private void DrawElements(Graphics graphics)
        {
            int curx = 5 - hScrollBar.Value;
            int cury = 5 - vScrollBar.Value;
            int startY = vScrollBar.Value;
            int maxX = 0;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < elements.Count; ++i)
            {
                DrawableElement e = elements.Get(i);
                Size size = e.Draw(graphics, curx, cury, this.Width, this.Height, brush, pen, font);
                cury += size.Height + 10;
                if (maxX < size.Width)
                    maxX = size.Width + 10;
            }

            if (dragging && selectedElement != null)
            {
                selectedElement.Draw(graphics, mouse.X, mouse.Y, this.Width, this.Height, brush, pen, font, false);
            }

            if (dragging)
            {
                foreach (DrawableElement e in elements)
                {
                    if (mouse.Y > e.rect.Bottom - 10 && mouse.Y < e.rect.Bottom + 10)
                    {
                        graphics.DrawLine(pen, 5, e.rect.Bottom + 5, this.Width - 10, e.rect.Bottom + 5);
                        break;
                    }
                }
            }
            vScrollBar.Maximum = Math.Max(0, cury - this.Height + startY+20);
            hScrollBar.Maximum = Math.Max(0, maxX - this.Width  + 20);
        }

        private void DragDown()
        {
            for (int i = 0; i < elements.Count; ++i)
            {
                DrawableElement element = elements.Get(i);
                if (element!= selectedElement && mouse.Y > element.rect.Bottom - 10 && mouse.Y < element.rect.Bottom + 10)
                {
                    elements.Remove(selectedElement);
                    elements.Insert(selectedElement, i);
                    break;
                }
            }
        }
        
        private void Scratch_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.X;
            mouse.Y = e.Y;
            if (e.Button == MouseButtons.Left && mouse.distance(mouse_start)>10)
                dragging = true;
            this.Refresh();
        }

        private void Scratch_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        private void Scratch_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;

            mouse_start.X = e.X;
            mouse_start.Y = e.Y;

            if (selectedElement != null)
                selectedElement.setSelected(false);
            selectedElement = null;

            foreach(DrawableElement element in elements)
            {
                if (element.contains(e.X, e.Y))
                {
                    element.setSelected(new Point(e.X, e.Y));
                    selectedElement = element;
                    ElementSelected(this, new EventArgs());
                }
            }
        }

        private void Scratch_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
            if (dragging)
            {
                DragDown();
                dragging = false;
            }
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.Refresh();
        }

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Scratch_VisibleChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.Refresh();
        }

        private void hScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }


        public void EnsureVisible(DrawableElement drawableElement)
        {
            if (drawableElement.rect.Top < 0 || drawableElement.rect.Top > this.Height)
                vScrollBar.Value += drawableElement.rect.Top ;
        }
    }
    public static class MyExtensionMethods
    {
        public static int distance(this Point p1, Point p2)
        {
            return (int)Math.Sqrt(Math.Pow(2, (p1.X - p2.X)) + Math.Pow(2, (p1.Y - p2.Y)));
        }
    }

}
