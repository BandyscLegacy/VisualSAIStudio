using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualSAIStudio
{
    public abstract class DrawableElement 
    {
        public Rectangle rect;
        public bool selected { get; set; }
        public EventHandler RequestUpdate = delegate { };
        public EventHandler RequestRemove = delegate { };
        public DrawableElement parent;

        public MouseEventHandler MouseDown = delegate { };
        public EventHandler Selected = delegate { };

        public bool contains(int x, int y)
        {
            return rect.Contains(x, y);
        }

        protected void SetRect(int x, int y, int width, int height)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = width;
            rect.Height = height;
        }

        public virtual void setSelected(bool value)
        {
            selected = value;
            if (value)
                Selected(this, new EventArgs());
        }
        public virtual void setSelected(Point mouse)
        {
            if (rect.Contains(mouse))
            {
                selected = true;
                Selected(this, new EventArgs());
            }
            else
                selected = false;
        }
        public abstract Size Draw(Graphics graphics, int x, int y, int width, int height, Brush brush, Pen pen, Font font, bool setRect=true);

        public abstract Size ComputeSize(Graphics graphics, Font font);
    }
}
