using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public struct GuidEntry
    {
        public int id { get; set; }
        public int map { get; set; }
        public string name { get; set; }
        public float position_x { get; set; }
        public float position_y { get; set; }
        public float position_z { get; set; }
        public bool smartAI { get; set; }
    }
}
