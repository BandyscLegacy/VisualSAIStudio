using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio.Sessions
{
    public struct OpenedScratch
    {
        public SAIType type { set; get; }
        public int entry { set; get; }
        public OpenedScratch(SAIType type, int entry)
            : this()
        {
            this.type = type;
            this.entry = entry;
        }
    }

    public struct Session
    {
        public List<OpenedScratch> list { get; set; }
        public string title { get; set; }
        public Session(List<OpenedScratch> list, string title)
            : this()
        {
            this.list = list;
            this.title = title;
        }

        public Session(string title)
            : this()
        {
            this.list = new List<OpenedScratch>();
            this.title = title;
        }
    }

}
