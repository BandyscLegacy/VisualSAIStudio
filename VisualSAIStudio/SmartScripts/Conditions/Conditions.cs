using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{
    public class CONDITION_LOGICAL_OR : SmartCondition
    {
        public CONDITION_LOGICAL_OR(): base(-1, "CONDITION_LOGICAL_OR", "or") { }

        public override string GetReadableString()
        {
            return "Or";
        }
    }

    public class CONDITION_NONE : SmartCondition
    {
        public CONDITION_NONE(): base(0, "CONDITION_NONE", "always true")
        {
        }

        public override string GetReadableString()
        {
            return "If (true)";
        }
    }

    public class CONDITION_AURA : SmartCondition
    {
        public CONDITION_AURA(): base(1, "CONDITION_AURA", "true if player (or target, if value3) has aura of spell_id with effect effindex")
        {
            SetParameter(0, new SpellParameter("Spell"));
            SetParameter(1, new SwitchParameter("Effect index", "Each spell has 'effects', you can see them in Spell Work", new [] {    "EFFECT_0", "EFFECT_1" ,"EFFECT_2"}));
        }

        public override string GetReadableString()
        {
            return "If {target} has aura of spell {pram1}, effect {pram2}";
        }
    }

    public class CONDITION_ITEM : SmartCondition
    {
        public CONDITION_ITEM(): base(2, "CONDITION_ITEM", "true if has #count of item_ids (if 'bank' is set it searches in bank slots too)")
        {
            SetParameter(0, new ItemParameter("Item", "Item id to check"));
            SetParameter(1, new Parameter("Amount", "Minimum amount of items to mark condition as met"));
            SetParameter(2, new Parameter("Check bank", "If true, core will check items in bank too"));
        }

        public override string GetReadableString()
        {
            return "If {target} has {pram2} of item {pram1} in backpack{pram3value:choose(0):| or in bank}";
        }
    }

    public class CONDITION_ITEM_EQUIPPED : SmartCondition
    {
        public CONDITION_ITEM_EQUIPPED(): base(3, "CONDITION_ITEM_EQUIPPED", "true if has item_id equipped")
        {
            SetParameter(0, new ItemParameter("Item", "Item id to check (this condition applies only to players!)"));
        }

        public override string GetReadableString()
        {
            return "If {target} has equiped item {pram1}";
        }
    }

    public class CONDITION_ZONEID : SmartCondition
    {
        public CONDITION_ZONEID(): base(4, "CONDITION_ZONEID", "true if in zone_id")
        {
            SetParameter(0, new ZoneAreaParameter("Zone"));
        }

        public override string GetReadableString()
        {
            return "If {target} in zone {pram1}";
        }
    }

    public class CONDITION_REPUTATION_RANK : SmartCondition
    {
        public static Dictionary<int, SelectOption> ranks = new Dictionary<int, SelectOption>()
        {
            {0, new SelectOption("REP_HATED")},
            {1, new SelectOption("REP_HOSTILE")},
            {2, new SelectOption("REP_UNFRIENDLY")},
            {3, new SelectOption("REP_NEUTRAL")},
            {4, new SelectOption("REP_FRIENDLY")},
            {5, new SelectOption("REP_HONORED")},
            {6, new SelectOption("REP_REVERED")},
            {7, new SelectOption("REP_EXALTED")},
        };
        public CONDITION_REPUTATION_RANK(): base(5, "CONDITION_REPUTATION_RANK", "true if has min_rank for faction_id")
        {
            SetParameter(0, new Parameter("Faction", "Facion id to test"));
            SetParameter(1, new SwitchParameter("Minimum rank", ranks));
        }

        public override string GetReadableString()
        {
            return "If {target} has at least reputation {pram2} to faction {pram1}";
        }
    }

    public class CONDITION_TEAM : SmartCondition
    {
        public static Dictionary<int, SelectOption> teams = new Dictionary<int,SelectOption>()
        {
            {67, new SelectOption("Horde")},
            {469, new SelectOption("Alliance")},
        };
        public CONDITION_TEAM(): base(6, "CONDITION_TEAM", "469 - Alliance, 67 - Horde)")
        {
            SetParameter(0, new SwitchParameter("Team", "Team to test. Only applies to players", teams));
        }

        public override string GetReadableString()
        {
            return "If {target} belongs to {pram1}";
        }
    }

    public class CONDITION_SKILL : SmartCondition
    {
        public CONDITION_SKILL(): base(7, "CONDITION_SKILL", "true if has skill_value for skill_id")
        {
            SetParameter(0, new SkillParameter("Skill", "Skill id to check", true));
            SetParameter(1, new Parameter("Minimum skill value"));
        }

        public override string GetReadableString()
        {
            return "If {target} has at least value {pram2} for skill {pram1}";
        }
    }

    public class CONDITION_QUESTREWARDED : SmartCondition
    {
        public CONDITION_QUESTREWARDED(): base(8, "CONDITION_QUESTREWARDED", "true if quest_id was rewarded before")
        {
            SetParameter(0, new QuestParameter("Quest", "Quest id to check", true));
        }

        public override string GetReadableString()
        {
            return "If {target} has quest {pram1} rewarded";
        }
    }

    public class CONDITION_QUESTTAKEN : SmartCondition
    {
        public CONDITION_QUESTTAKEN(): base(9, "CONDITION_QUESTTAKEN", "true while quest active")
        {
            SetParameter(0, new QuestParameter("Quest", "Quest id to check", true));
        }

        public override string GetReadableString()
        {
           return "If {target} has quest {pram1} active";
        }
    }

    public class CONDITION_DRUNKENSTATE : SmartCondition
    {
        public CONDITION_DRUNKENSTATE(): base(10, "CONDITION_DRUNKENSTATE", "true if player is drunk enough")
        {
            SetParameter(0, new SwitchParameter("Drunken State", new [] {"DRUNKEN_SOBER", "DRUNKEN_TIPSY", "DRUNKEN_DRUNK","DRUNKEN_SMASHED"}));
        }

        public override string GetReadableString()
        {
            return "If {target} has drunk state {pram1}";
        }
    }

    public class CONDITION_WORLD_STATE : SmartCondition
    {
        public CONDITION_WORLD_STATE(): base(11, "CONDITION_WORLD_STATE", "true if world has the value for the index")
        {
            SetParameter(0, new Parameter("Variable index"));
            SetParameter(1, new Parameter("Value"));
        }

        public override string GetReadableString()
        {
            return "If world variable {pram1} is set to {pram2}";
        }
    }

    public class CONDITION_ACTIVE_EVENT : SmartCondition
    {
        public CONDITION_ACTIVE_EVENT(): base(12, "CONDITION_ACTIVE_EVENT", "true if event is active")
        {
            SetParameter(0, new Parameter("event_id"));
        }

        public override string GetReadableString()
        {
            return "If event " +parameters[0]+ "is active";
        }
    }

    public class CONDITION_INSTANCE_INFO : SmartCondition
    {
        public CONDITION_INSTANCE_INFO(): base(13, "CONDITION_INSTANCE_INFO", "true if the instance info defined by type (enum InstanceInfo) equals data.")
        {
            SetParameter(0, new Parameter("entry"));
            SetParameter(1, new Parameter("data"));
            SetParameter(2, new Parameter("type"));
        }

        public override string GetReadableString()
        {
            return "true if the instance info defined by type (enum InstanceInfo) equals data.";
        }
    }

    public class CONDITION_QUEST_NONE : SmartCondition
    {
        public CONDITION_QUEST_NONE(): base(14, "CONDITION_QUEST_NONE", "true if doesn't have quest saved")
        {
            SetParameter(0, new Parameter("quest_id"));
        }

        public override string GetReadableString()
        {
            return "If player has quest "+parameters[0]+" neither rewarded nor active";
        }
    }

    public class CONDITION_CLASS : SmartCondition
    {
        public CONDITION_CLASS(): base(15, "CONDITION_CLASS", "true if player's class is equal to class")
        {
            SetParameter(0, new Parameter("class"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if player's class is equal to class";
        }
    }

    public class CONDITION_RACE : SmartCondition
    {
        public CONDITION_RACE(): base(16, "CONDITION_RACE", "true if player's race is equal to race")
        {
            SetParameter(0, new Parameter("race"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if player's race is equal to race";
        }
    }

    public class CONDITION_ACHIEVEMENT : SmartCondition
    {
        public CONDITION_ACHIEVEMENT(): base(17, "CONDITION_ACHIEVEMENT", "true if achievement is complete")
        {
            SetParameter(0, new Parameter("achievement_id"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if achievement is complete";
        }
    }

    public class CONDITION_TITLE : SmartCondition
    {
        public CONDITION_TITLE(): base(18, "CONDITION_TITLE", "0                  true if player has title")
        {
            SetParameter(0, new Parameter("title"));
            SetParameter(1, new Parameter("id"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "0                  true if player has title";
        }
    }

    public class CONDITION_SPAWNMASK : SmartCondition
    {
        public CONDITION_SPAWNMASK(): base(19, "CONDITION_SPAWNMASK", "true if in spawnMask")
        {
            SetParameter(0, new Parameter("spawnMask"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if in spawnMask";
        }
    }

    public class CONDITION_GENDER : SmartCondition
    {
        public CONDITION_GENDER(): base(20, "CONDITION_GENDER", "true if player's gender is equal to gender")
        {
            SetParameter(0, new Parameter("gender"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if player's gender is equal to gender";
        }
    }

    public class CONDITION_CREATURE_TYPE : SmartCondition
    {
        public CONDITION_CREATURE_TYPE(): base(21, "CONDITION_CREATURE_TYPE", "true if creature/unit is specific type..")
        {
            SetParameter(0, new Parameter("CreaturType"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if creature/unit is specific type..";
        }
    }

    public class CONDITION_MAPID : SmartCondition
    {
        public CONDITION_MAPID(): base(22, "CONDITION_MAPID", "true if in map_id")
        {
            SetParameter(0, new Parameter("map_id"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if in map_id";
        }
    }

    public class CONDITION_AREAID : SmartCondition
    {
        public CONDITION_AREAID(): base(23, "CONDITION_AREAID", "true if in area_id")
        {
            SetParameter(0, new ZoneAreaParameter("area_id"));
        }

        public override string GetReadableString()
        {
            return "If target in area "+parameters[0];
        }
    }

    public class CONDITION_SPELL : SmartCondition
    {
        public CONDITION_SPELL(): base(25, "CONDITION_SPELL", "true if player has learned spell")
        {
            SetParameter(0, new Parameter("spell_id"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if player has learned spell";
        }
    }

    public class CONDITION_PHASEMASK : SmartCondition
    {
        public CONDITION_PHASEMASK(): base(26, "CONDITION_PHASEMASK", "true if object is in phasemask")
        {
            SetParameter(0, new Parameter("phasemask"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if object is in phasemask";
        }
    }

    public class CONDITION_LEVEL : SmartCondition
    {
        public CONDITION_LEVEL(): base(27, "CONDITION_LEVEL", "true if unit's level is equal to param1 (param2 can modify the statement)")
        {
            SetParameter(0, new Parameter("level"));
            SetParameter(1, new Parameter("ComparisonType"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if unit's level is equal to param1 (param2 can modify the statement)";
        }
    }

    public class CONDITION_QUEST_COMPLETE : SmartCondition
    {
        public CONDITION_QUEST_COMPLETE(): base(28, "CONDITION_QUEST_COMPLETE", "true if player has quest_id with all objectives complete, but not yet rewarded")
        {
            SetParameter(0, new Parameter("quest_id"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if player has quest_id with all objectives complete, but not yet rewarded";
        }
    }

    public class CONDITION_NEAR_CREATURE : SmartCondition
    {
        public CONDITION_NEAR_CREATURE(): base(29, "CONDITION_NEAR_CREATURE", "true if there is a creature of entry in range")
        {
            SetParameter(0, new Parameter("creature_entry"));
            SetParameter(1, new Parameter("distance"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if there is a creature of entry in range";
        }
    }

    public class CONDITION_NEAR_GAMEOBJECT : SmartCondition
    {
        public CONDITION_NEAR_GAMEOBJECT(): base(30, "CONDITION_NEAR_GAMEOBJECT", "true if there is a gameobject of entry in range")
        {
            SetParameter(0, new Parameter("gameobject_entry"));
            SetParameter(1, new Parameter("distance"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if there is a gameobject of entry in range";
        }
    }

    public class CONDITION_OBJECT_ENTRY : SmartCondition
    {
        public CONDITION_OBJECT_ENTRY(): base(31, "CONDITION_OBJECT_ENTRY", "true if object is type TypeID and the entry is 0 or matches entry of the object")
        {
            SetParameter(0, new Parameter("TypeID"));
            SetParameter(1, new Parameter("entry"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if object is type TypeID and the entry is 0 or matches entry of the object";
        }
    }

    public class CONDITION_TYPE_MASK : SmartCondition
    {
        public CONDITION_TYPE_MASK(): base(32, "CONDITION_TYPE_MASK", "true if object is type object's TypeMask matches provided TypeMask")
        {
            SetParameter(0, new Parameter("TypeMask"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if object is type object's TypeMask matches provided TypeMask";
        }
    }

    public class CONDITION_RELATION_TO : SmartCondition
    {
        public CONDITION_RELATION_TO(): base(33, "CONDITION_RELATION_TO", "true if object is in given relation with object specified by ConditionTarget")
        {
            SetParameter(0, new Parameter("ConditionTarget"));
            SetParameter(1, new Parameter("RelationType"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if object is in given relation with object specified by ConditionTarget";
        }
    }

    public class CONDITION_REACTION_TO : SmartCondition
    {
        public CONDITION_REACTION_TO(): base(34, "CONDITION_REACTION_TO", "true if object's reaction matches rankMask object specified by ConditionTarget")
        {
            SetParameter(0, new Parameter("ConditionTarget"));
            SetParameter(1, new Parameter("rankMask"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if object's reaction matches rankMask object specified by ConditionTarget";
        }
    }

    public class CONDITION_DISTANCE_TO : SmartCondition
    {
        public CONDITION_DISTANCE_TO(): base(35, "CONDITION_DISTANCE_TO", "true if object and ConditionTarget are within distance given by parameters")
        {
            SetParameter(0, new Parameter("ConditionTarget"));
            SetParameter(1, new Parameter("distance"));
            SetParameter(2, new Parameter("ComparisonType"));
        }

        public override string GetReadableString()
        {
            return "true if object and ConditionTarget are within distance given by parameters";
        }
    }

    public class CONDITION_ALIVE : SmartCondition
    {
        public CONDITION_ALIVE(): base(36, "CONDITION_ALIVE", "true if unit is alive")
        {
            SetParameter(0, new Parameter("0"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if unit is alive";
        }
    }

    public class CONDITION_HP_VAL : SmartCondition
    {
        public CONDITION_HP_VAL(): base(37, "CONDITION_HP_VAL", "true if unit's hp matches given value")
        {
            SetParameter(0, new Parameter("hpVal"));
            SetParameter(1, new Parameter("ComparisonType"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if unit's hp matches given value";
        }
    }

    public class CONDITION_HP_PCT : SmartCondition
    {
        public CONDITION_HP_PCT(): base(38, "CONDITION_HP_PCT", "true if unit's hp matches given pct")
        {
            SetParameter(0, new Parameter("hpPct"));
            SetParameter(1, new Parameter("ComparisonType"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if unit's hp matches given pct";
        }
    }

    public class CONDITION_SMART_PHASE : SmartCondition
    {
        public CONDITION_SMART_PHASE(): base(39, "CONDITION_SMART_PHASE", "true if there is a gameobject of entry in range")
        {
            SetParameter(0, new Parameter("smart_script_phase"));
            SetParameter(1, new Parameter("id"));
            SetParameter(2, new Parameter("referenceID"));
        }

        public override string GetReadableString()
        {
            return "true if there is a gameobject of entry in range";
        }
    }

    public class CONDITION_NOT_NEAR_CREATURE : SmartCondition
    {
        public CONDITION_NOT_NEAR_CREATURE(): base(40, "CONDITION_NOT_NEAR_CREATURE", "true if there isn't a creature of entry in range")
        {
            SetParameter(0, new Parameter("creature_entry"));
            SetParameter(1, new Parameter("distance"));
            SetParameter(2, new Parameter("referenceID"));
        }

        public override string GetReadableString()
        {
            return "true if there isn't a creature of entry in range";
        }
    }

    public class CONDITION_NOT_NEAR_GAMEOBJECT : SmartCondition
    {
        public CONDITION_NOT_NEAR_GAMEOBJECT(): base(41, "CONDITION_NOT_NEAR_GAMEOBJECT", "true if there isn't a gameobject of entry in range")
        {
            SetParameter(0, new Parameter("gameobject_entry"));
            SetParameter(1, new Parameter("distance"));
            SetParameter(2, new Parameter("referenceID"));
        }

        public override string GetReadableString()
        {
            return "true if there isn't a gameobject of entry in range";
        }
    }

    public class CONDITION_TARGET_NO_AURA_IN_RANGE : SmartCondition
    {
        public CONDITION_TARGET_NO_AURA_IN_RANGE(): base(42, "CONDITION_TARGET_NO_AURA_IN_RANGE", "true if does not have aura of spell_id with effect effindex")
        {
            SetParameter(0, new Parameter("spell_id"));
            SetParameter(1, new Parameter("TargetEntry"));
            SetParameter(2, new Parameter("Range"));
        }

        public override string GetReadableString()
        {
            return "true if does not have aura of spell_id with effect effindex";
        }
    }

    public class CONDITION_QUEST_STATUS : SmartCondition
    {
        public CONDITION_QUEST_STATUS(): base(43, "CONDITION_QUEST_STATUS", "")
        {
            SetParameter(0, new Parameter("Quest_ID"));
            SetParameter(1, new Parameter("Status"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    public class CONDITION_HAS_SPELL : SmartCondition
    {
        public CONDITION_HAS_SPELL(): base(44, "CONDITION_HAS_SPELL", "")
        {
            SetParameter(0, new Parameter("spell_id"));
            SetParameter(1, new Parameter("0"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    public class CONDITION_HAS_SUMMON : SmartCondition
    {
        public CONDITION_HAS_SUMMON(): base(45, "CONDITION_HAS_SUMMON", "true if unit has a summon")
        {
            SetParameter(0, new Parameter("Summon_type"));
            SetParameter(1, new Parameter("Summon_entry"));
            SetParameter(2, new Parameter("0"));
        }

        public override string GetReadableString()
        {
            return "true if unit has a summon";
        }
    }

    public class CONDITION_OWNER_AURA : SmartCondition
    {
        public CONDITION_OWNER_AURA(): base(100, "CONDITION_OWNER_AURA", "target?        true if owner has aura of spell_id with effect effindex")
        {
            SetParameter(0, new Parameter("spell_id"));
            SetParameter(1, new Parameter("effindex"));
            SetParameter(2, new Parameter("use"));
        }

        public override string GetReadableString()
        {
            return "target?        true if owner has aura of spell_id with effect effindex";
        }
    }


}
