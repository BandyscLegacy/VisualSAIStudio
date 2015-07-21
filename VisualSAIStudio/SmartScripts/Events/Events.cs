using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    class SMART_EVENT_UPDATE_IC : SmartEvent
    {
        public SMART_EVENT_UPDATE_IC() : base()
        {
            ID = 0;
            SetParameter(0, new Parameter("Initial Min", "Minimum time to trigger event (only for the first time)"));
            SetParameter(1, new Parameter("Initial Max", "Maximum time to trigger event (only for the first time)"));
            SetParameter(2, new Parameter("Repeat Min", "Minimum time to trigger event (after first time)"));
            SetParameter(3, new Parameter("Repeat Max", "Maximum time to trigger event (after first time)"));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0 && parameters[1].GetValue() == 0 && parameters[2].GetValue() == 0 && parameters[3].GetValue() == 0)
                return "Never";
            else if (parameters[1].GetValue() < parameters[0].GetValue() || parameters[3].GetValue() < parameters[2].GetValue())
                return "Invalid! (max < min!)";
            else if (parameters[0].GetValue() == 0 && parameters[1].GetValue() == 0)
                return "When in combat and timer between " + parameters[2].ToString() + " and " + parameters[3].ToString();
            else if (parameters[2].GetValue() == 0 && parameters[3].GetValue() == 0)
                return "When in combat and timer between " + parameters[0].GetValue() + " and " + parameters[1].GetValue();
            else
                return "When in combat and timer at the begining between " + parameters[0].ToString() + " and " + parameters[1].ToString() + " (and then repeat between " + parameters[2].ToString() + " and " + parameters[3].ToString() + ")";
        }
    }

    class SMART_EVENT_UPDATE_OOC : SmartEvent
    {
        public SMART_EVENT_UPDATE_OOC() : base()
        {
            ID = 1;
            SetParameter(0, new Parameter("InitialMin"));
            SetParameter(1, new Parameter("InitialMax"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0 && parameters[1].GetValue() == 0 && parameters[2].GetValue() == 0 && parameters[3].GetValue() == 0)
                return "Never";
            else if (parameters[1].GetValue() < parameters[0].GetValue() || parameters[3].GetValue() < parameters[2].GetValue())
                return "Invalid! (max < min!)";
            else if (parameters[0].GetValue() == 0 && parameters[1].GetValue() == 0)
                return "When out of combat and timer between " + parameters[2].ToString() + " and " + parameters[3].ToString();
            else if (parameters[2].GetValue() == 0 && parameters[3].GetValue() == 0)
                return "When out of combat and timer between " + parameters[0].GetValue() + " and " + parameters[1].GetValue();
            else
                return "When out of combat and timer at the begining between " + parameters[0].ToString() + " and " + parameters[1].ToString() + " (and then repeat between " + parameters[2].ToString() + " and " + parameters[3].ToString() + ")";
        }
    }

    class SMART_EVENT_HEALT_PCT : SmartEvent
    {
        public SMART_EVENT_HEALT_PCT() : base()
        {
            ID = 2;
            SetParameter(0, new Parameter("HPMin%"));
            SetParameter(1, new Parameter("HPMax%"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_MANA_PCT : SmartEvent
    {
        public SMART_EVENT_MANA_PCT() : base()
        {
            ID = 3;
            SetParameter(0, new Parameter("ManaMin%"));
            SetParameter(1, new Parameter("ManaMax%"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_AGGRO : SmartEvent
    {
        public SMART_EVENT_AGGRO() : base()
        {
            ID = 4;
        }

        public override string GetReadableString()
        {
            return "On aggro";
        }
    }

    class SMART_EVENT_KILL : SmartEvent
    {
        public SMART_EVENT_KILL() : base()
        {
            ID = 5;
            SetParameter(0, new Parameter("CooldownMin0"));
            SetParameter(1, new Parameter("CooldownMax1"));
            SetParameter(2, new Parameter("playerOnly2"));
            SetParameter(3, new Parameter("else creature entry3"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_DEATH : SmartEvent
    {
        public SMART_EVENT_DEATH() : base()
        {
            ID = 6;
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_EVADE : SmartEvent
    {
        public SMART_EVENT_EVADE() : base()
        {
            ID = 7;
        }

        public override string GetReadableString()
        {
            return "On evade";
        }
    }

    class SMART_EVENT_SPELLHIT : SmartEvent
    {
        public SMART_EVENT_SPELLHIT() : base()
        {
            ID = 8;
            SetParameter(0, new SpellParameter("SpellID"));
            SetParameter(1, new Parameter("School"));
            SetParameter(2, new Parameter("CooldownMin"));
            SetParameter(3, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "On spell "+parameters[0].ToString() +" hit";
        }
    }

    class SMART_EVENT_RANGE : SmartEvent
    {
        public SMART_EVENT_RANGE() : base()
        {
            ID = 9;
            SetParameter(0, new Parameter("MinDist"));
            SetParameter(1, new Parameter("MaxDist"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "When target is between distance " + parameters[0].ToString() + " and " + parameters[1].ToString() + " (check every " + parameters[2].ToString() + " and " + parameters[3].ToString() + ")";
        }
    }

    class SMART_EVENT_OOC_LOS : SmartEvent
    {
        public SMART_EVENT_OOC_LOS() : base()
        {
            ID = 10;
            SetParameter(0, new Parameter("NoHostile"));
            SetParameter(1, new Parameter("MaxRnage"));
            SetParameter(2, new Parameter("CooldownMin"));
            SetParameter(3, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_RESPAWN : SmartEvent
    {
        public SMART_EVENT_RESPAWN() : base()
        {
            ID = 11;
            SetParameter(0, new Parameter("type"));
            SetParameter(1, new Parameter("MapId"));
            SetParameter(2, new Parameter("ZoneId"));
        }

        public override string GetReadableString()
        {
            return "On respawn";
        }
    }

    class SMART_EVENT_TARGET_HEALTH_PCT : SmartEvent
    {
        public SMART_EVENT_TARGET_HEALTH_PCT() : base()
        {
            ID = 12;
            SetParameter(0, new Parameter("HPMin%"));
            SetParameter(1, new Parameter("HPMax%"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_VICTIM_CASTING : SmartEvent
    {
        public SMART_EVENT_VICTIM_CASTING() : base()
        {
            ID = 13;
            SetParameter(0, new Parameter("RepeatMin"));
            SetParameter(1, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_FRIENDLY_HEALTH : SmartEvent
    {
        public SMART_EVENT_FRIENDLY_HEALTH() : base()
        {
            ID = 14;
            SetParameter(0, new Parameter("HPDeficit"));
            SetParameter(1, new Parameter("Radius"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_FRIENDLY_IS_CC : SmartEvent
    {
        public SMART_EVENT_FRIENDLY_IS_CC() : base()
        {
            ID = 15;
            SetParameter(0, new Parameter("Radius"));
            SetParameter(1, new Parameter("RepeatMin"));
            SetParameter(2, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_FRIENDLY_MISSING_BUFF : SmartEvent
    {
        public SMART_EVENT_FRIENDLY_MISSING_BUFF() : base()
        {
            ID = 16;
            SetParameter(0, new SpellParameter("SpellId"));
            SetParameter(1, new Parameter("Radius"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_SUMMONED_UNIT : SmartEvent
    {
        public SMART_EVENT_SUMMONED_UNIT() : base()
        {
            ID = 17;
            SetParameter(0, new CreatureParameter("CreatureId(0 all)"));
            SetParameter(1, new Parameter("CooldownMin"));
            SetParameter(2, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0)
                return "When just summoned any unit";
            return "When just summoned creature with entry "+parameters[0].ToString();
        }
    }

    class SMART_EVENT_TARGET_MANA_PCT : SmartEvent
    {
        public SMART_EVENT_TARGET_MANA_PCT() : base()
        {
            ID = 18;
            SetParameter(0, new Parameter("ManaMin%"));
            SetParameter(1, new Parameter("ManaMax%"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_ACCEPTED_QUEST : SmartEvent
    {
        public SMART_EVENT_ACCEPTED_QUEST() : base()
        {
            ID = 19;
            SetParameter(0, new QuestParameter("Quest ID", "Triggers when quest is accepted (if 0 - any quest)"));
        }

        public override string GetReadableString()
        {
            return "When quest " + parameters[0].ToString() + " accepted";
        }
    }

    class SMART_EVENT_REWARD_QUEST : SmartEvent
    {
        public SMART_EVENT_REWARD_QUEST() : base()
        {
            ID = 20;
            SetParameter(0, new QuestParameter("QuestID(0any)"));
        }

        public override string GetReadableString()
        {
            return "When quest "+parameters[0].ToString() +" rewarded";
        }
    }

    class SMART_EVENT_REACHED_HOME : SmartEvent
    {
        public SMART_EVENT_REACHED_HOME() : base()
        {
            ID = 21;
        }

        public override string GetReadableString()
        {
            return "On home reached";
        }
    }

    class SMART_EVENT_RECEIVE_EMOTE : SmartEvent
    {
        public SMART_EVENT_RECEIVE_EMOTE() : base()
        {
            ID = 22;
            SetParameter(0, new EmoteParameter("EmoteId"));
            SetParameter(1, new Parameter("CooldownMin"));
            SetParameter(2, new Parameter("CooldownMax"));
            SetParameter(3, new Parameter("condition, val1, val2, val3"));
        }

        public override string GetReadableString()
        {
            return "On emote "+parameters[0].ToString() +" received";
        }
    }

    class SMART_EVENT_HAS_AURA : SmartEvent
    {
        public SMART_EVENT_HAS_AURA() : base()
        {
            ID = 23;
            SetParameter(0, new SpellParameter("SpellID"));
            SetParameter(1, new Parameter("Number of Time STacked"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "When has aura "+parameters[0].ToString();
        }
    }

    class SMART_EVENT_TARGET_BUFFED : SmartEvent
    {
        public SMART_EVENT_TARGET_BUFFED() : base()
        {
            ID = 24;
            SetParameter(0, new SpellParameter("SpellID"));
            SetParameter(1, new Parameter("Number of Time STacked"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "When target buffed by spell "+parameters[0].ToString();
        }
    }

    class SMART_EVENT_RESET : SmartEvent
    {
        public SMART_EVENT_RESET() : base()
        {
            ID = 25;
        }

        public override string GetReadableString()
        {
            return "On reset";
        }
    }

    class SMART_EVENT_IC_LOS : SmartEvent
    {
        public SMART_EVENT_IC_LOS() : base()
        {
            ID = 26;
            SetParameter(0, new Parameter("NoHostile"));
            SetParameter(1, new Parameter("MaxRnage"));
            SetParameter(2, new Parameter("CooldownMin"));
            SetParameter(3, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_PASSENGER_BOARDED : SmartEvent
    {
        public SMART_EVENT_PASSENGER_BOARDED() : base()
        {
            ID = 27;
            SetParameter(0, new Parameter("CooldownMin"));
            SetParameter(1, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When passenger boarded";
        }
    }

    class SMART_EVENT_PASSENGER_REMOVED : SmartEvent
    {
        public SMART_EVENT_PASSENGER_REMOVED() : base()
        {
            ID = 28;
            SetParameter(0, new Parameter("CooldownMin"));
            SetParameter(1, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When passenger removed";
        }
    }

    class SMART_EVENT_CHARMED : SmartEvent
    {
        public SMART_EVENT_CHARMED() : base()
        {
            ID = 29;
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_CHARMED_TARGET : SmartEvent
    {
        public SMART_EVENT_CHARMED_TARGET() : base()
        {
            ID = 30;
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_SPELLHIT_TARGET : SmartEvent
    {
        public SMART_EVENT_SPELLHIT_TARGET() : base()
        {
            ID = 31;
            SetParameter(0, new SpellParameter("SpellID"));
            SetParameter(1, new Parameter("School"));
            SetParameter(2, new Parameter("CooldownMin"));
            SetParameter(3, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When target is hit by spell "+parameters[0].ToString();
        }
    }

    class SMART_EVENT_DAMAGED : SmartEvent
    {
        public SMART_EVENT_DAMAGED() : base()
        {
            ID = 32;
            SetParameter(0, new Parameter("MinDmg"));
            SetParameter(1, new Parameter("MaxDmg"));
            SetParameter(2, new Parameter("CooldownMin"));
            SetParameter(3, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When damaged between " + parameters[0].ToString() + " and " + parameters[1].ToString() +" taken";
        }
    }

    class SMART_EVENT_DAMAGED_TARGET : SmartEvent
    {
        public SMART_EVENT_DAMAGED_TARGET() : base()
        {
            ID = 33;
            SetParameter(0, new Parameter("MinDmg"));
            SetParameter(1, new Parameter("MaxDmg"));
            SetParameter(2, new Parameter("CooldownMin"));
            SetParameter(3, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When target is damaged between "+parameters[0].ToString() +" and "+parameters[1].ToString();
        }
    }

    class SMART_EVENT_MOVEMENTINFORM : SmartEvent
    {
        public SMART_EVENT_MOVEMENTINFORM() : base()
        {
            ID = 34;
            SetParameter(0, new Parameter("MovementType(any)"));
            SetParameter(1, new Parameter("PointID"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_SUMMON_DESPAWNED : SmartEvent
    {
        public SMART_EVENT_SUMMON_DESPAWNED() : base()
        {
            ID = 35;
            SetParameter(0, new Parameter("Entry"));
            SetParameter(1, new Parameter("CooldownMin"));
            SetParameter(2, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When summond entry "+parameters[0].ToString() +" is despawned";
        }
    }

    class SMART_EVENT_CORPSE_REMOVED : SmartEvent
    {
        public SMART_EVENT_CORPSE_REMOVED() : base()
        {
            ID = 36;
        }

        public override string GetReadableString()
        {
            return "When creature corpse is removed";
        }
    }

    class SMART_EVENT_AI_INIT : SmartEvent
    {
        public SMART_EVENT_AI_INIT() : base()
        {
            ID = 37;
        }

        public override string GetReadableString()
        {
            return "AI_INIT";
        }
    }

    class SMART_EVENT_DATA_SET : SmartEvent
    {
        public SMART_EVENT_DATA_SET() : base()
        {
            ID = 38;
            SetParameter(0, new Parameter("Id"));
            SetParameter(1, new Parameter("Value"));
            SetParameter(2, new Parameter("CooldownMin"));
            SetParameter(3, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When variable "+parameters[0].ToString() + " is set to "+parameters[1].ToString();
        }
    }

    class SMART_EVENT_WAYPOINT_START : SmartEvent
    {
        public SMART_EVENT_WAYPOINT_START() : base()
        {
            ID = 39;
            SetParameter(0, new Parameter("PointId(0any)"));
            SetParameter(1, new Parameter("pathID(0any)"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_WAYPOINT_REACHED : SmartEvent
    {
        public SMART_EVENT_WAYPOINT_REACHED() : base()
        {
            ID = 40;
            SetParameter(0, new Parameter("PointId(0any)"));
            SetParameter(1, new Parameter("pathID(0any)"));
        }

        public override string GetReadableString()
        {
            return "When waypoint point #" + parameters[0].ToString() +" (path: "+parameters[1].ToString() +") is reached";
        }
    }

    class SMART_EVENT_TRANSPORT_ADDPLAYER : SmartEvent
    {
        public SMART_EVENT_TRANSPORT_ADDPLAYER() : base()
        {
            ID = 41;
        }

        public override string GetReadableString()
        {
            return "When transport adds player";
        }
    }

    class SMART_EVENT_TRANSPORT_ADDCREATURE : SmartEvent
    {
        public SMART_EVENT_TRANSPORT_ADDCREATURE() : base()
        {
            ID = 42;
            SetParameter(0, new Parameter("Entry (0 any)"));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0)
                return "When transport adds any creature";
            return "When transport adda creature with entry "+parameters[0].ToString();
        }
    }

    class SMART_EVENT_TRANSPORT_REMOVE_PLAYER : SmartEvent
    {
        public SMART_EVENT_TRANSPORT_REMOVE_PLAYER() : base()
        {
            ID = 43;
        }

        public override string GetReadableString()
        {
            return "When transport removes player";
        }
    }

    class SMART_EVENT_TRANSPORT_RELOCATE : SmartEvent
    {
        public SMART_EVENT_TRANSPORT_RELOCATE() : base()
        {
            ID = 44;
            SetParameter(0, new Parameter("PointId"));
        }

        public override string GetReadableString()
        {
            return "todo";
        }
    }

    class SMART_EVENT_INSTANCE_PLAYER_ENTER : SmartEvent
    {
        public SMART_EVENT_INSTANCE_PLAYER_ENTER() : base()
        {
            ID = 45;
            SetParameter(0, new Parameter("Team (0 any)"));
            SetParameter(1, new Parameter("CooldownMin"));
            SetParameter(2, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When player enters instance";
        }
    }

    class SMART_EVENT_AREATRIGGER_ONTRIGGER : SmartEvent
    {
        public SMART_EVENT_AREATRIGGER_ONTRIGGER() : base()
        {
            ID = 46;
            SetParameter(0, new Parameter("TriggerId(0 any)"));
        }

        public override string GetReadableString()
        {
            return "When areatrigger #"+parameters[0].ToString() +" is triggered";
        }
    }

    class SMART_EVENT_QUEST_ACCEPTED : SmartEvent
    {
        public SMART_EVENT_QUEST_ACCEPTED() : base()
        {
            ID = 47;
        }

        public override string GetReadableString()
        {
            return "When quest is accepted";
        }
    }

    class SMART_EVENT_QUEST_OBJ_COPLETETION : SmartEvent
    {
        public SMART_EVENT_QUEST_OBJ_COPLETETION() : base()
        {
            ID = 48;
        }

        public override string GetReadableString()
        {
            return "When quest objective is completed";
        }
    }

    class SMART_EVENT_QUEST_COMPLETION : SmartEvent
    {
        public SMART_EVENT_QUEST_COMPLETION() : base()
        {
            ID = 49;
        }

        public override string GetReadableString()
        {
            return "On quest completion";
        }
    }

    class SMART_EVENT_QUEST_REWARDED : SmartEvent
    {
        public SMART_EVENT_QUEST_REWARDED() : base()
        {
            ID = 50;
        }

        public override string GetReadableString()
        {
            return "When quest is rewarded";
        }
    }

    class SMART_EVENT_QUEST_FAIL : SmartEvent
    {
        public SMART_EVENT_QUEST_FAIL() : base()
        {
            ID = 51;
        }

        public override string GetReadableString()
        {
            return "When quest failed";
        }
    }

    class SMART_EVENT_TEXT_OVER : SmartEvent
    {
        public SMART_EVENT_TEXT_OVER() : base()
        {
            ID = 52;
            SetParameter(0, new Parameter("GroupId from creature_text"));
            SetParameter(1, new Parameter(" creature entry who talks (0 any)"));
        }

        public override string GetReadableString()
        {
            return "When text #" + parameters[0].ToString() + " has diasppeared";
        }
    }

    class SMART_EVENT_RECEIVE_HEAL : SmartEvent
    {
        public SMART_EVENT_RECEIVE_HEAL() : base()
        {
            ID = 53;
            SetParameter(0, new Parameter("MinHeal"));
            SetParameter(1, new Parameter("MaxHeal"));
            SetParameter(2, new Parameter("CooldownMin"));
            SetParameter(3, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When creature has received heal (check every "+parameters[2].ToString() + " - "+parameters[3].ToString() +" ms)";
        }
    }

    class SMART_EVENT_JUST_SUMMONED : SmartEvent
    {
        public SMART_EVENT_JUST_SUMMONED() : base()
        {
            ID = 54;
        }

        public override string GetReadableString()
        {
            return "When creature has been summoned by";
        }
    }

    class SMART_EVENT_WAYPOINT_PAUSED : SmartEvent
    {
        public SMART_EVENT_WAYPOINT_PAUSED() : base()
        {
            ID = 55;
            SetParameter(0, new Parameter("PointId(0any)"));
            SetParameter(1, new Parameter("pathID(0any)"));
        }

        public override string GetReadableString()
        {
            return "When creature paused at waypoint path " + parameters[1].ToString() + " (point " + parameters[0].ToString() + ")";
        }
    }

    class SMART_EVENT_WAYPOINT_RESUMED : SmartEvent
    {
        public SMART_EVENT_WAYPOINT_RESUMED() : base()
        {
            ID = 56;
            SetParameter(0, new Parameter("PointId(0any)"));
            SetParameter(1, new Parameter("pathID(0any)"));
        }

        public override string GetReadableString()
        {
            return "When creature resumed at waypoint path " + parameters[1].ToString() + " (point " + parameters[0].ToString() + ")";
        }
    }

    class SMART_EVENT_WAYPOINT_STOPPED : SmartEvent
    {
        public SMART_EVENT_WAYPOINT_STOPPED() : base()
        {
            ID = 57;
            SetParameter(0, new Parameter("PointId(0any)"));
            SetParameter(1, new Parameter("pathID(0any)"));
        }

        public override string GetReadableString()
        {
            return "When creature stopped at waypoint path " + parameters[1].ToString() + " (point "+parameters[0].ToString() +")";
        }
    }

    class SMART_EVENT_WAYPOINT_ENDED : SmartEvent
    {
        public SMART_EVENT_WAYPOINT_ENDED() : base()
        {
            ID = 58;
            SetParameter(0, new Parameter("PointId (0 any)"));
            SetParameter(1, new Parameter("pathID (0 any)"));
        }

        public override string GetReadableString()
        {
            return "When creature finished waypoint path " + parameters[1].ToString() + " (point " + parameters[0].ToString() + ")";
        }
    }

    class SMART_EVENT_TIMED_EVENT_TRIGGERED : SmartEvent
    {
        public SMART_EVENT_TIMED_EVENT_TRIGGERED() : base()
        {
            ID = 59;
            SetParameter(0, new Parameter("id"));
        }

        public override string GetReadableString()
        {
            return "When timed event #" + parameters[0].ToString() +" is triggered";
        }
    }

    class SMART_EVENT_UPDATE : SmartEvent
    {
        public SMART_EVENT_UPDATE() : base()
        {
            ID = 60;
            SetParameter(0, new Parameter("InitialMin"));
            SetParameter(1, new Parameter("InitialMax"));
            SetParameter(2, new Parameter("RepeatMin"));
            SetParameter(3, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0 && parameters[1].GetValue() == 0 && parameters[2].GetValue() == 0 && parameters[3].GetValue() == 0)
                return "Never";
            else if (parameters[1].GetValue() < parameters[0].GetValue() || parameters[3].GetValue() < parameters[2].GetValue())
                return "Invalid! (max < min!)";
            else if (parameters[0].GetValue() == 0 && parameters[1].GetValue() == 0)
                return "Every " + parameters[2].ToString() + " and " + parameters[3].ToString();
            else if (parameters[2].GetValue() == 0 && parameters[3].GetValue() == 0)
                return "Every" + parameters[0].ToString() + " and " + parameters[1].ToString();
            else
                return "Every " + parameters[2].ToString() + " and " + parameters[3].ToString() + " (for the first time every " + parameters[2].ToString() + " and " + parameters[3].ToString() + ")";
        }
    }

    class SMART_EVENT_LINK : SmartEvent
    {
        public SMART_EVENT_LINK() : base()
        {
            ID = 61;
            SetParameter(0, new Parameter("INTERNAL USAGE"));
            SetParameter(1, new Parameter("no params"));
            SetParameter(2, new Parameter("used to link together multiple elements"));
            SetParameter(3, new Parameter("does not use any extra resources to iterate event lists needlessly"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_GOSSIP_SELECT : SmartEvent
    {
        public SMART_EVENT_GOSSIP_SELECT() : base()
        {
            ID = 62;
            SetParameter(0, new Parameter("menuID"));
            SetParameter(1, new Parameter("actionID"));
        }

        public override string GetReadableString()
        {
            return "When player select action #" + parameters[1].ToString() + " from menu "+parameters[0].ToString();
        }
    }

    class SMART_EVENT_JUST_CREATED : SmartEvent
    {
        public SMART_EVENT_JUST_CREATED() : base()
        {
            ID = 63;
        }

        public override string GetReadableString()
        {
            return "When creature has been just created";
        }
    }

    class SMART_EVENT_GOSSIP_HELLO : SmartEvent
    {
        public SMART_EVENT_GOSSIP_HELLO() : base()
        {
            ID = 64;
        }

        public override string GetReadableString()
        {
            return "When player opens gossip menu";
        }
    }

    class SMART_EVENT_FOLLOW_COMPLETED : SmartEvent
    {
        public SMART_EVENT_FOLLOW_COMPLETED() : base()
        {
            ID = 65;
        }

        public override string GetReadableString()
        {
            return "When follow is finished";
        }
    }

    class SMART_EVENT_DUMMY_EFFECT : SmartEvent
    {
        public SMART_EVENT_DUMMY_EFFECT() : base()
        {
            ID = 66;
            SetParameter(0, new SpellParameter("spellId"));
            SetParameter(1, new Parameter("effectIndex"));
        }

        public override string GetReadableString()
        {
            return "When spell " + parameters[0].ToString() + " effect #"+parameters[1].ToString() +" triggers";
        }
    }

    class SMART_EVENT_IS_BEHIND_TARGET : SmartEvent
    {
        public SMART_EVENT_IS_BEHIND_TARGET() : base()
        {
            ID = 67;
            SetParameter(0, new Parameter("cooldownMin"));
            SetParameter(1, new Parameter("CooldownMax"));
        }

        public override string GetReadableString()
        {
            return "When event is behind target (check every "+parameters[0].ToString() +" - "+parameters[1].ToString() +" ms)";
        }
    }

    class SMART_EVENT_GAME_EVENT_START : SmartEvent
    {
        public SMART_EVENT_GAME_EVENT_START() : base()
        {
            ID = 68;
            SetParameter(0, new Parameter("game_event.Entry"));
        }

        public override string GetReadableString()
        {
            return "When game_event " + parameters[0].ToString() + " starts";
        }
    }

    class SMART_EVENT_GAME_EVENT_END : SmartEvent
    {
        public SMART_EVENT_GAME_EVENT_END() : base()
        {
            ID = 69;
            SetParameter(0, new Parameter("game_event.Entry"));
        }

        public override string GetReadableString()
        {
            return "When game_event " + parameters[0].ToString() + " ends";
        }
    }

    class SMART_EVENT_GO_STATE_CHANGED : SmartEvent
    {
        public SMART_EVENT_GO_STATE_CHANGED() : base()
        {
            ID = 70;
            SetParameter(0, new Parameter("go state"));
        }

        public override string GetReadableString()
        {
            return "On game object state changed to "+parameters[0].ToString();
        }
    }

    class SMART_EVENT_HAS_RESOURCE : SmartEvent
    {
        public SMART_EVENT_HAS_RESOURCE() : base()
        {
            ID = 71;
            SetParameter(0, new Parameter("resource"));
        }

        public override string GetReadableString()
        {
            return "???";
        }
    }

    class SMART_EVENT_SUMMONEROROWNER_HEALTH : SmartEvent
    {
        public SMART_EVENT_SUMMONEROROWNER_HEALTH() : base()
        {
            ID = 72;
            SetParameter(0, new Parameter("health_min"));
            SetParameter(1, new Parameter("health_max"));
        }

        public override string GetReadableString()
        {
            return "??";
        }
    }

    class SMART_EVENT_SUMMONEROROWNER_RESOURCE : SmartEvent
    {
        public SMART_EVENT_SUMMONEROROWNER_RESOURCE() : base()
        {
            ID = 73;
            SetParameter(0, new Parameter("mana_min"));
            SetParameter(1, new Parameter("mana_max"));
        }

        public override string GetReadableString()
        {
            return "???";
        }
    }

    class SMART_EVENT_MISSING_BUFF : SmartEvent
    {
        public SMART_EVENT_MISSING_BUFF() : base()
        {
            ID = 74;
            SetParameter(0, new SpellParameter("SpellId"));
            SetParameter(1, new Parameter("RepeatMin"));
            SetParameter(2, new Parameter("RepeatMax"));
        }

        public override string GetReadableString()
        {
            return "When creature has no buff " + parameters[0].ToString() + " (check every " + parameters[1].ToString() + " - " + parameters[2].ToString() + "ms)";
        }
    }

    class SMART_EVENT_ACTION_DONE : SmartEvent
    {
        public SMART_EVENT_ACTION_DONE() : base()
        {
            ID = 75;
            SetParameter(0, new Parameter("eventId (SharedDefines.EventId)"));
        }

        public override string GetReadableString()
        {
            return "SMART_EVENT_ACTION_DONE";
        }
    }

    class SMART_EVENT_ON_SPELLCLICK : SmartEvent
    {
        public SMART_EVENT_ON_SPELLCLICK() : base()
        {
            ID = 76;
        }

        public override string GetReadableString()
        {
            return "On spellclick";
        }
    }

    class SMART_EVENT_GO_EVENT_INFORM : SmartEvent
    {
        public SMART_EVENT_GO_EVENT_INFORM() : base()
        {
            ID = 77;
            SetParameter(0, new Parameter("eventId"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_SMART_TIMER : SmartEvent
    {
        public SMART_EVENT_SMART_TIMER() : base()
        {
            ID = 78;
            SetParameter(0, new Parameter("Id"));
        }

        public override string GetReadableString()
        {
            return "";
        }
    }

    class SMART_EVENT_ON_AREA_CHANGE : SmartEvent
    {
        public SMART_EVENT_ON_AREA_CHANGE() : base()
        {
            ID = 79;
        }

        public override string GetReadableString()
        {
            return "";
        }
    }
}
