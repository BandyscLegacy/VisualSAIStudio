using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public class DBCConfig
    {
        public int version { get; set; }
        public string definition { get; set; }
        public Dictionary<StorageType, OffsetDefinition> offsets { get; set; }
    }

    public class OffsetDefinition
    {
        public string file {get; set;}
        public int offset {get; set;}
        public bool unsupported { get; set; }
    }
}
