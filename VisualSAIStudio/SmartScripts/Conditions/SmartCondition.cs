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


    public abstract class SmartCondition : SmartElement
    {
        public string help;
        public int ID { get; protected set; }
        public bool invert { get; set; }
        public ConditionTarget target { get; set; }

        public SmartCondition(int id, string name, string help)
        {
            parameters = new Parameter[3];
            this.ID = id;
            this.help = help;
            this.name = name;
            for (int i = 0; i < 3;++i )
                parameters[i] = new NullParameter();
        }

        protected override void UpdateParamsInternal(int index, int value)
        {
            this.parameters[index].SetValue(value);
            paramValueChanged(this, new EventArgs());
        }

        protected override void paramValueChanged(object sender, EventArgs e)
        {
            if (GetReadableString() == null)
                return;
            readable = Smart.Format(GetReadableString(), new
            {
                target = target,
                pram1 = parameters[0],
                pram2 = parameters[1],
                pram3 = parameters[2],
                pram1value = parameters[0].GetValue(),
                pram2value = parameters[1].GetValue(),
                pram3value = parameters[2].GetValue(),
            });
            base.paramValueChanged(sender, e);
        }

        public override System.Drawing.Size Draw(System.Drawing.Graphics graphics, int x, int y, int width, int height, System.Drawing.Brush brush, System.Drawing.Pen pen, System.Drawing.Font font, bool setRect = true)
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
}
