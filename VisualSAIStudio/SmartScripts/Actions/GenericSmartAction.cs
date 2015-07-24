using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSAIStudio.SmartScripts;

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
            action.AddParameters(data.parameters);
            action.AddConditionals(data.conditions);
            return action;
        }

    }
}
