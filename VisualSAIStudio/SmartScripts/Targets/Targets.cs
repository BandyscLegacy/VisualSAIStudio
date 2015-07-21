using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public class SMART_TARGET_NONE : SmartTarget
    {
        public SMART_TARGET_NONE() : base(0, "") { }

        public override string GetReadableString()
        {
            return "";
        }
    }

    public class SMART_TARGET_SELF : SmartTarget
    {
        public SMART_TARGET_SELF() : base(1, "Self cast") { }

        public override string GetReadableString()
        {
            return "Self";
        }
    }

    public class SMART_TARGET_VICTIM : SmartTarget
    {
        public SMART_TARGET_VICTIM() : base(2, "Our current target (ie: highest aggro)") { }

        public override string GetReadableString()
        {
            return "Current target";
        }
    }

    public class SMART_TARGET_HOSTILE_SECOND_AGGRO : SmartTarget
    {
        public SMART_TARGET_HOSTILE_SECOND_AGGRO() : base(3, "Second highest aggro") { }

        public override string GetReadableString()
        {
            return "Second highest aggro";
        }
    }

    public class SMART_TARGET_HOSTILE_LAST_AGGRO : SmartTarget
    {
        public SMART_TARGET_HOSTILE_LAST_AGGRO() : base(4, "Dead last on aggro") { }

        public override string GetReadableString()
        {
            return "Dead last on aggro";
        }
    }

    public class SMART_TARGET_HOSTILE_RANDOM : SmartTarget
    {
        public SMART_TARGET_HOSTILE_RANDOM() : base(5, "Any random target on our threat list") { }

        public override string GetReadableString()
        {
            return "Any random target on threat list";
        }
    }

    public class SMART_TARGET_HOSTILE_RANDOM_NOT_TOP : SmartTarget
    {
        public SMART_TARGET_HOSTILE_RANDOM_NOT_TOP() : base(6, "Any random target except top threat") { }

        public override string GetReadableString()
        {
            return "Any random target except top threat";
        }
    }

    public class SMART_TARGET_ACTION_INVOKER : SmartTarget
    {
        public SMART_TARGET_ACTION_INVOKER() : base(7, "Unit who caused the event to occur") { }

        public override string GetReadableString()
        {
            return "Action invoker";
        }
    }

    public class SMART_TARGET_POSITION : SmartTarget
    {
        public SMART_TARGET_POSITION() : base(8, "Use xyz from event params") { }

        public override string GetReadableString()
        {
            return "(" + position[0] + ", " + position[1] + ", " + position[2] + ", " + position[3]+")";
        }
    }

    public class SMART_TARGET_CREATURE_RANGE : SmartTarget
    {
        public SMART_TARGET_CREATURE_RANGE()
            : base(9, "Creature in range")
        {
            parameters[0] = new CreatureParameter("CreatureEntry(0any)");
            parameters[1] = new Parameter("minDist");
            parameters[2] = new Parameter("maxDist");
        }

        public override string GetReadableString()
        {
            return "Creature " + parameters[0] + "in range " + parameters[1] + " - " + parameters[2] + " ms";
        }
    }

    public class SMART_TARGET_CREATURE_GUID : SmartTarget
    {
        public SMART_TARGET_CREATURE_GUID()
            : base(10, "Creature by guid")
        {
            parameters[0] = new Parameter("guid");
            parameters[1] = new CreatureParameter("entry");
        }

        public override string GetReadableString()
        {
            return "Creature guid "+parameters[0];
        }
    }

    public class SMART_TARGET_CREATURE_DISTANCE : SmartTarget
    {
        public SMART_TARGET_CREATURE_DISTANCE()
            : base(11, "Creature within distance")
        {
            parameters[0] = new CreatureParameter("CreatureEntry(0any)");
            parameters[1] = new Parameter("maxDist");
        }

        public override string GetReadableString()
        {
            return "Creature "+parameters[0]+" within distance "+parameters[1];
        }
    }

    public class SMART_TARGET_STORED : SmartTarget
    {
        public SMART_TARGET_STORED()
            : base(12, "Stored target")
        {
            parameters[0] = new Parameter("id");
        }

        public override string GetReadableString()
        {
            return "storeTarget["+parameters[0]+"]";
        }
    }

    public class SMART_TARGET_GAMEOBJECT_RANGE : SmartTarget
    {
        public SMART_TARGET_GAMEOBJECT_RANGE()
            : base(13, "Gameobject in range")
        {
            parameters[0] = new Parameter("entry(0any)");
            parameters[1] = new Parameter("min");
            parameters[2] = new Parameter("max");
        }

        public override string GetReadableString()
        {
            return "Gameobject "+parameters[0]+"in range "+parameters[1]+" - "+parameters[2] + " yards";
        }
    }

    public class SMART_TARGET_GAMEOBJECT_GUID : SmartTarget
    {
        public SMART_TARGET_GAMEOBJECT_GUID()
            : base(14, "Gameobject by guid")
        {
            parameters[0] = new Parameter("guid");
            parameters[1] = new Parameter("entry");
        }

        public override string GetReadableString()
        {
            return "Gameobject guid "+parameters[0];
        }
    }

    public class SMART_TARGET_GAMEOBJECT_DISTANCE : SmartTarget
    {
        public SMART_TARGET_GAMEOBJECT_DISTANCE()
            : base(15, "Gameobject within distance")
        {
            parameters[0] = new Parameter("entry(0any)");
            parameters[1] = new Parameter("maxDist");
        }

        public override string GetReadableString()
        {
            return "Gameobject "+parameters[0]+" within distance "+parameters[1];
        }
    }

    public class SMART_TARGET_INVOKER_PARTY : SmartTarget
    {
        public SMART_TARGET_INVOKER_PARTY() : base(16, "Invoker's party members") { }

        public override string GetReadableString()
        {
            return "Invoker's party members";
        }
    }

    public class SMART_TARGET_PLAYER_RANGE : SmartTarget
    {
        public SMART_TARGET_PLAYER_RANGE()
            : base(17, "Player in range")
        {
            parameters[0] = new Parameter("min");
            parameters[1] = new Parameter("max");
        }

        public override string GetReadableString()
        {
            return "Player in range "+parameters[0]+" - "+parameters[1] + " yards";
        }
    }

    public class SMART_TARGET_PLAYER_DISTANCE : SmartTarget
    {
        public SMART_TARGET_PLAYER_DISTANCE()
            : base(18, "Player within distance")
        {
            parameters[0] = new Parameter("maxDist");
        }

        public override string GetReadableString()
        {
            return "Player within distance "+parameters[0];
        }
    }

    public class SMART_TARGET_CLOSEST_CREATURE : SmartTarget
    {
        public SMART_TARGET_CLOSEST_CREATURE()
            : base(19, "Closest creature")
        {
            parameters[0] = new CreatureParameter("CreatureEntry(0any)");
            parameters[1] = new Parameter("maxDist");
            parameters[2] = new Parameter("dead?");
        }

        public override string GetReadableString()
        {
            return "Closest creature "+parameters[0];
        }
    }

    public class SMART_TARGET_CLOSEST_GAMEOBJECT : SmartTarget
    {
        public SMART_TARGET_CLOSEST_GAMEOBJECT()
            : base(20, "Closest gameobject")
        {
            parameters[0] = new Parameter("entry(0any)");
            parameters[1] = new Parameter("maxDist");
        }

        public override string GetReadableString()
        {
            return "Closest gameobject";
        }
    }

    public class SMART_TARGET_CLOSEST_PLAYER : SmartTarget
    {
        public SMART_TARGET_CLOSEST_PLAYER()
            : base(21, "Closest player")
        {
            parameters[0] = new Parameter("maxDist");
        }

        public override string GetReadableString()
        {
            return "Closest player within "+parameters[0];
        }
    }

    public class SMART_TARGET_ACTION_INVOKER_VEHICLE : SmartTarget
    {
        public SMART_TARGET_ACTION_INVOKER_VEHICLE() : base(22, "Unit's vehicle who caused this Event to occur") { }

        public override string GetReadableString()
        {
            return "Unit's vehicle who caused this Event to occur";
        }
    }

    public class SMART_TARGET_OWNER_OR_SUMMONER : SmartTarget
    {
        public SMART_TARGET_OWNER_OR_SUMMONER() : base(23, "Unit's owner or summoner") { }

        public override string GetReadableString()
        {
            return "Unit's owner or summoner";
        }
    }

    public class SMART_TARGET_THREAT_LIST : SmartTarget
    {
        public SMART_TARGET_THREAT_LIST() : base(24, "All units on creature's threat list") { }

        public override string GetReadableString()
        {
            return "All units on creature's threat list";
        }
    }


}
