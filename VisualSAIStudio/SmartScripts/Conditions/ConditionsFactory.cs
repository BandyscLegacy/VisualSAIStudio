using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio
{
    public static class ConditionsFactory
    {
        public static SmartCondition Factory(int id)
        {
            switch (id)
            {
                case 0:
                    return new CONDITION_NONE();
                case 1:
                    return new CONDITION_AURA();
                case 2:
                    return new CONDITION_ITEM();
                case 3:
                    return new CONDITION_ITEM_EQUIPPED();
                case 4:
                    return new CONDITION_ZONEID();
                case 5:
                    return new CONDITION_REPUTATION_RANK();
                case 6:
                    return new CONDITION_TEAM();
                case 7:
                    return new CONDITION_SKILL();
                case 8:
                    return new CONDITION_QUESTREWARDED();
                case 9:
                    return new CONDITION_QUESTTAKEN();
                case 10:
                    return new CONDITION_DRUNKENSTATE();
                case 11:
                    return new CONDITION_WORLD_STATE();
                case 12:
                    return new CONDITION_ACTIVE_EVENT();
                case 13:
                    return new CONDITION_INSTANCE_INFO();
                case 14:
                    return new CONDITION_QUEST_NONE();
                case 15:
                    return new CONDITION_CLASS();
                case 16:
                    return new CONDITION_RACE();
                case 17:
                    return new CONDITION_ACHIEVEMENT();
                case 18:
                    return new CONDITION_TITLE();
                case 19:
                    return new CONDITION_SPAWNMASK();
                case 20:
                    return new CONDITION_GENDER();
                case 21:
                    return new CONDITION_CREATURE_TYPE();
                case 22:
                    return new CONDITION_MAPID();
                case 23:
                    return new CONDITION_AREAID();
                case 25:
                    return new CONDITION_SPELL();
                case 26:
                    return new CONDITION_PHASEMASK();
                case 27:
                    return new CONDITION_LEVEL();
                case 28:
                    return new CONDITION_QUEST_COMPLETE();
                case 29:
                    return new CONDITION_NEAR_CREATURE();
                case 30:
                    return new CONDITION_NEAR_GAMEOBJECT();
                case 31:
                    return new CONDITION_OBJECT_ENTRY();
                case 32:
                    return new CONDITION_TYPE_MASK();
                case 33:
                    return new CONDITION_RELATION_TO();
                case 34:
                    return new CONDITION_REACTION_TO();
                case 35:
                    return new CONDITION_DISTANCE_TO();
                case 36:
                    return new CONDITION_ALIVE();
                case 37:
                    return new CONDITION_HP_VAL();
                case 38:
                    return new CONDITION_HP_PCT();
                case 39:
                    return new CONDITION_SMART_PHASE();
                case 40:
                    return new CONDITION_NOT_NEAR_CREATURE();
                case 41:
                    return new CONDITION_NOT_NEAR_GAMEOBJECT();
                case 42:
                    return new CONDITION_TARGET_NO_AURA_IN_RANGE();
                case 43:
                    return new CONDITION_QUEST_STATUS();
                case 44:
                    return new CONDITION_HAS_SPELL();
                case 45:
                    return new CONDITION_HAS_SUMMON();
                case 100:
                    return new CONDITION_OWNER_AURA();

            }
            return null;
        }

        public static SmartCondition Factory(String name)
        {
            switch (name)
            {
                case "CONDITION_NONE":
                    return new CONDITION_NONE();
                case "CONDITION_AURA":
                    return new CONDITION_AURA();
                case "CONDITION_ITEM":
                    return new CONDITION_ITEM();
                case "CONDITION_ITEM_EQUIPPED":
                    return new CONDITION_ITEM_EQUIPPED();
                case "CONDITION_ZONEID":
                    return new CONDITION_ZONEID();
                case "CONDITION_REPUTATION_RANK":
                    return new CONDITION_REPUTATION_RANK();
                case "CONDITION_TEAM":
                    return new CONDITION_TEAM();
                case "CONDITION_SKILL":
                    return new CONDITION_SKILL();
                case "CONDITION_QUESTREWARDED":
                    return new CONDITION_QUESTREWARDED();
                case "CONDITION_QUESTTAKEN":
                    return new CONDITION_QUESTTAKEN();
                case "CONDITION_DRUNKENSTATE":
                    return new CONDITION_DRUNKENSTATE();
                case "CONDITION_WORLD_STATE":
                    return new CONDITION_WORLD_STATE();
                case "CONDITION_ACTIVE_EVENT":
                    return new CONDITION_ACTIVE_EVENT();
                case "CONDITION_INSTANCE_INFO":
                    return new CONDITION_INSTANCE_INFO();
                case "CONDITION_QUEST_NONE":
                    return new CONDITION_QUEST_NONE();
                case "CONDITION_CLASS":
                    return new CONDITION_CLASS();
                case "CONDITION_RACE":
                    return new CONDITION_RACE();
                case "CONDITION_ACHIEVEMENT":
                    return new CONDITION_ACHIEVEMENT();
                case "CONDITION_TITLE":
                    return new CONDITION_TITLE();
                case "CONDITION_SPAWNMASK":
                    return new CONDITION_SPAWNMASK();
                case "CONDITION_GENDER":
                    return new CONDITION_GENDER();
                case "CONDITION_CREATURE_TYPE":
                    return new CONDITION_CREATURE_TYPE();
                case "CONDITION_MAPID":
                    return new CONDITION_MAPID();
                case "CONDITION_AREAID":
                    return new CONDITION_AREAID();
                case "CONDITION_SPELL":
                    return new CONDITION_SPELL();
                case "CONDITION_PHASEMASK":
                    return new CONDITION_PHASEMASK();
                case "CONDITION_LEVEL":
                    return new CONDITION_LEVEL();
                case "CONDITION_QUEST_COMPLETE":
                    return new CONDITION_QUEST_COMPLETE();
                case "CONDITION_NEAR_CREATURE":
                    return new CONDITION_NEAR_CREATURE();
                case "CONDITION_NEAR_GAMEOBJECT":
                    return new CONDITION_NEAR_GAMEOBJECT();
                case "CONDITION_OBJECT_ENTRY":
                    return new CONDITION_OBJECT_ENTRY();
                case "CONDITION_TYPE_MASK":
                    return new CONDITION_TYPE_MASK();
                case "CONDITION_RELATION_TO":
                    return new CONDITION_RELATION_TO();
                case "CONDITION_REACTION_TO":
                    return new CONDITION_REACTION_TO();
                case "CONDITION_DISTANCE_TO":
                    return new CONDITION_DISTANCE_TO();
                case "CONDITION_ALIVE":
                    return new CONDITION_ALIVE();
                case "CONDITION_HP_VAL":
                    return new CONDITION_HP_VAL();
                case "CONDITION_HP_PCT":
                    return new CONDITION_HP_PCT();
                case "CONDITION_SMART_PHASE":
                    return new CONDITION_SMART_PHASE();
                case "CONDITION_NOT_NEAR_CREATURE":
                    return new CONDITION_NOT_NEAR_CREATURE();
                case "CONDITION_NOT_NEAR_GAMEOBJECT":
                    return new CONDITION_NOT_NEAR_GAMEOBJECT();
                case "CONDITION_TARGET_NO_AURA_IN_RANGE":
                    return new CONDITION_TARGET_NO_AURA_IN_RANGE();
                case "CONDITION_QUEST_STATUS":
                    return new CONDITION_QUEST_STATUS();
                case "CONDITION_HAS_SPELL":
                    return new CONDITION_HAS_SPELL();
                case "CONDITION_HAS_SUMMON":
                    return new CONDITION_HAS_SUMMON();
                case "CONDITION_OWNER_AURA":
                    return new CONDITION_OWNER_AURA();
                case "CONDITION_LOGICAL_OR":
                    return new CONDITION_LOGICAL_OR();
            }
            return null;
        }
    }
}
