using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public abstract class DrawableContainerElement : DrawableElement
    {
        public List<DrawableElement> children = new List<DrawableElement>();
        protected DrawableElement selectedChildren;
        
        public override void setSelected(Point mouse)
        {
            setSelected(true);
            foreach (DrawableElement child in children)
            {
                if (child.contains(mouse.X, mouse.Y))
                {
                    child.setSelected(true);
                    selectedChildren = child;
                    return;
                }
            }
        }

        private void DeselectChildren()
        {
            if (selectedChildren != null)
                selectedChildren.setSelected(false);
            selectedChildren = null;
        }

        public override void setSelected(bool value)
        {
            base.setSelected(value);
            DeselectChildren();
        }


        public DrawableElement GetElementFromPos(int x, int y)
        {
            foreach (DrawableElement child in children)
            {
                if (child.contains(x, y))
                    return child;
            }
            return null;
        }
    }
}
