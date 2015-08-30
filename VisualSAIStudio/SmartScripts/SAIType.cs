using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{
    class UnsupportedAttribute : Attribute
    {
    }

    public enum SAIType
    {
        Creature = 0, 
        Gameobject = 1, 
        AreaTrigger = 2, 
        [Unsupported]
        Event = 3,
        [Unsupported]
        Gossip = 4,
        [Unsupported]
        Quest = 5,
        [Unsupported]
        Spell = 6,
        [Unsupported]
        Transport = 7,
        [Unsupported]
        Instance = 8,
        TimedActionList = 9, 
    }

    public enum SmartType
    {
        SMART_EVENT = 0,
        SMART_ACTION = 1,
        SMART_TARGET = 2,
        SMART_CONDITION = 3,
    }
}
