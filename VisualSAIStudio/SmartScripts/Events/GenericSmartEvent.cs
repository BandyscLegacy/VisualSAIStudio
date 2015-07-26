using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    class GenericSmartEvent : SmartEvent
    {
        protected string description;

        public GenericSmartEvent(int id, string name, string description) : base(id, name) 
        {
            this.readable = description;
            this.description = description;
        }

        public override string GetReadableString()
        {
            return description;
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
            GenericSmartEvent ev = new GenericSmartEvent(data.id, data.name, data.description);
            ev.AddParameters(data.parameters);
            ev.AddConditionals(data.conditions);
            return ev;
        }

    }
}
