using SmartFormat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio
{
    public class SmartAction : SmartElement
    {
        private SmartTarget _target;
        public SmartTarget target
        { 
            get
            {
                return this._target;
            }
            set
            {
                this._target = value;
                Invalide();
            }
        }

        public SmartAction(SmartGenericJSONData data) : base(data) {
            this.target = new SMART_TARGET_NONE();
            ParameterValueChanged(this, new EventArgs());
        }

        public override void Copy(SmartElement prev)
        {
            base.Copy(prev);
            this.target = ((SmartAction)prev).target;
        }

        public object[] SerializeToArray()
        {
            object[] array = new object[15];
            array[0] = ID;
            for (int i = 0; i < 6;++i)
                array[i+1] = parameters[i].GetValue();
            array[7] = target.ID;
            for (int i = 0; i < 3;++i)
                array[i+8] = target.parameters[i].GetValue();
            for (int i = 0; i < 4;++i)
                array[i+11] = target.position[i];
            return array;
        }

        public static SmartAction DeserializeFromArray(string[] array)
        {
            SmartAction action = SmartFactory.GetInstance().ActionFactory(int.Parse(array[0]));
            action.target = SmartFactory.GetInstance().TargetFactory(int.Parse(array[7]));
            for (int i = 0; i < 6; ++i)
                action.parameters[i].SetValue(int.Parse(array[i + 1]));
            for (int i = 0; i < 3; ++i)
                action.target.parameters[i].SetValue(int.Parse(array[i + 8]));
            for (int i = 0; i < 4; ++i)
                action.target.position[i] = float.Parse(array[i + 11]);
            return action;
        }

        public override Size Draw(Graphics graphics, int x, int y, int width, int height, Brush default_brush, Pen pen, Font font, Font mini_font, bool setRect = true)
        {
            SizeF size = graphics.MeasureString(ToString(), font);
            Brush brush = default_brush;
            graphics.DrawString(ToString(), font, brush, x + 5, y + 3);

            if (setRect)
                SetRect(x, y, width, (int)size.Height + 6);

            if (selected)
                graphics.DrawLine(pen, x, y, x, y + size.Height);

            return new Size(width, (int)size.Height+6);
        }

        protected override void ParameterValueChanged(object sender, EventArgs e)
        {
            if (readable == null || target == null)
                return;
            output = Smart.Format(readable, new
            {
                                                               target = target.ToString(),
                                                               targetcoords = target.GetCoords(), 
                                                               targetid = target.ID,
                                                               pram1 = parameters[0],
                                                               pram2 = parameters[1],
                                                               pram3 = parameters[2],
                                                               pram4 = parameters[3],
                                                               pram5 = parameters[4],
                                                               pram6 = parameters[5],
                                                               pram1value = parameters[0].GetValue(),
                                                               pram2value = parameters[1].GetValue(),
                                                               pram3value = parameters[2].GetValue(),
                                                               pram4value = parameters[3].GetValue(),
                                                               pram5value = parameters[4].GetValue(),
                                                               pram6value = parameters[5].GetValue()});
            base.ParameterValueChanged(sender, e);
        }

        public override List<Warning> Validate()
        {
            List<Warning> warnings = base.Validate();
            if (!(target.ID == 0 || target.ID == 1) &&
                !readable.Contains("{target}") && !readable.Contains("{targetcoords}")
                )
                warnings.Add(new Warning(WarningType.INVALID_TARGET, "Target will be ignored", this));

            return warnings;
        }

        
    }

}
