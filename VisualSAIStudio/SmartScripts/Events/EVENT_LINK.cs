using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts.Events
{
    public static class EVENT_LINK
    {
        private static SmartEvent instance;
        public static SmartEvent GetInstance()
        {
            if (instance == null)
            {
                instance = new SmartEvent(new SmartGenericJSONData());
                instance.ID = 61;
                instance.readable = " Linked";
            }
            return instance;
        }
    }
}
