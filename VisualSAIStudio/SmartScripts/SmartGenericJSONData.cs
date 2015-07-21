using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    class SmartParameterJSONData
    {
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public Dictionary<int, string> values { get; set; }
    }

    class SmartGenericJSONData
    {
        public int id {get; set;}
        public string name {get; set;}
        public IList<SmartParameterJSONData> parameters { get; set; }
        public string description {get; set;}
        public string tooltip {get; set;}
    }
}
