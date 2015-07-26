using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public class SmartParameterJSONData
    {
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
        public Dictionary<int, SelectOption> values { get; set; }
    }

    public class SmartConditionalJSONData
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WarningType warning_type {get; set;}
        [JsonConverter(typeof(StringEnumConverter))]
        public SmartScripts.CompareType compare_type {get; set;}

        public string type {get; set;}
        public bool invert {get; set;}
        public int compared_parameter_id { get; set;  }
        public int compare_to_parameter_id {get; set;}
        public int compare_to_value {get; set;}
        public string error {get; set;}
    }

    public class SmartGenericJSONData
    {
        public int id {get; set;}
        public string name {get; set;}
        public IList<SmartParameterJSONData> parameters { get; set; }
        public IList<SmartConditionalJSONData> conditions { get; set; }
        public IList<SmartScripts.SAIType> valid_types { get; set; }
        public string description {get; set;}
        public string tooltip {get; set;}
    }
}
