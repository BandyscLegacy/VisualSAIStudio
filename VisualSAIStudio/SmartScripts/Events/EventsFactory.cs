using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio
{
    public static class EventsFactory
    {
        public static SmartEvent Factory(String name)
        {
            switch (name)
            {
                case "SMART_EVENT_UPDATE_IC":
                    return new SMART_EVENT_UPDATE_IC();
                case "SMART_EVENT_UPDATE_OOC":
                    return new SMART_EVENT_UPDATE_OOC();
                case "SMART_EVENT_HEALT_PCT":
                    return new SMART_EVENT_HEALT_PCT();
                case "SMART_EVENT_MANA_PCT":
                    return new SMART_EVENT_MANA_PCT();
                case "SMART_EVENT_AGGRO":
                    return new SMART_EVENT_AGGRO();
                case "SMART_EVENT_KILL":
                    return new SMART_EVENT_KILL();
                case "SMART_EVENT_DEATH":
                    return new SMART_EVENT_DEATH();
                case "SMART_EVENT_EVADE":
                    return new SMART_EVENT_EVADE();
                case "SMART_EVENT_SPELLHIT":
                    return new SMART_EVENT_SPELLHIT();
                case "SMART_EVENT_RANGE":
                    return new SMART_EVENT_RANGE();
                case "SMART_EVENT_OOC_LOS":
                    return new SMART_EVENT_OOC_LOS();
                case "SMART_EVENT_RESPAWN":
                    return new SMART_EVENT_RESPAWN();
                case "SMART_EVENT_TARGET_HEALTH_PCT":
                    return new SMART_EVENT_TARGET_HEALTH_PCT();
                case "SMART_EVENT_VICTIM_CASTING":
                    return new SMART_EVENT_VICTIM_CASTING();
                case "SMART_EVENT_FRIENDLY_HEALTH":
                    return new SMART_EVENT_FRIENDLY_HEALTH();
                case "SMART_EVENT_FRIENDLY_IS_CC":
                    return new SMART_EVENT_FRIENDLY_IS_CC();
                case "SMART_EVENT_FRIENDLY_MISSING_BUFF":
                    return new SMART_EVENT_FRIENDLY_MISSING_BUFF();
                case "SMART_EVENT_SUMMONED_UNIT":
                    return new SMART_EVENT_SUMMONED_UNIT();
                case "SMART_EVENT_TARGET_MANA_PCT":
                    return new SMART_EVENT_TARGET_MANA_PCT();
                case "SMART_EVENT_ACCEPTED_QUEST":
                    return new SMART_EVENT_ACCEPTED_QUEST();
                case "SMART_EVENT_REWARD_QUEST":
                    return new SMART_EVENT_REWARD_QUEST();
                case "SMART_EVENT_REACHED_HOME":
                    return new SMART_EVENT_REACHED_HOME();
                case "SMART_EVENT_RECEIVE_EMOTE":
                    return new SMART_EVENT_RECEIVE_EMOTE();
                case "SMART_EVENT_HAS_AURA":
                    return new SMART_EVENT_HAS_AURA();
                case "SMART_EVENT_TARGET_BUFFED":
                    return new SMART_EVENT_TARGET_BUFFED();
                case "SMART_EVENT_RESET":
                    return new SMART_EVENT_RESET();
                case "SMART_EVENT_IC_LOS":
                    return new SMART_EVENT_IC_LOS();
                case "SMART_EVENT_PASSENGER_BOARDED":
                    return new SMART_EVENT_PASSENGER_BOARDED();
                case "SMART_EVENT_PASSENGER_REMOVED":
                    return new SMART_EVENT_PASSENGER_REMOVED();
                case "SMART_EVENT_CHARMED":
                    return new SMART_EVENT_CHARMED();
                case "SMART_EVENT_CHARMED_TARGET":
                    return new SMART_EVENT_CHARMED_TARGET();
                case "SMART_EVENT_SPELLHIT_TARGET":
                    return new SMART_EVENT_SPELLHIT_TARGET();
                case "SMART_EVENT_DAMAGED":
                    return new SMART_EVENT_DAMAGED();
                case "SMART_EVENT_DAMAGED_TARGET":
                    return new SMART_EVENT_DAMAGED_TARGET();
                case "SMART_EVENT_MOVEMENTINFORM":
                    return new SMART_EVENT_MOVEMENTINFORM();
                case "SMART_EVENT_SUMMON_DESPAWNED":
                    return new SMART_EVENT_SUMMON_DESPAWNED();
                case "SMART_EVENT_CORPSE_REMOVED":
                    return new SMART_EVENT_CORPSE_REMOVED();
                case "SMART_EVENT_AI_INIT":
                    return new SMART_EVENT_AI_INIT();
                case "SMART_EVENT_DATA_SET":
                    return new SMART_EVENT_DATA_SET();
                case "SMART_EVENT_WAYPOINT_START":
                    return new SMART_EVENT_WAYPOINT_START();
                case "SMART_EVENT_WAYPOINT_REACHED":
                    return new SMART_EVENT_WAYPOINT_REACHED();
                case "SMART_EVENT_TRANSPORT_ADDPLAYER":
                    return new SMART_EVENT_TRANSPORT_ADDPLAYER();
                case "SMART_EVENT_TRANSPORT_ADDCREATURE":
                    return new SMART_EVENT_TRANSPORT_ADDCREATURE();
                case "SMART_EVENT_TRANSPORT_REMOVE_PLAYER":
                    return new SMART_EVENT_TRANSPORT_REMOVE_PLAYER();
                case "SMART_EVENT_TRANSPORT_RELOCATE":
                    return new SMART_EVENT_TRANSPORT_RELOCATE();
                case "SMART_EVENT_INSTANCE_PLAYER_ENTER":
                    return new SMART_EVENT_INSTANCE_PLAYER_ENTER();
                case "SMART_EVENT_AREATRIGGER_ONTRIGGER":
                    return new SMART_EVENT_AREATRIGGER_ONTRIGGER();
                case "SMART_EVENT_QUEST_ACCEPTED":
                    return new SMART_EVENT_QUEST_ACCEPTED();
                case "SMART_EVENT_QUEST_OBJ_COPLETETION":
                    return new SMART_EVENT_QUEST_OBJ_COPLETETION();
                case "SMART_EVENT_QUEST_COMPLETION":
                    return new SMART_EVENT_QUEST_COMPLETION();
                case "SMART_EVENT_QUEST_REWARDED":
                    return new SMART_EVENT_QUEST_REWARDED();
                case "SMART_EVENT_QUEST_FAIL":
                    return new SMART_EVENT_QUEST_FAIL();
                case "SMART_EVENT_TEXT_OVER":
                    return new SMART_EVENT_TEXT_OVER();
                case "SMART_EVENT_RECEIVE_HEAL":
                    return new SMART_EVENT_RECEIVE_HEAL();
                case "SMART_EVENT_JUST_SUMMONED":
                    return new SMART_EVENT_JUST_SUMMONED();
                case "SMART_EVENT_WAYPOINT_PAUSED":
                    return new SMART_EVENT_WAYPOINT_PAUSED();
                case "SMART_EVENT_WAYPOINT_RESUMED":
                    return new SMART_EVENT_WAYPOINT_RESUMED();
                case "SMART_EVENT_WAYPOINT_STOPPED":
                    return new SMART_EVENT_WAYPOINT_STOPPED();
                case "SMART_EVENT_WAYPOINT_ENDED":
                    return new SMART_EVENT_WAYPOINT_ENDED();
                case "SMART_EVENT_TIMED_EVENT_TRIGGERED":
                    return new SMART_EVENT_TIMED_EVENT_TRIGGERED();
                case "SMART_EVENT_UPDATE":
                    return new SMART_EVENT_UPDATE();
                case "SMART_EVENT_LINK":
                    return new SMART_EVENT_LINK();
                case "SMART_EVENT_GOSSIP_SELECT":
                    return new SMART_EVENT_GOSSIP_SELECT();
                case "SMART_EVENT_JUST_CREATED":
                    return new SMART_EVENT_JUST_CREATED();
                case "SMART_EVENT_GOSSIP_HELLO":
                    return new SMART_EVENT_GOSSIP_HELLO();
                case "SMART_EVENT_FOLLOW_COMPLETED":
                    return new SMART_EVENT_FOLLOW_COMPLETED();
                case "SMART_EVENT_DUMMY_EFFECT":
                    return new SMART_EVENT_DUMMY_EFFECT();
                case "SMART_EVENT_IS_BEHIND_TARGET":
                    return new SMART_EVENT_IS_BEHIND_TARGET();
                case "SMART_EVENT_GAME_EVENT_START":
                    return new SMART_EVENT_GAME_EVENT_START();
                case "SMART_EVENT_GAME_EVENT_END":
                    return new SMART_EVENT_GAME_EVENT_END();
                case "SMART_EVENT_GO_STATE_CHANGED":
                    return new SMART_EVENT_GO_STATE_CHANGED();
                case "SMART_EVENT_HAS_RESOURCE":
                    return new SMART_EVENT_HAS_RESOURCE();
                case "SMART_EVENT_SUMMONEROROWNER_HEALTH":
                    return new SMART_EVENT_SUMMONEROROWNER_HEALTH();
                case "SMART_EVENT_SUMMONEROROWNER_RESOURCE":
                    return new SMART_EVENT_SUMMONEROROWNER_RESOURCE();
                case "SMART_EVENT_MISSING_BUFF":
                    return new SMART_EVENT_MISSING_BUFF();
                case "SMART_EVENT_ACTION_DONE":
                    return new SMART_EVENT_ACTION_DONE();
                case "SMART_EVENT_ON_SPELLCLICK":
                    return new SMART_EVENT_ON_SPELLCLICK();
                case "SMART_EVENT_GO_EVENT_INFORM":
                    return new SMART_EVENT_GO_EVENT_INFORM();
                case "SMART_EVENT_SMART_TIMER":
                    return new SMART_EVENT_SMART_TIMER();
                case "SMART_EVENT_ON_AREA_CHANGE":
                    return new SMART_EVENT_ON_AREA_CHANGE();
            }
            return null;
        }

        public static SmartEvent Factory(int id)
        {
            switch (id)
            {
                case 0:
                    return new SMART_EVENT_UPDATE_IC();
                case 1:
                    return new SMART_EVENT_UPDATE_OOC();
                case 2:
                    return new SMART_EVENT_HEALT_PCT();
                case 3:
                    return new SMART_EVENT_MANA_PCT();
                case 4:
                    return new SMART_EVENT_AGGRO();
                case 5:
                    return new SMART_EVENT_KILL();
                case 6:
                    return new SMART_EVENT_DEATH();
                case 7:
                    return new SMART_EVENT_EVADE();
                case 8:
                    return new SMART_EVENT_SPELLHIT();
                case 9:
                    return new SMART_EVENT_RANGE();
                case 10:
                    return new SMART_EVENT_OOC_LOS();
                case 11:
                    return new SMART_EVENT_RESPAWN();
                case 12:
                    return new SMART_EVENT_TARGET_HEALTH_PCT();
                case 13:
                    return new SMART_EVENT_VICTIM_CASTING();
                case 14:
                    return new SMART_EVENT_FRIENDLY_HEALTH();
                case 15:
                    return new SMART_EVENT_FRIENDLY_IS_CC();
                case 16:
                    return new SMART_EVENT_FRIENDLY_MISSING_BUFF();
                case 17:
                    return new SMART_EVENT_SUMMONED_UNIT();
                case 18:
                    return new SMART_EVENT_TARGET_MANA_PCT();
                case 19:
                    return new SMART_EVENT_ACCEPTED_QUEST();
                case 20:
                    return new SMART_EVENT_REWARD_QUEST();
                case 21:
                    return new SMART_EVENT_REACHED_HOME();
                case 22:
                    return new SMART_EVENT_RECEIVE_EMOTE();
                case 23:
                    return new SMART_EVENT_HAS_AURA();
                case 24:
                    return new SMART_EVENT_TARGET_BUFFED();
                case 25:
                    return new SMART_EVENT_RESET();
                case 26:
                    return new SMART_EVENT_IC_LOS();
                case 27:
                    return new SMART_EVENT_PASSENGER_BOARDED();
                case 28:
                    return new SMART_EVENT_PASSENGER_REMOVED();
                case 29:
                    return new SMART_EVENT_CHARMED();
                case 30:
                    return new SMART_EVENT_CHARMED_TARGET();
                case 31:
                    return new SMART_EVENT_SPELLHIT_TARGET();
                case 32:
                    return new SMART_EVENT_DAMAGED();
                case 33:
                    return new SMART_EVENT_DAMAGED_TARGET();
                case 34:
                    return new SMART_EVENT_MOVEMENTINFORM();
                case 35:
                    return new SMART_EVENT_SUMMON_DESPAWNED();
                case 36:
                    return new SMART_EVENT_CORPSE_REMOVED();
                case 37:
                    return new SMART_EVENT_AI_INIT();
                case 38:
                    return new SMART_EVENT_DATA_SET();
                case 39:
                    return new SMART_EVENT_WAYPOINT_START();
                case 40:
                    return new SMART_EVENT_WAYPOINT_REACHED();
                case 41:
                    return new SMART_EVENT_TRANSPORT_ADDPLAYER();
                case 42:
                    return new SMART_EVENT_TRANSPORT_ADDCREATURE();
                case 43:
                    return new SMART_EVENT_TRANSPORT_REMOVE_PLAYER();
                case 44:
                    return new SMART_EVENT_TRANSPORT_RELOCATE();
                case 45:
                    return new SMART_EVENT_INSTANCE_PLAYER_ENTER();
                case 46:
                    return new SMART_EVENT_AREATRIGGER_ONTRIGGER();
                case 47:
                    return new SMART_EVENT_QUEST_ACCEPTED();
                case 48:
                    return new SMART_EVENT_QUEST_OBJ_COPLETETION();
                case 49:
                    return new SMART_EVENT_QUEST_COMPLETION();
                case 50:
                    return new SMART_EVENT_QUEST_REWARDED();
                case 51:
                    return new SMART_EVENT_QUEST_FAIL();
                case 52:
                    return new SMART_EVENT_TEXT_OVER();
                case 53:
                    return new SMART_EVENT_RECEIVE_HEAL();
                case 54:
                    return new SMART_EVENT_JUST_SUMMONED();
                case 55:
                    return new SMART_EVENT_WAYPOINT_PAUSED();
                case 56:
                    return new SMART_EVENT_WAYPOINT_RESUMED();
                case 57:
                    return new SMART_EVENT_WAYPOINT_STOPPED();
                case 58:
                    return new SMART_EVENT_WAYPOINT_ENDED();
                case 59:
                    return new SMART_EVENT_TIMED_EVENT_TRIGGERED();
                case 60:
                    return new SMART_EVENT_UPDATE();
                case 61:
                    return new SMART_EVENT_LINK();
                case 62:
                    return new SMART_EVENT_GOSSIP_SELECT();
                case 63:
                    return new SMART_EVENT_JUST_CREATED();
                case 64:
                    return new SMART_EVENT_GOSSIP_HELLO();
                case 65:
                    return new SMART_EVENT_FOLLOW_COMPLETED();
                case 66:
                    return new SMART_EVENT_DUMMY_EFFECT();
                case 67:
                    return new SMART_EVENT_IS_BEHIND_TARGET();
                case 68:
                    return new SMART_EVENT_GAME_EVENT_START();
                case 69:
                    return new SMART_EVENT_GAME_EVENT_END();
                case 70:
                    return new SMART_EVENT_GO_STATE_CHANGED();
                case 71:
                    return new SMART_EVENT_HAS_RESOURCE();
                case 72:
                    return new SMART_EVENT_SUMMONEROROWNER_HEALTH();
                case 73:
                    return new SMART_EVENT_SUMMONEROROWNER_RESOURCE();
                case 74:
                    return new SMART_EVENT_MISSING_BUFF();
                case 75:
                    return new SMART_EVENT_ACTION_DONE();
                case 76:
                    return new SMART_EVENT_ON_SPELLCLICK();
                case 77:
                    return new SMART_EVENT_GO_EVENT_INFORM();
                case 78:
                    return new SMART_EVENT_SMART_TIMER();
                case 79:
                    return new SMART_EVENT_ON_AREA_CHANGE();
            }
            return null;
        }
    }
}
