using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts.Actions
{
    class ACTION_UNSUPPORTED : SmartAction
    {
        public ACTION_UNSUPPORTED(int id)
            : base(new SmartGenericJSONData())
        {
            this.ID = id;
            readable = "Unknown action "+id;
        }

        public ACTION_UNSUPPORTED(string id) : base(new SmartGenericJSONData())
        {
            this.ID = -1;
            readable = "Unknown action "+id;
        }
    }
}
