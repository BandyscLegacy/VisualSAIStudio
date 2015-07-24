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
        protected List<ParameterConditional> conditionals = new List<ParameterConditional>();
        public Parameter[] parameters { get; set; }
        public int ID { get; set; }
        protected string readable;
        public string name;

        public SmartElement()
        {
            parameters = new Parameter[6];
            for (int i = 0; i < 6; ++i)
                SetParameter(i, new NullParameter(), false);
        }

        public void SetParameter(int index, Parameter parameter, bool invalide = true)
        {
            parameters[index] = parameter;

            this.conditionals.AddRange(parameter.GetValidators());

            if (invalide)
                Invalide();
        }

        protected void AddConditional(ParameterConditional conditional)
        {
            this.conditionals.Add(conditional);
        }

        protected virtual void paramValueChanged(object sender, EventArgs e)
        {
            RequestUpdate(sender, e);
        }

        public virtual void Copy(SmartElement prev)
        {
            for (int i = 0; i < parameters.Length; ++i)
                parameters[i].SetValue(prev.parameters[i].GetValue());
            this.children = prev.children;
        }

        public void Invalide()
        {
            paramValueChanged(this, new EventArgs());
        }

        public virtual List<Warning> Validate()
        {
            List<Warning> warnings = new List<Warning>();
            foreach (ParameterConditional condit in conditionals)
            {
                if (!condit.Validate())
                    warnings.Add(new Warning(condit.warningType, condit.description, this));
            }

            return warnings;
        }

        public override Size ComputeSize(Graphics graphics, Font font)
        {
            SizeF measure = graphics.MeasureString(ToString(), font);
            return new Size((int)measure.Width,(int)measure.Height);
        }

        public virtual void UpdateParams(int index, int value)
        {
            UpdateParamsInternal(index, value);
            Invalide();
        }

        protected abstract void UpdateParamsInternal(int index, int value);

        public abstract string GetReadableString();

        public override string ToString()
        {
            return readable;
        }
    }
}
