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
        protected string output;
        private string _readable;
        public string readable
        {
            get
            {
                return _readable;
            }
            set
            {
                _readable = value;
                ParameterValueChanged(this, new EventArgs());
            }
        }

        public Parameter[] parameters { get; set; }
        public int ID { get; set; }
        public string name;


        public SmartElement(int ID, string name, string readable, int parameters_count)
        {
            this.ID = ID;
            this.name = name;
            this._readable = readable;
            parameters = new Parameter[parameters_count];
            for (int i = 0; i < parameters_count; ++i)
                SetParameter(i, new NullParameter(), false);
            Invalide();
        }

        public SmartElement(SmartGenericJSONData data, int parameters_count) : this(data.id, data.name, data.description, parameters_count)
        {
            AddParameters(data.parameters);
            AddConditionals(data.conditions);
            ParameterValueChanged(this, new EventArgs());
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

        protected virtual void ParameterValueChanged(object sender, EventArgs e)
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
            ParameterValueChanged(this, new EventArgs());
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

        public override Size ComputeSize(Graphics graphics, Font font, Font mini_font)
        {
            SizeF measure = graphics.MeasureString(ToString(), font);
            return new Size((int)measure.Width,(int)measure.Height);
        }

        public virtual void UpdateParams(int index, int value)
        {
            this.parameters[index].SetValue(value);
            Invalide();
        }

        protected void AddParameters(IList<SmartParameterJSONData> parameters)
        {
            int i = 0;
            if (parameters == null)
                return;
            foreach (SmartParameterJSONData param_data in parameters)
            {
                Parameter pram = Parameter.Factory(param_data.type);
                pram.name = param_data.name;
                pram.description = param_data.description;
                if (param_data.values != null)
                    ((SwitchParameter)pram).select = param_data.values;
                SetParameter(i, pram);
                i++;
            }
        }

        protected void AddConditionals(IList<SmartConditionalJSONData> conditions)
        {
            if (conditions != null)
            {
                foreach (SmartConditionalJSONData condition in conditions)
                {
                    ParameterConditional conditional = ParameterConditional.Factory(condition.type, parameters[condition.compared_parameter_id - 1]);
                    if (condition.compare_to_parameter_id > 0)
                        conditional.SetCompareTo(parameters[condition.compare_to_parameter_id - 1]);
                    else
                        conditional.SetCompareTo(condition.compare_to_value);

                    if (conditional is ParameterConditionalCompareValue)
                        ((ParameterConditionalCompareValue)conditional).SetCompareType(condition.compare_type);

                    if (condition.warning_type != WarningType.NOT_SET)
                        conditional.warningType = condition.warning_type;

                    if (condition.error != null)
                        conditional.SetDescription(condition.error);
                    
                    if (condition.invert)
                        conditional = new ParameterConditionalInversed(conditional);
                    AddConditional(conditional);
                }
            }
        }

        public override string ToString()
        {
            return output;
        }
    }

}
