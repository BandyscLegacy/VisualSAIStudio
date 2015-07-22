using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    class GenericSmartEvent : SmartEvent
    {
        public GenericSmartEvent(int id, string name) : base(id, name) { }

        protected string description;

        public override string GetReadableString()
        {
            return String.Format(description, parameters[0], parameters[1], parameters[2], parameters[3]);
        }


        private void SetParameter(int index, Parameter pram)
        {
            this.parameters[index] = pram;
        }

        private void SetDescription(string desc)
        {
            description = desc;
        }

        public static GenericSmartEvent Factory(SmartGenericJSONData data)
        {
            GenericSmartEvent ev = new GenericSmartEvent(data.id, data.name);
            int i = 0;
            foreach (SmartParameterJSONData param_data in data.parameters)
            {
                Parameter pram = Parameter.Factory(param_data.type);
                pram.name = param_data.name;
                pram.description = param_data.description;
                if (param_data.values != null)
                    ((SwitchParameter)pram).select = param_data.values;
                
                ev.SetParameter(i++, pram);
            }
            ev.SetDescription(data.description);
            return ev;
        }

    }
}
