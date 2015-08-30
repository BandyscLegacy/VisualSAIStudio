using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.Updater
{
    public enum UpdateResult
    {
        Ok,
        NewVersion
    }

    class UpdateData
    {
        public string download { get; set; }
        public UpdateResult result { get; set; }
    }
}
