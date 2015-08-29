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
        private string _comment;
        public string Comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
                Invalide();
            }
        }

        private SmartTarget _target;
        public SmartTarget Target
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

        public SmartAction(SmartGenericJSONData data) : base(data, 6) {
            this.Target = new SMART_TARGET_NONE();
        }

        public override void Copy(SmartElement prev)
        {
            base.Copy(prev);
            this.Target = ((SmartAction)prev).Target;
        }

        public object[] SerializeToArray()
        {
            object[] array = new object[15];
            array[0] = ID;
            for (int i = 0; i < 6;++i)
                array[i+1] = parameters[i].GetValue();
            array[7] = Target.ID;
            for (int i = 0; i < 3;++i)
                array[i+8] = Target.parameters[i].GetValue();
            for (int i = 0; i < 4;++i)
                array[i+11] = Target.position[i];
            return array;
        }

        public static SmartAction DeserializeFromArray(string[] array)
        {
            SmartAction action = SmartFactory.GetInstance().ActionFactory(int.Parse(array[0]));
            action.Target = SmartFactory.GetInstance().TargetFactory(int.Parse(array[7]));
            for (int i = 0; i < 6; ++i)
                action.parameters[i].SetValue(int.Parse(array[i + 1]));
            for (int i = 0; i < 3; ++i)
                action.Target.parameters[i].SetValue(int.Parse(array[i + 8]));
            for (int i = 0; i < 4; ++i)
                action.Target.position[i] = float.Parse(array[i + 11]);
            return action;
        }

        public override Size Draw(Graphics graphics, int x, int start_y, int width, int height, Brush default_brush, Pen pen, Font font, Font mini_font, bool setRect = true)
        {
            SizeF size = graphics.MeasureString(ToString(), font);
            Brush brush = default_brush;
            int y = start_y+3;
            if (!String.IsNullOrEmpty(Comment))
            {
                graphics.DrawString("//" + Comment, font, Brushes.CadetBlue, x + 5, y);
                y += (int)size.Height;
                size.Height *= 2;
            }
            
            graphics.DrawString(ToString(), font, brush, x + 5, y);

            if (setRect)
                SetRect(x, start_y, width, (int)size.Height + 6);

            if (selected)
                graphics.DrawLine(pen, x, start_y, x, start_y + size.Height);

            return new Size(width, (int)size.Height+6);
        }

        protected override void ParameterValueChanged(object sender, EventArgs e)
        {
            if (readable == null || Target == null)
                return;
            output = Smart.Format(readable, new
            {
                                                               target = Target.ToString(),
                                                               targetcoords = Target.GetCoords(), 
                                                               targetid = Target.ID,
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
            if (!(Target.ID == 0 || Target.ID == 1) &&
                !readable.Contains("{target}") && !readable.Contains("{targetcoords}")
                )
                warnings.Add(new Warning(WarningType.INVALID_TARGET, "Target will be ignored", this));

            return warnings;
        }

        
    }

}
