using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public class CONDITION_LOGICAL_OR : SmartCondition
    {
        public CONDITION_LOGICAL_OR() : base(-1, "or") { }

        public override string GetReadableString()
        {
            return "Or";
        }
    }

    public class CONDITION_NONE : SmartCondition
    {
        public CONDITION_NONE()
            : base(0, "always true")
        {
        }

        public override string GetReadableString()
        {
            return "If (true)";
        }
    }

    public class CONDITION_AURA : SmartCondition
    {
        public CONDITION_AURA()
            : base(1, "true if player (or target, if value3) has aura of spell_id with effect effindex")
        {
            parameters[0] = new SpellParameter("spell_id");
            parameters[1] = new Parameter("effindex");
            parameters[2] = new Parameter("use target?");
        }

        public override string GetReadableString()
        {
            return "If "+(parameters[2].GetValue() == 0 ? "player":"target")+" has aura of spell " + parameters[0] + " with effect " + parameters[1];
        }
    }

    public class CONDITION_ITEM : SmartCondition
    {
        public CONDITION_ITEM()
            : base(2, "true if has #count of item_ids (if 'bank' is set it searches in bank slots too)")
        {
            parameters[0] = new ItemParameter("item_id");
            parameters[1] = new Parameter("count");
            parameters[2] = new Parameter("bank");
        }

        public override string GetReadableString()
        {
            return "If target has "+parameters[1]+" of item "+parameters[1]+(parameters[2].GetValue()==1?"(check bank too)":"");
        }
    }

    public class CONDITION_ITEM_EQUIPPED : SmartCondition
    {
        public CONDITION_ITEM_EQUIPPED()
            : base(3, "true if has item_id equipped")
        {
            parameters[0] = new ItemParameter("item_id");
        }

        public override string GetReadableString()
        {
            return "If target has equiped item "+parameters[0];
        }
    }

    public class CONDITION_ZONEID : SmartCondition
    {
        public CONDITION_ZONEID()
            : base(4, "true if in zone_id")
        {
            parameters[0] = new ZoneAreaParameter("zone_id");
        }

        public override string GetReadableString()
        {
            return "If target in zone "+parameters[0];
        }
    }

    public class CONDITION_REPUTATION_RANK : SmartCondition
    {
        public CONDITION_REPUTATION_RANK()
            : base(5, "true if has min_rank for faction_id")
        {
            parameters[0] = new Parameter("faction_id");
            parameters[1] = new Parameter("rankMask");
        }

        public override string GetReadableString()
        {
            return "true if has min_rank for faction_id";
        }
    }

    public class CONDITION_TEAM : SmartCondition
    {
        public CONDITION_TEAM()
            : base(6, "469 - Alliance, 67 - Horde)")
        {
            parameters[0] = new Parameter("player_team");
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 469)
                return "If player belongs to Alliance";
            else if (parameters[0].GetValue() ==67)
                return "If player belongs to Horde";
            else
                return "(invalid) only accepted value 469 (ally) and 67 (horde)";
        }
    }

    public class CONDITION_SKILL : SmartCondition
    {
        public CONDITION_SKILL()
            : base(7, "true if has skill_value for skill_id")
        {
            parameters[0] = new Parameter("skill_id");
            parameters[1] = new Parameter("skill_value");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if has skill_value for skill_id";
        }
    }

    public class CONDITION_QUESTREWARDED : SmartCondition
    {
        public CONDITION_QUESTREWARDED()
            : base(8, "true if quest_id was rewarded before")
        {
            parameters[0] = new QuestParameter("quest_id");
        }

        public override string GetReadableString()
        {
            return "If player has quest "+parameters[0]+" rewarded";
        }
    }

    public class CONDITION_QUESTTAKEN : SmartCondition
    {
        public CONDITION_QUESTTAKEN()
            : base(9, "true while quest active")
        {
            parameters[0] = new QuestParameter("quest_id");
        }

        public override string GetReadableString()
        {
           return "If player has quest "+parameters[0]+" active";
        }
    }

    public class CONDITION_DRUNKENSTATE : SmartCondition
    {
        public CONDITION_DRUNKENSTATE()
            : base(10, "true if player is drunk enough")
        {
            parameters[0] = new Parameter("DrunkenState");
        }

        public override string GetReadableString()
        {
            return "If player has drunk state "+parameters[0];
        }
    }

    public class CONDITION_WORLD_STATE : SmartCondition
    {
        public CONDITION_WORLD_STATE()
            : base(11, "true if world has the value for the index")
        {
            parameters[0] = new Parameter("index");
            parameters[1] = new Parameter("value");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if world has the value for the index";
        }
    }

    public class CONDITION_ACTIVE_EVENT : SmartCondition
    {
        public CONDITION_ACTIVE_EVENT()
            : base(12, "true if event is active")
        {
            parameters[0] = new Parameter("event_id");
        }

        public override string GetReadableString()
        {
            return "If event " +parameters[0]+ "is active";
        }
    }

    public class CONDITION_INSTANCE_INFO : SmartCondition
    {
        public CONDITION_INSTANCE_INFO()
            : base(13, "true if the instance info defined by type (enum InstanceInfo) equals data.")
        {
            parameters[0] = new Parameter("entry");
            parameters[1] = new Parameter("data");
            parameters[2] = new Parameter("type");
        }

        public override string GetReadableString()
        {
            return "true if the instance info defined by type (enum InstanceInfo) equals data.";
        }
    }

    public class CONDITION_QUEST_NONE : SmartCondition
    {
        public CONDITION_QUEST_NONE()
            : base(14, "true if doesn't have quest saved")
        {
            parameters[0] = new Parameter("quest_id");
        }

        public override string GetReadableString()
        {
            return "If player has quest "+parameters[0]+" neither rewarded nor active";
        }
    }

    public class CONDITION_CLASS : SmartCondition
    {
        public CONDITION_CLASS()
            : base(15, "true if player's class is equal to class")
        {
            parameters[0] = new Parameter("class");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if player's class is equal to class";
        }
    }

    public class CONDITION_RACE : SmartCondition
    {
        public CONDITION_RACE()
            : base(16, "true if player's race is equal to race")
        {
            parameters[0] = new Parameter("race");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if player's race is equal to race";
        }
    }

    public class CONDITION_ACHIEVEMENT : SmartCondition
    {
        public CONDITION_ACHIEVEMENT()
            : base(17, "true if achievement is complete")
        {
            parameters[0] = new Parameter("achievement_id");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if achievement is complete";
        }
    }

    public class CONDITION_TITLE : SmartCondition
    {
        public CONDITION_TITLE()
            : base(18, "0                  true if player has title")
        {
            parameters[0] = new Parameter("title");
            parameters[1] = new Parameter("id");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "0                  true if player has title";
        }
    }

    public class CONDITION_SPAWNMASK : SmartCondition
    {
        public CONDITION_SPAWNMASK()
            : base(19, "true if in spawnMask")
        {
            parameters[0] = new Parameter("spawnMask");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if in spawnMask";
        }
    }

    public class CONDITION_GENDER : SmartCondition
    {
        public CONDITION_GENDER()
            : base(20, "true if player's gender is equal to gender")
        {
            parameters[0] = new Parameter("gender");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if player's gender is equal to gender";
        }
    }

    public class CONDITION_CREATURE_TYPE : SmartCondition
    {
        public CONDITION_CREATURE_TYPE()
            : base(21, "true if creature/unit is specific type..")
        {
            parameters[0] = new Parameter("CreaturType");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if creature/unit is specific type..";
        }
    }

    public class CONDITION_MAPID : SmartCondition
    {
        public CONDITION_MAPID()
            : base(22, "true if in map_id")
        {
            parameters[0] = new Parameter("map_id");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if in map_id";
        }
    }

    public class CONDITION_AREAID : SmartCondition
    {
        public CONDITION_AREAID()
            : base(23, "true if in area_id")
        {
            parameters[0] = new ZoneAreaParameter("area_id");
        }

        public override string GetReadableString()
        {
            return "If target in area "+parameters[0];
        }
    }

    public class CONDITION_SPELL : SmartCondition
    {
        public CONDITION_SPELL()
            : base(25, "true if player has learned spell")
        {
            parameters[0] = new Parameter("spell_id");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if player has learned spell";
        }
    }

    public class CONDITION_PHASEMASK : SmartCondition
    {
        public CONDITION_PHASEMASK()
            : base(26, "true if object is in phasemask")
        {
            parameters[0] = new Parameter("phasemask");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if object is in phasemask";
        }
    }

    public class CONDITION_LEVEL : SmartCondition
    {
        public CONDITION_LEVEL()
            : base(27, "true if unit's level is equal to param1 (param2 can modify the statement)")
        {
            parameters[0] = new Parameter("level");
            parameters[1] = new Parameter("ComparisonType");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if unit's level is equal to param1 (param2 can modify the statement)";
        }
    }

    public class CONDITION_QUEST_COMPLETE : SmartCondition
    {
        public CONDITION_QUEST_COMPLETE()
            : base(28, "true if player has quest_id with all objectives complete, but not yet rewarded")
        {
            parameters[0] = new Parameter("quest_id");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if player has quest_id with all objectives complete, but not yet rewarded";
        }
    }

    public class CONDITION_NEAR_CREATURE : SmartCondition
    {
        public CONDITION_NEAR_CREATURE()
            : base(29, "true if there is a creature of entry in range")
        {
            parameters[0] = new Parameter("creature_entry");
            parameters[1] = new Parameter("distance");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if there is a creature of entry in range";
        }
    }

    public class CONDITION_NEAR_GAMEOBJECT : SmartCondition
    {
        public CONDITION_NEAR_GAMEOBJECT()
            : base(30, "true if there is a gameobject of entry in range")
        {
            parameters[0] = new Parameter("gameobject_entry");
            parameters[1] = new Parameter("distance");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if there is a gameobject of entry in range";
        }
    }

    public class CONDITION_OBJECT_ENTRY : SmartCondition
    {
        public CONDITION_OBJECT_ENTRY()
            : base(31, "true if object is type TypeID and the entry is 0 or matches entry of the object")
        {
            parameters[0] = new Parameter("TypeID");
            parameters[1] = new Parameter("entry");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if object is type TypeID and the entry is 0 or matches entry of the object";
        }
    }

    public class CONDITION_TYPE_MASK : SmartCondition
    {
        public CONDITION_TYPE_MASK()
            : base(32, "true if object is type object's TypeMask matches provided TypeMask")
        {
            parameters[0] = new Parameter("TypeMask");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if object is type object's TypeMask matches provided TypeMask";
        }
    }

    public class CONDITION_RELATION_TO : SmartCondition
    {
        public CONDITION_RELATION_TO()
            : base(33, "true if object is in given relation with object specified by ConditionTarget")
        {
            parameters[0] = new Parameter("ConditionTarget");
            parameters[1] = new Parameter("RelationType");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if object is in given relation with object specified by ConditionTarget";
        }
    }

    public class CONDITION_REACTION_TO : SmartCondition
    {
        public CONDITION_REACTION_TO()
            : base(34, "true if object's reaction matches rankMask object specified by ConditionTarget")
        {
            parameters[0] = new Parameter("ConditionTarget");
            parameters[1] = new Parameter("rankMask");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if object's reaction matches rankMask object specified by ConditionTarget";
        }
    }

    public class CONDITION_DISTANCE_TO : SmartCondition
    {
        public CONDITION_DISTANCE_TO()
            : base(35, "true if object and ConditionTarget are within distance given by parameters")
        {
            parameters[0] = new Parameter("ConditionTarget");
            parameters[1] = new Parameter("distance");
            parameters[2] = new Parameter("ComparisonType");
        }

        public override string GetReadableString()
        {
            return "true if object and ConditionTarget are within distance given by parameters";
        }
    }

    public class CONDITION_ALIVE : SmartCondition
    {
        public CONDITION_ALIVE()
            : base(36, "true if unit is alive")
        {
            parameters[0] = new Parameter("0");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if unit is alive";
        }
    }

    public class CONDITION_HP_VAL : SmartCondition
    {
        public CONDITION_HP_VAL()
            : base(37, "true if unit's hp matches given value")
        {
            parameters[0] = new Parameter("hpVal");
            parameters[1] = new Parameter("ComparisonType");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if unit's hp matches given value";
        }
    }

    public class CONDITION_HP_PCT : SmartCondition
    {
        public CONDITION_HP_PCT()
            : base(38, "true if unit's hp matches given pct")
        {
            parameters[0] = new Parameter("hpPct");
            parameters[1] = new Parameter("ComparisonType");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if unit's hp matches given pct";
        }
    }

    public class CONDITION_SMART_PHASE : SmartCondition
    {
        public CONDITION_SMART_PHASE()
            : base(39, "true if there is a gameobject of entry in range")
        {
            parameters[0] = new Parameter("smart_script_phase");
            parameters[1] = new Parameter("id");
            parameters[2] = new Parameter("referenceID");
        }

        public override string GetReadableString()
        {
            return "true if there is a gameobject of entry in range";
        }
    }

    public class CONDITION_NOT_NEAR_CREATURE : SmartCondition
    {
        public CONDITION_NOT_NEAR_CREATURE()
            : base(40, "true if there isn't a creature of entry in range")
        {
            parameters[0] = new Parameter("creature_entry");
            parameters[1] = new Parameter("distance");
            parameters[2] = new Parameter("referenceID");
        }

        public override string GetReadableString()
        {
            return "true if there isn't a creature of entry in range";
        }
    }

    public class CONDITION_NOT_NEAR_GAMEOBJECT : SmartCondition
    {
        public CONDITION_NOT_NEAR_GAMEOBJECT()
            : base(41, "true if there isn't a gameobject of entry in range")
        {
            parameters[0] = new Parameter("gameobject_entry");
            parameters[1] = new Parameter("distance");
            parameters[2] = new Parameter("referenceID");
        }

        public override string GetReadableString()
        {
            return "true if there isn't a gameobject of entry in range";
        }
    }

    public class CONDITION_TARGET_NO_AURA_IN_RANGE : SmartCondition
    {
        public CONDITION_TARGET_NO_AURA_IN_RANGE()
            : base(42, "true if does not have aura of spell_id with effect effindex")
        {
            parameters[0] = new Parameter("spell_id");
            parameters[1] = new Parameter("TargetEntry");
            parameters[2] = new Parameter("Range");
        }

        public override string GetReadableString()
        {
            return "true if does not have aura of spell_id with effect effindex";
        }
    }

    public class CONDITION_QUEST_STATUS : SmartCondition
    {
        public CONDITION_QUEST_STATUS()
            : base(43, "")
        {
            parameters[0] = new Parameter("Quest_ID");
            parameters[1] = new Parameter("Status");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    public class CONDITION_HAS_SPELL : SmartCondition
    {
        public CONDITION_HAS_SPELL()
            : base(44, "")
        {
            parameters[0] = new Parameter("spell_id");
            parameters[1] = new Parameter("0");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    public class CONDITION_HAS_SUMMON : SmartCondition
    {
        public CONDITION_HAS_SUMMON()
            : base(45, "true if unit has a summon")
        {
            parameters[0] = new Parameter("Summon_type");
            parameters[1] = new Parameter("Summon_entry");
            parameters[2] = new Parameter("0");
        }

        public override string GetReadableString()
        {
            return "true if unit has a summon";
        }
    }

    public class CONDITION_OWNER_AURA : SmartCondition
    {
        public CONDITION_OWNER_AURA()
            : base(100, "target?        true if owner has aura of spell_id with effect effindex")
        {
            parameters[0] = new Parameter("spell_id");
            parameters[1] = new Parameter("effindex");
            parameters[2] = new Parameter("use");
        }

        public override string GetReadableString()
        {
            return "target?        true if owner has aura of spell_id with effect effindex";
        }
    }


}
