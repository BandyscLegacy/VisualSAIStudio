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
        public event EventHandler ElementSelected = delegate { };

        public DrawableElement selectedElement;

        private Point mouse;
        private Point mouse_start;
        private bool mousedown;
        private bool dragging;

        protected Brush brush;
        protected Pen pen;
        protected Font font;
        protected Font mini_font;


        public DrawableElementsCollection elements;

        public float scale = 1;

        public Scratch()
        {
            InitializeComponent();

            brush = new SolidBrush(Color.FromArgb(20, 20, 20));
            pen = new Pen(brush);
            font = new Font("Tahoma", 9);
            mini_font = new Font(font.FontFamily, 8);

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
                Size size = e.Draw(graphics, curx, cury, this.Width + hScrollBar.Maximum, this.Height, brush, pen, font, mini_font);
                cury += size.Height + 10;
                if (maxX < size.Width)
                    maxX = size.Width + 10;
            }

            if (dragging && selectedElement != null)
            {
                selectedElement.Draw(graphics, mouse.X, mouse.Y, this.Width, this.Height, brush, pen, font, mini_font, false);
                if (elements.Count>1)
                {
                    int index = elements.GetInsertIndexFromPos(mouse.X, mouse.Y);
                    int y = (index == elements.Count ? elements[index - 1].rect.Bottom : elements[index].rect.Top);
                    graphics.DrawLine(pen, 5, y, this.Width - 10, y);
                }
            }


            vScrollBar.Maximum = Math.Max(0, cury - this.Height + startY+20);
            if (startY > 0)
            {
                if (cury+30 < this.Height)
                {
                    vScrollBar.Value = Math.Max(0, startY - this.Height - cury);
                    this.Refresh();
                }
            }
            hScrollBar.Maximum = Math.Max(0, maxX - this.Width  + 20);
            if (vScrollBar.Maximum == 0)
                vScrollBar.Visible = false;
            else
                vScrollBar.Visible = true;

            if (hScrollBar.Maximum == 0)
                hScrollBar.Visible = false;
            else
                hScrollBar.Visible = true;
        }

        private void DragDown()
        {
            int index = elements.GetInsertIndexFromPos(mouse.X, mouse.Y);
            if (elements.IndexOf(selectedElement) < index)
                index--;
            elements.Remove(selectedElement);
            elements.Insert(selectedElement, index);

        }
        
        private void Scratch_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.X;
            mouse.Y = e.Y;

            if (selectedElement != null)
                selectedElement.MouseMove(sender, e);

            if (!dragging && e.Button == MouseButtons.Left && selectedElement !=null && selectedElement.IsDraggableArea(e.X, e.Y) && mouse.distance(mouse_start)>10)
                dragging = true;
            this.Refresh();
        }

        private void Scratch_Load(object sender, EventArgs e)
        {
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


            selectedElement = elements.ElementAt(e.X, e.Y);

            if (selectedElement != null) {
                selectedElement.setSelected(new Point(e.X, e.Y));
                selectedElement.MouseDown(sender, e);
                ElementSelected(this, new EventArgs());
            }
        }

        private void Scratch_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
            if (dragging && selectedElement != null)
                DragDown();

            if (selectedElement != null)
                selectedElement.MouseUp(sender, e);

            dragging = false;
        }


        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Scratch_VisibleChanged(object sender, EventArgs e)
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
                vScrollBar.Value += Math.Min(vScrollBar.Maximum, drawableElement.rect.Top);
        }

        private void Scratch_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }

}
