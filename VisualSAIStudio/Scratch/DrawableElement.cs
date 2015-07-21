using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VisualSAIStudio
{
    public abstract class DrawableElement
    {
        public Rectangle rect;
        public bool selected { get; set; }
        public EventHandler RequestUpdate = delegate { };

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
        }
        public virtual void setSelected(Point mouse)
        {
            if (rect.Contains(mouse))
                selected = true;
            else
                selected = false;
        }
        public abstract Size Draw(Graphics graphics, int x, int y, int width, int height, Brush brush, Pen pen, Font font, bool setRect=true);

        public abstract Size ComputeSize(Graphics graphics, Font font);
    }
}
