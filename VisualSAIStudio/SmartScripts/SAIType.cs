using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{
    public enum SAIType
    {
        Creature = 0, 
        Gameobject = 1, 
        AreaTrigger = 2, 
        Event = 3, 
        Gossip = 4, 
        Quest = 5, 
        Spell = 6, 
        Transport = 7, 
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
