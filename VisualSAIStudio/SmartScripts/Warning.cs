using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualSAIStudio
{
    public class Warning
    {
        public WarningType warning { get; set; }
        public SmartElement element { get; set; }
        public string description { get; set; }

        public Warning(WarningType warning, string description, SmartElement element)
        {
            this.warning = warning;
            this.description = description;
            this.element = element;
        }

    }

    public enum WarningType
    {
        IGNORED_TARGET,
        INVALID_PARAMETER
    }
}
