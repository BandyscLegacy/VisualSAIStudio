using SmartFormat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio
{
    public abstract class SmartAction : SmartElement
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
                paramValueChanged(this, new EventArgs());
            }
        }

        public SmartAction() : base()
        {
            target = new SMART_TARGET_SELF();
        }

        public SmartAction(int id, string name) : base()
        {
            target = new SMART_TARGET_SELF();
            this.ID = id;
            this.name = name;
        }

        protected virtual string GetCpp() { return ""; }

        public string GetCPPCode()
        {
            return "";
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
            SmartAction action = SmartFactory.ActionFactory(int.Parse(array[0]));
            action.target = TargetsFactory.Factory(int.Parse(array[7]));
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

        protected override void paramValueChanged(object sender, EventArgs e)
        {
            if (GetReadableString() == null)
                return;
            readable = Smart.Format(GetReadableString(), new { target = target.GetReadableString(),
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
            base.paramValueChanged(sender, e);
        }

        protected override void UpdateParamsInternal(int index, int value)
        {
            this.parameters[index].SetValue(value);
            paramValueChanged(this, new EventArgs());
        }


        public override List<Warning> Validate()
        {
            List<Warning> warnings = base.Validate();
            if (!(target is SMART_TARGET_NONE || target is SMART_TARGET_SELF) &&
                !GetReadableString().Contains("{target}") && !GetReadableString().Contains("{targetcoords}")
                )
                warnings.Add(new Warning(WarningType.INVALID_TARGET, "Target will be ignored", this));

            return warnings;
        }

        
    }

}
