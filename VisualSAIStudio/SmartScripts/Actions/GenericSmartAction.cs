using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    class GenericSmartAction : SmartAction
    {
        public GenericSmartAction(int id, string name, string description) : base(id, name) 
        {
            this.description = description;
        }

        protected string description = "";

        public override string GetReadableString()
        {
            return description;
        }


        private void SetParameter(int index, Parameter pram)
        {
            this.parameters[index] = pram;
            paramValueChanged(this, new EventArgs());
        }

        private void SetDescription(string desc)
        {
            description = desc;
        }

        public static GenericSmartAction Factory(SmartGenericJSONData data)
        {
            GenericSmartAction action = new GenericSmartAction(data.id, data.name, data.description);
            int i = 0;
            foreach (SmartParameterJSONData param_data in data.parameters)
            {
                Parameter pram = Parameter.Factory(param_data.type);
                pram.name = param_data.name;
                pram.description = param_data.description;
                if (param_data.values != null)
                    ((SwitchParameter)pram).select = param_data.values;

                action.SetParameter(i, pram);
                i++;
            }
            return action;
        }

    }
}
