using SmartFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public class SmartTarget : SmartElement
    {
        public float[] position = new float[4];

        public SmartTarget(int id, string name, string readable) : base (id, name, readable, 3) { }

        public SmartTarget(SmartGenericJSONData data) : base(data, 3) {  }

        public string GetCoords()
        {
            return String.Format("({0}, {1}, {2}, {3})", position[0], position[1], position[2], position[3]);
        }

        protected override void ParameterValueChanged(object sender, EventArgs e)
        {
            if (readable == null)
                return;
            output = Smart.Format(readable, new
            {
                pram1 = parameters[0].ToString(),
                pram2 = parameters[1].ToString(),
                pram3 = parameters[2].ToString(),
                pram1value = parameters[0].GetValue(),
                pram2value = parameters[1].GetValue(),
                pram3value = parameters[2].GetValue(),
                x = position[0],
                y = position[1],
                z = position[2],
                o = position[3],
            });
            base.ParameterValueChanged(sender, e);
        }

        public void UpdateParams(SmartTarget smartTarget)
        {
            for (int i = 0; i < 3;++i)
                parameters[i].SetValue(smartTarget.parameters[i].GetValue());

            for (int i = 0; i < 4; ++i)
                position[i] = smartTarget.position[i];
        }


        public override System.Drawing.Size Draw(System.Drawing.Graphics graphics, int x, int y, int width, int height, System.Drawing.Brush brush, System.Drawing.Pen pen, System.Drawing.Font font, System.Drawing.Font mini_font, bool setRect = true)
        {
            throw new NotImplementedException();
        }
    }

    public class SMART_TARGET_NONE : SmartTarget
    {
        public SMART_TARGET_NONE() : base(0, "SMART_TARGET_NONE", "None") { }
    }
}

