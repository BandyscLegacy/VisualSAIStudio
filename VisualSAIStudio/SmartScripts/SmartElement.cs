using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio
{
    public abstract class SmartElement : DrawableContainerElement
    {
       public Parameter[] parameters {get; set; }
       public int ID;
 
       public SmartElement()
       {
           parameters = new Parameter[6];
           for (int i = 0; i < 6; ++i)
               parameters[i] = NullParameter.GetInstance();
       }

        public void SetParameter(int index, Parameter parameter)
       {
           parameters[index] = parameter;
           Invalide();
       }

        protected virtual void paramValueChanged(object sender, EventArgs e)
        {
            RequestUpdate(sender, e);
        }

       public virtual void Copy(SmartElement prev)
       {
           for (int i = 0; i < 6; ++i)
               parameters[i].SetValue(prev.parameters[i].GetValue());
            this.children = prev.children;
       }

       public void Invalide()
       {
           paramValueChanged(this, new EventArgs());
       }

       public override Size ComputeSize(Graphics graphics, Font font)
       {
           SizeF measure = graphics.MeasureString(GetReadableString(), font);
           int width = (int)measure.Width + 10;
           children.ForEach(child => width = Math.Max(width, (int)child.ComputeSize(graphics, font).Width));
           int height;
           if (children.Count > 0)
               height = children.Last().rect.Bottom - rect.Top+5;
           else
               height = (int)measure.Height + 10;
           return new Size(width, height);
       }

       public virtual void UpdateParams(int index, int value)
       {
           UpdateParamsInternal(index, value);
           Invalide();
       }

       protected abstract void UpdateParamsInternal(int index, int value);

       public abstract string GetReadableString();
    }
}
