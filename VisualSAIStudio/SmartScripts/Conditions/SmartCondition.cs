using SmartFormat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public enum ConditionTarget
    {
        Invoker = 0,
        Object = 1,
    }


    public class SmartCondition : SmartElement
    {
        public bool invert { get; set; }
        public ConditionTarget target { get; set; }

        public SmartCondition(int id, string name, string readable) : base(id, name, readable, 3) {  }

        public SmartCondition(SmartGenericJSONData data) : base(data, 3) { }

        protected override void ParameterValueChanged(object sender, EventArgs e)
        {
            if (readable == null)
                return;
            output = Smart.Format(readable, new
            {
                target = target,
                pram1 = parameters[0],
                pram2 = parameters[1],
                pram3 = parameters[2],
                pram1value = parameters[0].GetValue(),
                pram2value = parameters[1].GetValue(),
                pram3value = parameters[2].GetValue(),
            });
            base.ParameterValueChanged(sender, e);
        }

        public override System.Drawing.Size Draw(System.Drawing.Graphics graphics, int x, int y, int width, int height, Brush brush, Pen pen, Font font, Font mini_font, bool setRect = true)
        {
            SizeF size = graphics.MeasureString(ToString(), font);
            graphics.DrawString(ToString(), font, brush, x, y);

            if (setRect)
                SetRect(x, y, width, (int)size.Height + 6);

            if (selected)
                graphics.DrawLine(pen, x, y, x, y + size.Height);

            return new Size(width, (int)size.Height + 6);
        }
    }

    public class CONDITION_LOGICAL_OR : SmartCondition
    {
        public CONDITION_LOGICAL_OR() : base(-1, "CONDITION_LOGICAL_OR", "or") { }
    }
}
