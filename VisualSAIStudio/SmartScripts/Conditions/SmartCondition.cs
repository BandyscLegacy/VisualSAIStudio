using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public abstract class SmartCondition
    {
        public string help;
        public int ID { get; protected set; }
        public Parameter[] parameters = new Parameter[3];

        public SmartCondition(int id, string help)
        {
            this.ID = id;
            this.help = help;
            for (int i = 0; i < 3;++i )
                parameters[i] = new NullParameter();
        }

        public abstract String GetReadableString();
    }
}
