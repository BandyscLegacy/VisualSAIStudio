using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public static class ActionsFactory
    {
        public static SmartAction Factory(String id)
        {
            switch (id)
            {
                case "SMART_ACTION_NONE":
                    return new SMART_ACTION_NONE();
                case "SMART_ACTION_TALK":
                    return new SMART_ACTION_TALK();
                case "SMART_ACTION_SET_FACTION":
                    return new SMART_ACTION_SET_FACTION();
                case "SMART_ACTION_MORPH_TO_ENTRY_OR_MODEL":
                    return new SMART_ACTION_MORPH_TO_ENTRY_OR_MODEL();
                case "SMART_ACTION_SOUND":
                    return new SMART_ACTION_SOUND();
                case "SMART_ACTION_PLAY_EMOTE":
                    return new SMART_ACTION_PLAY_EMOTE();
                case "SMART_ACTION_FAIL_QUEST":
                    return new SMART_ACTION_FAIL_QUEST();
                case "SMART_ACTION_ADD_QUEST":
                    return new SMART_ACTION_ADD_QUEST();
                case "SMART_ACTION_SET_REACT_STATE":
                    return new SMART_ACTION_SET_REACT_STATE();
                case "SMART_ACTION_ACTIVATE_GOBJECT":
                    return new SMART_ACTION_ACTIVATE_GOBJECT();
                case "SMART_ACTION_RANDOM_EMOTE":
                    return new SMART_ACTION_RANDOM_EMOTE();
                case "SMART_ACTION_CAST":
                    return new SMART_ACTION_CAST();
                case "SMART_ACTION_SUMMON_CREATURE":
                    return new SMART_ACTION_SUMMON_CREATURE();
                case "SMART_ACTION_THREAT_SINGLE_PCT":
                    return new SMART_ACTION_THREAT_SINGLE_PCT();
                case "SMART_ACTION_THREAT_ALL_PCT":
                    return new SMART_ACTION_THREAT_ALL_PCT();
                case "SMART_ACTION_CALL_AREAEXPLOREDOREVENTHAPPENS":
                    return new SMART_ACTION_CALL_AREAEXPLOREDOREVENTHAPPENS();
                case "SMART_ACTION_SET_INGAME_PHASE_GROUP":
                    return new SMART_ACTION_SET_INGAME_PHASE_GROUP();
                case "SMART_ACTION_SET_EMOTE_STATE":
                    return new SMART_ACTION_SET_EMOTE_STATE();
                case "SMART_ACTION_SET_UNIT_FLAG":
                    return new SMART_ACTION_SET_UNIT_FLAG();
                case "SMART_ACTION_REMOVE_UNIT_FLAG":
                    return new SMART_ACTION_REMOVE_UNIT_FLAG();
                case "SMART_ACTION_AUTO_ATTACK":
                    return new SMART_ACTION_AUTO_ATTACK();
                case "SMART_ACTION_ALLOW_COMBAT_MOVEMENT":
                    return new SMART_ACTION_ALLOW_COMBAT_MOVEMENT();
                case "SMART_ACTION_SET_EVENT_PHASE":
                    return new SMART_ACTION_SET_EVENT_PHASE();
                case "SMART_ACTION_INC_EVENT_PHASE":
                    return new SMART_ACTION_INC_EVENT_PHASE();
                case "SMART_ACTION_EVADE":
                    return new SMART_ACTION_EVADE();
                case "SMART_ACTION_FLEE_FOR_ASSIST":
                    return new SMART_ACTION_FLEE_FOR_ASSIST();
                case "SMART_ACTION_CALL_GROUPEVENTHAPPENS":
                    return new SMART_ACTION_CALL_GROUPEVENTHAPPENS();
                case "SMART_ACTION_REMOVEAURASFROMSPELL":
                    return new SMART_ACTION_REMOVEAURASFROMSPELL();
                case "SMART_ACTION_FOLLOW":
                    return new SMART_ACTION_FOLLOW();
                case "SMART_ACTION_RANDOM_PHASE":
                    return new SMART_ACTION_RANDOM_PHASE();
                case "SMART_ACTION_RANDOM_PHASE_RANGE":
                    return new SMART_ACTION_RANDOM_PHASE_RANGE();
                case "SMART_ACTION_RESET_GOBJECT":
                    return new SMART_ACTION_RESET_GOBJECT();
                case "SMART_ACTION_CALL_KILLEDMONSTER":
                    return new SMART_ACTION_CALL_KILLEDMONSTER();
                case "SMART_ACTION_SET_INST_DATA":
                    return new SMART_ACTION_SET_INST_DATA();
                case "SMART_ACTION_SET_INST_DATA64":
                    return new SMART_ACTION_SET_INST_DATA64();
                case "SMART_ACTION_UPDATE_TEMPLATE":
                    return new SMART_ACTION_UPDATE_TEMPLATE();
                case "SMART_ACTION_DIE":
                    return new SMART_ACTION_DIE();
                case "SMART_ACTION_SET_IN_COMBAT_WITH_ZONE":
                    return new SMART_ACTION_SET_IN_COMBAT_WITH_ZONE();
                case "SMART_ACTION_CALL_FOR_HELP":
                    return new SMART_ACTION_CALL_FOR_HELP();
                case "SMART_ACTION_SET_SHEATH":
                    return new SMART_ACTION_SET_SHEATH();
                case "SMART_ACTION_FORCE_DESPAWN":
                    return new SMART_ACTION_FORCE_DESPAWN();
                case "SMART_ACTION_SET_INVINCIBILITY_HP_LEVEL":
                    return new SMART_ACTION_SET_INVINCIBILITY_HP_LEVEL();
                case "SMART_ACTION_MOUNT_TO_ENTRY_OR_MODEL":
                    return new SMART_ACTION_MOUNT_TO_ENTRY_OR_MODEL();
                case "SMART_ACTION_SET_DATA":
                    return new SMART_ACTION_SET_DATA();
                case "SMART_ACTION_MOVE_FORWARD":
                    return new SMART_ACTION_MOVE_FORWARD();
                case "SMART_ACTION_SET_VISIBILITY":
                    return new SMART_ACTION_SET_VISIBILITY();
                case "SMART_ACTION_SET_ACTIVE":
                    return new SMART_ACTION_SET_ACTIVE();
                case "SMART_ACTION_ATTACK_START":
                    return new SMART_ACTION_ATTACK_START();
                case "SMART_ACTION_SUMMON_GO":
                    return new SMART_ACTION_SUMMON_GO();
                case "SMART_ACTION_KILL_UNIT":
                    return new SMART_ACTION_KILL_UNIT();
                case "SMART_ACTION_ACTIVATE_TAXI":
                    return new SMART_ACTION_ACTIVATE_TAXI();
                case "SMART_ACTION_WP_START":
                    return new SMART_ACTION_WP_START();
                case "SMART_ACTION_WP_PAUSE":
                    return new SMART_ACTION_WP_PAUSE();
                case "SMART_ACTION_WP_STOP":
                    return new SMART_ACTION_WP_STOP();
                case "SMART_ACTION_ADD_ITEM":
                    return new SMART_ACTION_ADD_ITEM();
                case "SMART_ACTION_REMOVE_ITEM":
                    return new SMART_ACTION_REMOVE_ITEM();
                case "SMART_ACTION_INSTALL_AI_TEMPLATE":
                    return new SMART_ACTION_INSTALL_AI_TEMPLATE();
                case "SMART_ACTION_SET_RUN":
                    return new SMART_ACTION_SET_RUN();
                case "SMART_ACTION_SET_FLY":
                    return new SMART_ACTION_SET_FLY();
                case "SMART_ACTION_SET_SWIM":
                    return new SMART_ACTION_SET_SWIM();
                case "SMART_ACTION_TELEPORT":
                    return new SMART_ACTION_TELEPORT();
                case "SMART_ACTION_STORE_VARIABLE_DECIMAL":
                    return new SMART_ACTION_STORE_VARIABLE_DECIMAL();
                case "SMART_ACTION_STORE_TARGET_LIST":
                    return new SMART_ACTION_STORE_TARGET_LIST();
                case "SMART_ACTION_WP_RESUME":
                    return new SMART_ACTION_WP_RESUME();
                case "SMART_ACTION_SET_ORIENTATION":
                    return new SMART_ACTION_SET_ORIENTATION();
                case "SMART_ACTION_CREATE_TIMED_EVENT":
                    return new SMART_ACTION_CREATE_TIMED_EVENT();
                case "SMART_ACTION_PLAYMOVIE":
                    return new SMART_ACTION_PLAYMOVIE();
                case "SMART_ACTION_MOVE_TO_POS":
                    return new SMART_ACTION_MOVE_TO_POS();
                case "SMART_ACTION_RESPAWN_TARGET":
                    return new SMART_ACTION_RESPAWN_TARGET();
                case "SMART_ACTION_EQUIP":
                    return new SMART_ACTION_EQUIP();
                case "SMART_ACTION_CLOSE_GOSSIP":
                    return new SMART_ACTION_CLOSE_GOSSIP();
                case "SMART_ACTION_TRIGGER_TIMED_EVENT":
                    return new SMART_ACTION_TRIGGER_TIMED_EVENT();
                case "SMART_ACTION_REMOVE_TIMED_EVENT":
                    return new SMART_ACTION_REMOVE_TIMED_EVENT();
                case "SMART_ACTION_ADD_AURA":
                    return new SMART_ACTION_ADD_AURA();
                case "SMART_ACTION_OVERRIDE_SCRIPT_BASE_OBJECT":
                    return new SMART_ACTION_OVERRIDE_SCRIPT_BASE_OBJECT();
                case "SMART_ACTION_RESET_SCRIPT_BASE_OBJECT":
                    return new SMART_ACTION_RESET_SCRIPT_BASE_OBJECT();
                case "SMART_ACTION_CALL_SCRIPT_RESET":
                    return new SMART_ACTION_CALL_SCRIPT_RESET();
                case "SMART_ACTION_SET_RANGED_MOVEMENT":
                    return new SMART_ACTION_SET_RANGED_MOVEMENT();
                case "SMART_ACTION_CALL_TIMED_ACTIONLIST":
                    return new SMART_ACTION_CALL_TIMED_ACTIONLIST();
                case "SMART_ACTION_SET_NPC_FLAG":
                    return new SMART_ACTION_SET_NPC_FLAG();
                case "SMART_ACTION_ADD_NPC_FLAG":
                    return new SMART_ACTION_ADD_NPC_FLAG();
                case "SMART_ACTION_REMOVE_NPC_FLAG":
                    return new SMART_ACTION_REMOVE_NPC_FLAG();
                case "SMART_ACTION_SIMPLE_TALK":
                    return new SMART_ACTION_SIMPLE_TALK();
                case "SMART_ACTION_INVOKER_CAST":
                    return new SMART_ACTION_INVOKER_CAST();
                case "SMART_ACTION_CROSS_CAST":
                    return new SMART_ACTION_CROSS_CAST();
                case "SMART_ACTION_CALL_RANDOM_TIMED_ACTIONLIST":
                    return new SMART_ACTION_CALL_RANDOM_TIMED_ACTIONLIST();
                case "SMART_ACTION_CALL_RANDOM_RANGE_TIMED_ACTIONLIST":
                    return new SMART_ACTION_CALL_RANDOM_RANGE_TIMED_ACTIONLIST();
                case "SMART_ACTION_RANDOM_MOVE":
                    return new SMART_ACTION_RANDOM_MOVE();
                case "SMART_ACTION_SET_UNIT_FIELD_BYTES_1":
                    return new SMART_ACTION_SET_UNIT_FIELD_BYTES_1();
                case "SMART_ACTION_REMOVE_UNIT_FIELD_BYTES_1":
                    return new SMART_ACTION_REMOVE_UNIT_FIELD_BYTES_1();
                case "SMART_ACTION_INTERRUPT_SPELL":
                    return new SMART_ACTION_INTERRUPT_SPELL();
                case "SMART_ACTION_SEND_GO_CUSTOM_ANIM":
                    return new SMART_ACTION_SEND_GO_CUSTOM_ANIM();
                case "SMART_ACTION_SET_DYNAMIC_FLAG":
                    return new SMART_ACTION_SET_DYNAMIC_FLAG();
                case "SMART_ACTION_ADD_DYNAMIC_FLAG":
                    return new SMART_ACTION_ADD_DYNAMIC_FLAG();
                case "SMART_ACTION_REMOVE_DYNAMIC_FLAG":
                    return new SMART_ACTION_REMOVE_DYNAMIC_FLAG();
                case "SMART_ACTION_JUMP_TO_POS":
                    return new SMART_ACTION_JUMP_TO_POS();
                case "SMART_ACTION_SEND_GOSSIP_MENU":
                    return new SMART_ACTION_SEND_GOSSIP_MENU();
                case "SMART_ACTION_GO_SET_LOOT_STATE":
                    return new SMART_ACTION_GO_SET_LOOT_STATE();
                case "SMART_ACTION_SEND_TARGET_TO_TARGET":
                    return new SMART_ACTION_SEND_TARGET_TO_TARGET();
                case "SMART_ACTION_SET_HOME_POS":
                    return new SMART_ACTION_SET_HOME_POS();
                case "SMART_ACTION_SET_HEALTH_REGEN":
                    return new SMART_ACTION_SET_HEALTH_REGEN();
                case "SMART_ACTION_SET_ROOT":
                    return new SMART_ACTION_SET_ROOT();
                case "SMART_ACTION_SET_GO_FLAG":
                    return new SMART_ACTION_SET_GO_FLAG();
                case "SMART_ACTION_ADD_GO_FLAG":
                    return new SMART_ACTION_ADD_GO_FLAG();
                case "SMART_ACTION_REMOVE_GO_FLAG":
                    return new SMART_ACTION_REMOVE_GO_FLAG();
                case "SMART_ACTION_SUMMON_CREATURE_GROUP":
                    return new SMART_ACTION_SUMMON_CREATURE_GROUP();
                case "SMART_ACTION_SET_POWER":
                    return new SMART_ACTION_SET_POWER();
                case "SMART_ACTION_ADD_POWER":
                    return new SMART_ACTION_ADD_POWER();
                case "SMART_ACTION_REMOVE_POWER":
                    return new SMART_ACTION_REMOVE_POWER();
                case "SMART_ACTION_GAME_EVENT_STOP":
                    return new SMART_ACTION_GAME_EVENT_STOP();
                case "SMART_ACTION_GAME_EVENT_START":
                    return new SMART_ACTION_GAME_EVENT_START();
                case "SMART_ACTION_START_CLOSEST_WAYPOINT":
                    return new SMART_ACTION_START_CLOSEST_WAYPOINT();
            }
            return null;
        }

        public static SmartAction Factory(int id)
        {
            switch (id)
            {
                case 0:
                    return new SMART_ACTION_NONE();
                case 1:
                    return new SMART_ACTION_TALK();
                case 2:
                    return new SMART_ACTION_SET_FACTION();
                case 3:
                    return new SMART_ACTION_MORPH_TO_ENTRY_OR_MODEL();
                case 4:
                    return new SMART_ACTION_SOUND();
                case 5:
                    return new SMART_ACTION_PLAY_EMOTE();
                case 6:
                    return new SMART_ACTION_FAIL_QUEST();
                case 7:
                    return new SMART_ACTION_ADD_QUEST();
                case 8:
                    return new SMART_ACTION_SET_REACT_STATE();
                case 9:
                    return new SMART_ACTION_ACTIVATE_GOBJECT();
                case 10:
                    return new SMART_ACTION_RANDOM_EMOTE();
                case 11:
                    return new SMART_ACTION_CAST();
                case 12:
                    return new SMART_ACTION_SUMMON_CREATURE();
                case 13:
                    return new SMART_ACTION_THREAT_SINGLE_PCT();
                case 14:
                    return new SMART_ACTION_THREAT_ALL_PCT();
                case 15:
                    return new SMART_ACTION_CALL_AREAEXPLOREDOREVENTHAPPENS();
                case 16:
                    return new SMART_ACTION_SET_INGAME_PHASE_GROUP();
                case 17:
                    return new SMART_ACTION_SET_EMOTE_STATE();
                case 18:
                    return new SMART_ACTION_SET_UNIT_FLAG();
                case 19:
                    return new SMART_ACTION_REMOVE_UNIT_FLAG();
                case 20:
                    return new SMART_ACTION_AUTO_ATTACK();
                case 21:
                    return new SMART_ACTION_ALLOW_COMBAT_MOVEMENT();
                case 22:
                    return new SMART_ACTION_SET_EVENT_PHASE();
                case 23:
                    return new SMART_ACTION_INC_EVENT_PHASE();
                case 24:
                    return new SMART_ACTION_EVADE();
                case 25:
                    return new SMART_ACTION_FLEE_FOR_ASSIST();
                case 26:
                    return new SMART_ACTION_CALL_GROUPEVENTHAPPENS();
                case 28:
                    return new SMART_ACTION_REMOVEAURASFROMSPELL();
                case 29:
                    return new SMART_ACTION_FOLLOW();
                case 30:
                    return new SMART_ACTION_RANDOM_PHASE();
                case 31:
                    return new SMART_ACTION_RANDOM_PHASE_RANGE();
                case 32:
                    return new SMART_ACTION_RESET_GOBJECT();
                case 33:
                    return new SMART_ACTION_CALL_KILLEDMONSTER();
                case 34:
                    return new SMART_ACTION_SET_INST_DATA();
                case 35:
                    return new SMART_ACTION_SET_INST_DATA();
                case 36:
                    return new SMART_ACTION_UPDATE_TEMPLATE();
                case 37:
                    return new SMART_ACTION_DIE();
                case 38:
                    return new SMART_ACTION_SET_IN_COMBAT_WITH_ZONE();
                case 39:
                    return new SMART_ACTION_CALL_FOR_HELP();
                case 40:
                    return new SMART_ACTION_SET_SHEATH();
                case 41:
                    return new SMART_ACTION_FORCE_DESPAWN();
                case 42:
                    return new SMART_ACTION_SET_INVINCIBILITY_HP_LEVEL();
                case 43:
                    return new SMART_ACTION_MOUNT_TO_ENTRY_OR_MODEL();
                case 44:
                    return new SMART_ACTION_SET_INGAME_PHASE_ID();
                case 45:
                    return new SMART_ACTION_SET_DATA();
                case 46:
                    return new SMART_ACTION_MOVE_FORWARD();
                case 47:
                    return new SMART_ACTION_SET_VISIBILITY();
                case 48:
                    return new SMART_ACTION_SET_ACTIVE();
                case 49:
                    return new SMART_ACTION_ATTACK_START();
                case 50:
                    return new SMART_ACTION_SUMMON_GO();
                case 51:
                    return new SMART_ACTION_KILL_UNIT();
                case 52:
                    return new SMART_ACTION_ACTIVATE_TAXI();
                case 53:
                    return new SMART_ACTION_WP_START();
                case 54:
                    return new SMART_ACTION_WP_PAUSE();
                case 55:
                    return new SMART_ACTION_WP_STOP();
                case 56:
                    return new SMART_ACTION_ADD_ITEM();
                case 57:
                    return new SMART_ACTION_REMOVE_ITEM();
                case 58:
                    return new SMART_ACTION_INSTALL_AI_TEMPLATE();
                case 59:
                    return new SMART_ACTION_SET_RUN();
                case 60:
                    return new SMART_ACTION_SET_FLY();
                case 61:
                    return new SMART_ACTION_SET_SWIM();
                case 62:
                    return new SMART_ACTION_TELEPORT();
                case 63:
                    return new SMART_ACTION_STORE_VARIABLE_DECIMAL();
                case 64:
                    return new SMART_ACTION_STORE_TARGET_LIST();
                case 65:
                    return new SMART_ACTION_WP_RESUME();
                case 66:
                    return new SMART_ACTION_SET_ORIENTATION();
                case 67:
                    return new SMART_ACTION_CREATE_TIMED_EVENT();
                case 68:
                    return new SMART_ACTION_PLAYMOVIE();
                case 69:
                    return new SMART_ACTION_MOVE_TO_POS();
                case 70:
                    return new SMART_ACTION_RESPAWN_TARGET();
                case 71:
                    return new SMART_ACTION_EQUIP();
                case 72:
                    return new SMART_ACTION_CLOSE_GOSSIP();
                case 73:
                    return new SMART_ACTION_TRIGGER_TIMED_EVENT();
                case 74:
                    return new SMART_ACTION_REMOVE_TIMED_EVENT();
                case 75:
                    return new SMART_ACTION_ADD_AURA();
                case 76:
                    return new SMART_ACTION_OVERRIDE_SCRIPT_BASE_OBJECT();
                case 77:
                    return new SMART_ACTION_RESET_SCRIPT_BASE_OBJECT();
                case 78:
                    return new SMART_ACTION_CALL_SCRIPT_RESET();
                case 79:
                    return new SMART_ACTION_SET_RANGED_MOVEMENT();
                case 80:
                    return new SMART_ACTION_CALL_TIMED_ACTIONLIST();
                case 81:
                    return new SMART_ACTION_SET_NPC_FLAG();
                case 82:
                    return new SMART_ACTION_ADD_NPC_FLAG();
                case 83:
                    return new SMART_ACTION_REMOVE_NPC_FLAG();
                case 84:
                    return new SMART_ACTION_SIMPLE_TALK();
                case 85:
                    return new SMART_ACTION_INVOKER_CAST();
                case 86:
                    return new SMART_ACTION_CROSS_CAST();
                case 87:
                    return new SMART_ACTION_CALL_RANDOM_TIMED_ACTIONLIST();
                case 88:
                    return new SMART_ACTION_CALL_RANDOM_RANGE_TIMED_ACTIONLIST();
                case 89:
                    return new SMART_ACTION_RANDOM_MOVE();
                case 90:
                    return new SMART_ACTION_SET_UNIT_FIELD_BYTES_1();
                case 91:
                    return new SMART_ACTION_REMOVE_UNIT_FIELD_BYTES_1();
                case 92:
                    return new SMART_ACTION_INTERRUPT_SPELL();
                case 93:
                    return new SMART_ACTION_SEND_GO_CUSTOM_ANIM();
                case 94:
                    return new SMART_ACTION_SET_DYNAMIC_FLAG();
                case 95:
                    return new SMART_ACTION_ADD_DYNAMIC_FLAG();
                case 96:
                    return new SMART_ACTION_REMOVE_DYNAMIC_FLAG();
                case 97:
                    return new SMART_ACTION_JUMP_TO_POS();
                case 98:
                    return new SMART_ACTION_SEND_GOSSIP_MENU();
                case 99:
                    return new SMART_ACTION_GO_SET_LOOT_STATE();
                case 100:
                    return new SMART_ACTION_SEND_TARGET_TO_TARGET();
                case 101:
                    return new SMART_ACTION_SET_HOME_POS();
                case 102:
                    return new SMART_ACTION_SET_HEALTH_REGEN();
                case 103:
                    return new SMART_ACTION_SET_ROOT();
                case 104:
                    return new SMART_ACTION_SET_GO_FLAG();
                case 105:
                    return new SMART_ACTION_ADD_GO_FLAG();
                case 106:
                    return new SMART_ACTION_REMOVE_GO_FLAG();
                case 107:
                    return new SMART_ACTION_SUMMON_CREATURE_GROUP();
                case 108:
                    return new SMART_ACTION_SET_POWER();
                case 109:
                    return new SMART_ACTION_ADD_POWER();
                case 110:
                    return new SMART_ACTION_REMOVE_POWER();
                case 111:
                    return new SMART_ACTION_GAME_EVENT_STOP();
                case 112:
                    return new SMART_ACTION_GAME_EVENT_START();
                case 113:
                    return new SMART_ACTION_START_CLOSEST_WAYPOINT();
            }
            return null;
        }
    }
}
