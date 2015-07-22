using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{
    class SMART_ACTION_NONE : SmartAction
    {
        public SMART_ACTION_NONE() : base(0, "ACTION_NONE")
        {
        }

        public override string GetReadableString()
        {
            return "Do nothing";
        }
    }


    class SMART_ACTION_TALK : SmartAction
    {
        public SMART_ACTION_TALK() : base(1, "ACTION_TALK")
        {
            SetParameter(0, new Parameter("Group ID", "Group id from table creature_text. Random text will be said if multiple rows with the same group"));
            SetParameter(1, new Parameter("Delay", "Duration to wait before TEXT_OVER event is triggered"));
            AddConditional(new ParameterConditionalCompareValue(parameters[0], 0, CompareType.GREATER_OR_EQUALS));
            AddConditional(new ParameterConditionalCompareValue(parameters[1], 0, CompareType.GREATER_OR_EQUALS));
        }

        protected override string GetCpp()
        {
            if (target is SMART_TARGET_SELF)
                return "Talk({pram1});";
            return "{target_creature}->AI()->Talk({pram1});";
        }

        public override string GetReadableString()
        {
            if (target is SMART_TARGET_SELF)
                return "Talk {pram1} (self)";
            return "{target}: Talk {pram1}";
        }
    }


    class SMART_ACTION_SET_FACTION : SmartAction
    {
        public SMART_ACTION_SET_FACTION() : base(2, "ACTION_SET_FACTION")
        {
            SetParameter(0, new Parameter("Faction Id", "Use 0 to reset"));
        }

        protected override string GetCpp()
        {
            return "{target_creature}->setFaction({pram1});";
        }

        public override string GetReadableString()
        {
            return "{target}: {pram1value:choose(0):Reset faction|Set faction to {pram1}}";
        }
    }


    class SMART_ACTION_MORPH_TO_ENTRY_OR_MODEL : SmartAction
    {
        public SMART_ACTION_MORPH_TO_ENTRY_OR_MODEL() : base(3, "ACTION_MORPH_TO_ENTRY_OR_MODEL")
        {
            SetParameter(0, new CreatureParameter("Creature entry", "Use 0 for both to reset"));
            SetParameter(1, new Parameter("Model id", "Use 0 for both to reset"));
            List<ParameterConditional> conditions = new List<ParameterConditional>();
            conditions.Add(new ParameterConditionalCompareValue(parameters[0], 0, CompareType.GREATER_THAN));
            conditions.Add(new ParameterConditionalCompareValue(parameters[1], 0, CompareType.GREATER_THAN));
            AddConditional(new ParameterConditionalInversed(new ParameterConditionalGroup(conditions, "Creature entry and model id cannot be set at once")));
        }

        protected override string GetCpp()
        {
            if (parameters[0].GetValue() == 0 && parameters[1].GetValue() == 0)
                return "{target_creature}->DeMorph();";
            else if (parameters[1].GetValue() == 0)
                return "if (CreatureTemplate const* ci = sObjectMgr->GetCreatureTemplate({pram1}))\n{\n {target_creature}->SetDisplayId(sObjectMgr->ChooseDisplayId(0, ci));\n };";
            else
                return "{target_creature}->SetDisplayId({pram2});";
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0 && parameters[1].GetValue() == 0)
                return "{target}: Demorph";
            else if (parameters[0].GetValue() != 0 && parameters[1].GetValue() != 0)
                return "[!] Invalid morph ({pram1}, {pram2}";
            else if (parameters[0].GetValue() == 0)
                return "{target}: Morph to model {pram2}";
            else
                return "{target}: Morph to model from creature {pram1}";
        }
    }


    class SMART_ACTION_SOUND : SmartAction
    {
        public SMART_ACTION_SOUND() : base(4, "ACTION_SOUND")
        {
            SetParameter(0, new SoundParameter("Sound ID"));
            SetParameter(1, new Parameter("Text Range"));
        }

        public override string GetReadableString()
        {
            return "Play sound {pram1} to {target} {pram2:choose(0):|within range {pram2}}";
        }
    }


    class SMART_ACTION_PLAY_EMOTE : SmartAction
    {
        public SMART_ACTION_PLAY_EMOTE() : base(5, "ACTION_PLAY_EMOTE")
        {
            SetParameter(0, new EmoteParameter("Emote ID", "Emote id to play"));
        }

        public override string GetReadableString()
        {
            return "{target}: Play emote {pram1}";
        }
    }


    class SMART_ACTION_FAIL_QUEST : SmartAction
    {
        public SMART_ACTION_FAIL_QUEST() : base(6, "ACTION_FAIL_QUEST")
        {
            SetParameter(0, new QuestParameter("Quest ID", "Quest ID to mark as failed. Only for players"));
        }

        public override string GetReadableString()
        {
            return "{target}: Fail quest {pram1}";
        }
    }


    class SMART_ACTION_ADD_QUEST : SmartAction
    {
        public SMART_ACTION_ADD_QUEST() : base(7, "ACTION_ADD_QUEST")
        {
            SetParameter(0, new QuestParameter("Quest ID", "Add quest, only for players"));
        }

        public override string GetReadableString()
        {
            return "{target}: Add quest {pram1}";
        }
    }


    class SMART_ACTION_SET_REACT_STATE : SmartAction
    {
        public SMART_ACTION_SET_REACT_STATE() : base(8, "ACTION_SET_REACT_STATE")
        {
            SetParameter(0, new SwitchParameter("ReactState", new [] {"REACT_PASSIVE", "REACT_DEFENSIVE", "REACT_AGGRESSIVE"}));
            AddConditional(new ParameterConditionalCompareValue(parameters[0], 0, CompareType.GREATER_OR_EQUALS));
            AddConditional(new ParameterConditionalCompareValue(parameters[0], 2, CompareType.LOWER_OR_EQUALS));
        }
        public override string GetReadableString()
        {
            return "{target}: Set react state to {pram1}";
        }
    }


    class SMART_ACTION_ACTIVATE_GOBJECT : SmartAction
    {
        public SMART_ACTION_ACTIVATE_GOBJECT() : base(9, "ACTION_ACTIVATE_GOBJECT")  {   }

        public override string GetReadableString()
        {
            return "{target}: Activate (only gameobject)";
        }
    }


    class SMART_ACTION_RANDOM_EMOTE : SmartAction
    {
        public SMART_ACTION_RANDOM_EMOTE() : base(10, "ACTION_RANDOM_EMOTE")
        {
            SetParameter(0, new EmoteParameter("Emote 1", "If 0, the emote will be skipped"));
            SetParameter(1, new EmoteParameter("Emote 2", "If 0, the emote will be skipped"));
            SetParameter(2, new EmoteParameter("Emote 3", "If 0, the emote will be skipped"));
        }

        public override string GetReadableString()
        {
            return "{target}: Play random emote: {pram1}, {pram2}, {pram3}";
        }
    }


    class SMART_ACTION_CAST : SmartAction
    {
        public SMART_ACTION_CAST() : base(11, "ACTION_CAST")
        {
            SetParameter(0, new SpellParameter("Spell"));
            SetParameter(1, new CastFlagsParameter("Cast Flags"));
        }

        public override string GetReadableString()
        {
            return "Self: Cast spell {pram1} on {target}";
        }
    }


    class SMART_ACTION_SUMMON_CREATURE : SmartAction
    {
        public SMART_ACTION_SUMMON_CREATURE() : base(12, "ACTION_SUMMON_CREATURE")
        {
            SetParameter(0, new CreatureParameter("Creature"));
            SetParameter(1, new Parameter("Summon Type"));
            SetParameter(2, new Parameter("Duration", "In ms"));
            SetParameter(3, new Parameter("Storage ID", "Target variable id to store summoned creature"));
            SetParameter(4, new BoolParameter("Attack Invoker"));
        }

        public override string GetReadableString()
        {
            if (target is SMART_TARGET_POSITION)
                return "Self: Summon creature {pram1} at {targetcoords}";
            return "{target}: Summon creature {pram1} at my position, moved by offset {targetcoords}";
        }
    }


    class SMART_ACTION_THREAT_SINGLE_PCT : SmartAction
    {
        public SMART_ACTION_THREAT_SINGLE_PCT() : base(13, "ACTION_THREAT_SINGLE_PCT")
        {
            SetParameter(0, new PercentageParameter("Increment", "Modify threat of target by %"));
            SetParameter(1, new PercentageParameter("Decrement", "Modify threat of target by %"));
        }

        public override string GetReadableString()
        {
            return "Self: {pram1value:choose(0):Decrement|Increment} threat of {target} by {pram1value:choose(0):{pram2}|{pram1}}";
        }
    }


    class SMART_ACTION_THREAT_ALL_PCT : SmartAction
    {
        public SMART_ACTION_THREAT_ALL_PCT() : base(14, "ACTION_THREAT_ALL_PCT")
        {
            SetParameter(0, new PercentageParameter("Increment", "Modify threat of all units in threat list by %"));
            SetParameter(1, new PercentageParameter("Decrement", "Modify threat of all units in threat list by %"));
        }

        public override string GetReadableString()
        {
            return "Self: {pram1value:choose(0):Decrement|Increment} threat of all units in threat list by {pram1value:choose(0):{pram2}|{pram1}}";
        }
    }


    class SMART_ACTION_CALL_AREAEXPLOREDOREVENTHAPPENS : SmartAction
    {
        public SMART_ACTION_CALL_AREAEXPLOREDOREVENTHAPPENS() : base(15, "ACTION_CALL_AREAEXPLOREDOREVENTHAPPENS")
        {
            SetParameter(0, new QuestParameter("Quest ID", "Trigger quest group event. Also complete the quest"));
        }

        public override string GetReadableString()
        {
            return "{target}: Call quest {pram1} group event happened";
        }
    }


    class SMART_ACTION_SET_INGAME_PHASE_GROUP : SmartAction
    {
        public SMART_ACTION_SET_INGAME_PHASE_GROUP()
            : base(16, "SMART_ACTION_SET_INGAME_PHASE_GROUP")
        {
            SetParameter(0, new Parameter("Phase ID", "This is actual unit phase"));
        }

        public override string GetReadableString()
        {
            return "{target}: Set phase {pram1}";
        }
    }


    class SMART_ACTION_SET_EMOTE_STATE : SmartAction
    {
        public SMART_ACTION_SET_EMOTE_STATE() : base(17, "ACTION_SET_EMOTE_STATE")
        {
            SetParameter(0, new EmoteParameter("Emote ID"));
        }

        public override string GetReadableString()
        {
            return "{target}: Set emote state (UNIT_NPC_EMOTESTATE) to {pram1}";
        }
    }


    class SMART_ACTION_SET_UNIT_FLAG : SmartAction
    {
        public static Dictionary<int, string> flags = new Dictionary<int, string>()
        {
            {0x00000001, "UNIT_FLAG_SERVER_CONTROLLED"}, 
            {0x00000002, "UNIT_FLAG_NON_ATTACKABLE"}, 
            {0x00000004, "UNIT_FLAG_DISABLE_MOVE"}, 
            {0x00000008, "UNIT_FLAG_PVP_ATTACKABLE"}, 
            {0x00000010, "UNIT_FLAG_RENAME"}, 
            {0x00000020, "UNIT_FLAG_PREPARATION"}, 
            {0x00000040, "UNIT_FLAG_UNK_6"}, 
            {0x00000080, "UNIT_FLAG_NOT_ATTACKABLE_1"}, 
            {0x00000100, "UNIT_FLAG_IMMUNE_TO_PC"}, 
            {0x00000200, "UNIT_FLAG_IMMUNE_TO_NPC"}, 
            {0x00000400, "UNIT_FLAG_LOOTING"}, 
            {0x00000800, "UNIT_FLAG_PET_IN_COMBAT"}, 
            {0x00001000, "UNIT_FLAG_PVP"}, 
            {0x00002000, "UNIT_FLAG_SILENCED"}, 
            {0x00004000, "UNIT_FLAG_UNK_14"}, 
            {0x00008000, "UNIT_FLAG_UNK_15"}, 
            {0x00010000, "UNIT_FLAG_UNK_16"}, 
            {0x00020000, "UNIT_FLAG_PACIFIED"}, 
            {0x00040000, "UNIT_FLAG_STUNNED"}, 
            {0x00080000, "UNIT_FLAG_IN_COMBAT"}, 
            {0x00100000, "UNIT_FLAG_TAXI_FLIGHT"}, 
            {0x00200000, "UNIT_FLAG_DISARMED"}, 
            {0x00400000, "UNIT_FLAG_CONFUSED"}, 
            {0x00800000, "UNIT_FLAG_FLEEING"}, 
            {0x01000000, "UNIT_FLAG_PLAYER_CONTROLLED"}, 
            {0x02000000, "UNIT_FLAG_NOT_SELECTABLE"}, 
            {0x04000000, "UNIT_FLAG_SKINNABLE"}, 
            {0x08000000, "UNIT_FLAG_MOUNT"}, 
            {0x10000000, "UNIT_FLAG_UNK_28"}, 
            {0x20000000, "UNIT_FLAG_UNK_29"}, 
            {0x40000000, "UNIT_FLAG_SHEATHE"}, 
        };
        public SMART_ACTION_SET_UNIT_FLAG() : base(18, "ACTION_SET_UNIT_FLAG")
        {
            SetParameter(0, new FlagParameter("Flags", flags));
            SetParameter(1, new SwitchParameter("Type", new[] { "UNIT_FLAGS", "UNIT_FLAGS_2" }));
        }

        public override string GetReadableString()
        {
            return "{target}: Set {pram2value:choose(0):UNIT_FLAGS to {pram1}|UNIT_FLAGS_2 to {pram1value}}";
        }
    }


    class SMART_ACTION_REMOVE_UNIT_FLAG : SmartAction
    {
        public SMART_ACTION_REMOVE_UNIT_FLAG() : base(19, "ACTION_REMOVE_UNIT_FLAG")
        {
            SetParameter(0, new FlagParameter("Flags", SMART_ACTION_SET_UNIT_FLAG.flags));
            SetParameter(1, new SwitchParameter("Type", new[] { "UNIT_FLAGS", "UNIT_FLAGS_2" }));
        }

        public override string GetReadableString()
        {
            return "{target}: Remove {pram2value:choose(0):UNIT_FLAGS to {pram1}|UNIT_FLAGS_2 to {pram1value}}";
        }
    }


    class SMART_ACTION_AUTO_ATTACK : SmartAction
    {
        public SMART_ACTION_AUTO_ATTACK() : base(20, "ACTION_AUTO_ATTACK")
        {
            SetParameter(0, new BoolParameter("Allow Attack State", "0 = stop attack, 1 = attack"));
            AddConditional(new ParameterConditionalCompareValue(parameters[0], 0, 1));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0)
                return "Self: Disable auto attack";
            return "Self: Enable auto attack";
        }
    }


    class SMART_ACTION_ALLOW_COMBAT_MOVEMENT : SmartAction
    {
        public SMART_ACTION_ALLOW_COMBAT_MOVEMENT() : base(21, "ACTION_ALLOW_COMBAT_MOVEMENT")
        {
            SetParameter(0, new BoolParameter("Allow Combat Movement", "0 = stop combat based movement, 1 = enable combat movement"));
            AddConditional(new ParameterConditionalCompareValue(parameters[0], 0, 1));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0)
                return "Self: Disable combat based movement";
            return "Self: Enable combat based movement";
        }
    }


    class SMART_ACTION_SET_EVENT_PHASE : SmartAction
    {
        public SMART_ACTION_SET_EVENT_PHASE() : base(22, "ACTION_SET_EVENT_PHASE")
        {
            SetParameter(0, new Parameter("Phase", "Note: This is Smart AI event phase, not actual creature phasemask"));
        }

        public override string GetReadableString()
        {
            return "Self: Set event phase to {pram1}";
        }
    }


    class SMART_ACTION_INC_EVENT_PHASE : SmartAction
    {
        public SMART_ACTION_INC_EVENT_PHASE() : base(23, "ACTION_INC_EVENT_PHASE")
        {
            SetParameter(0, new Parameter("Increment"));
            SetParameter(1, new Parameter("Decrement"));
        }

        public override string GetReadableString()
        {
            return "Self: Increment phase by {pram1} and decrement by {pram2}";
        }
    }


    class SMART_ACTION_EVADE : SmartAction
    {
        public SMART_ACTION_EVADE() : base(24, "ACTION_EVADE")
        {
        }

        public override string GetReadableString()
        {
            return "Self: Evade";
        }
    }


    class SMART_ACTION_FLEE_FOR_ASSIST : SmartAction
    {
        public SMART_ACTION_FLEE_FOR_ASSIST() : base(25, "ACTION_FLEE_FOR_ASSIST")
        {
            SetParameter(0, new EmoteParameter("With Emote"));
        }

        public override string GetReadableString()
        {
            return "Self: Flee for assist";
        }
    }


    class SMART_ACTION_CALL_GROUPEVENTHAPPENS : SmartAction
    {
        public SMART_ACTION_CALL_GROUPEVENTHAPPENS() : base(26, "ACTION_CALL_GROUPEVENTHAPPENS")
        {
            SetParameter(0, new QuestParameter("Quest"));
        }

        public override string GetReadableString()
        {
            return "{target}: Call event happened from quest {pram1} for the whole group";
        }
    }

    class SMART_ACTION_REMOVEAURASFROMSPELL : SmartAction
    {
        public SMART_ACTION_REMOVEAURASFROMSPELL() : base(28, "ACTION_REMOVEAURASFROMSPELL")
        {
            SetParameter(0, new SpellParameter("Spell"));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0)
                return "{target}: Remove all auras";
            else
                return "{target}: Remove aura due to spell {pram1}";
        }
    }


    class SMART_ACTION_FOLLOW : SmartAction
    {
        public SMART_ACTION_FOLLOW() : base(29, "ACTION_FOLLOW")
        {
            SetParameter(0, new Parameter("Distance","0 = default"));
            SetParameter(1, new Parameter("Angle","0 = default"));
            SetParameter(2, new Parameter("EndCreatureEntry"));
            SetParameter(3, new Parameter("Credit"));
            SetParameter(4, new SwitchParameter("Credit Type", new [] {"Monster kill",  "Event"}));
        }

        public override string GetReadableString()
        {
            return "Self: Follow {target} by distance {pram1}, angle {pram2}";
        }
    }


    class SMART_ACTION_RANDOM_PHASE : SmartAction
    {
        public SMART_ACTION_RANDOM_PHASE() : base(30, "ACTION_RANDOM_PHASE")
        {
            SetParameter(0, new Parameter("Phase Id 1"));
            SetParameter(1, new Parameter("Phase Id 2"));
            SetParameter(2, new Parameter("Phase Id 3"));
            SetParameter(3, new Parameter("Phase Id 4"));
            SetParameter(4, new Parameter("Phase Id 5"));
            SetParameter(5, new Parameter("Phase Id 6"));

        }

        public override string GetReadableString()
        {
            return "Self: Set random event phase between: {pram1}, {pram2}, {pram3}, {pram4}, {pram5}, {pram6}";
        }
    }


    class SMART_ACTION_RANDOM_PHASE_RANGE : SmartAction
    {
        public SMART_ACTION_RANDOM_PHASE_RANGE() : base(31, "ACTION_RANDOM_PHASE_RANGE")
        {
            SetParameter(0, new Parameter("PhaseMin"));
            SetParameter(1, new Parameter("PhaseMax"));
            AddConditional(new ParameterConditionalCompareValue(parameters[0], parameters[1], CompareType.LOWER_OR_EQUALS));
        }

        public override string GetReadableString()
        {
            return "Self: Set random event phase between range {pram1} and {pram2}";
        }
    }


    class SMART_ACTION_RESET_GOBJECT : SmartAction
    {
        public SMART_ACTION_RESET_GOBJECT() : base(32, "ACTION_RESET_GOBJECT") { }

        public override string GetReadableString()
        {
            return "{target}: Reset (gameobject)";
        }
    }


    class SMART_ACTION_CALL_KILLEDMONSTER : SmartAction
    {
        public SMART_ACTION_CALL_KILLEDMONSTER() : base(33, "ACTION_CALL_KILLEDMONSTER")
        {
            SetParameter(0, new CreatureParameter("Creature"));
        }

        public override string GetReadableString()
        {
            return "{target}: Give kill credit {pram1}";
        }
    }


    class SMART_ACTION_SET_INST_DATA : SmartAction
    {
        public SMART_ACTION_SET_INST_DATA() : base(34, "ACTION_SET_INST_DATA")
        {
            SetParameter(0, new Parameter("Field"));
            SetParameter(1, new Parameter("Data"));
        }

        public override string GetReadableString()
        {
            return "Set instance data #{pram1} to {pram2}";
        }
    }


    class SMART_ACTION_SET_INST_DATA64 : SmartAction
    {
        public SMART_ACTION_SET_INST_DATA64() : base(35, "ACTION_SET_INST_DATA64")
        {
            SetParameter(0, new Parameter("Field"));
            SetParameter(1, new Parameter("Data"));
        }

        public override string GetReadableString()
        {
            return "Set instance data64 #{pram1} to {pram2}";
        }
    }


    class SMART_ACTION_UPDATE_TEMPLATE : SmartAction
    {
        public SMART_ACTION_UPDATE_TEMPLATE() : base(36, "ACTION_UPDATE_TEMPLATE")
        {
            SetParameter(0, new CreatureParameter("Entry"));
        }

        public override string GetReadableString()
        {
            return "{target}: Update template as if it was creature {pram1}";
        }
    }


    class SMART_ACTION_DIE : SmartAction
    {
        public SMART_ACTION_DIE() : base(37, "ACTION_DIE") { }

        public override string GetReadableString()
        {
            return "Self: Die";
        }
    }


    class SMART_ACTION_SET_IN_COMBAT_WITH_ZONE : SmartAction
    {
        public SMART_ACTION_SET_IN_COMBAT_WITH_ZONE() : base(38, "ACTION_SET_IN_COMBAT_WITH_ZONE") { }

        public override string GetReadableString()
        {
            return "Self: Set in combat with current zone";
        }
    }


    class SMART_ACTION_CALL_FOR_HELP : SmartAction
    {
        public SMART_ACTION_CALL_FOR_HELP() : base(39, "ACTION_CALL_FOR_HELP")
        {
            SetParameter(0, new Parameter("Radius"));
        }

        public override string GetReadableString()
        {
            return "Self: Call for help within {pram1} yards";
        }
    }


    class SMART_ACTION_SET_SHEATH : SmartAction
    {
        public SMART_ACTION_SET_SHEATH() : base(40, "ACTION_SET_SHEATH")
        {
            SetParameter(0, new SwitchParameter("Sheath Type",new [] {"Unarmed","Melee","Ranged"}));
            AddConditional(new ParameterConditionalCompareValue(parameters[0], 0, 2));
        }

        public override string GetReadableString()
        {
            return "Self: Set sheath to {pram1}";
        }
    }


    class SMART_ACTION_FORCE_DESPAWN : SmartAction
    {
        public SMART_ACTION_FORCE_DESPAWN() : base(41, "ACTION_FORCE_DESPAWN")
        {
            SetParameter(0, new Parameter("Delay"));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0)
                return "{target}: Despawn instantly";
            else
                return "{target}: Despawn in {pram1} ms";
        }
    }


    class SMART_ACTION_SET_INVINCIBILITY_HP_LEVEL : SmartAction
    {
        public SMART_ACTION_SET_INVINCIBILITY_HP_LEVEL() : base(42, "ACTION_SET_INVINCIBILITY_HP_LEVEL")
        {
            SetParameter(0, new Parameter("MinHpValue(+pct"));
            SetParameter(1, new Parameter("-flat)"));
        }

        public override string GetReadableString()
        {
            return "What is it?";
        }
    }


    class SMART_ACTION_MOUNT_TO_ENTRY_OR_MODEL : SmartAction
    {
        public SMART_ACTION_MOUNT_TO_ENTRY_OR_MODEL() : base(43, "ACTION_MOUNT_TO_ENTRY_OR_MODEL")
        {
            SetParameter(0, new CreatureParameter("Creature entry"));
            SetParameter(1, new Parameter("Model Id"));
            List<ParameterConditional> conditions = new List<ParameterConditional>();
            conditions.Add(new ParameterConditionalCompareValue(parameters[0], 0, CompareType.GREATER_THAN));
            conditions.Add(new ParameterConditionalCompareValue(parameters[1], 0, CompareType.GREATER_THAN));
            AddConditional(new ParameterConditionalInversed(new ParameterConditionalGroup(conditions, "Creature entry and model id cannot be set at once")));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() > 0 && parameters[1].GetValue() > 0)
                return "MOUNT_TO_ENTRY_OR_MODEL (invalid)";
            else if (parameters[0].GetValue() == 0 && parameters[1].GetValue() == 0)
                return "{target}: Dismount";
            else if (parameters[0].GetValue() > 0)
                return "{target}: Mount to creature {pram1}";
            else
                return "{target}: Mount to model {pram2}";
        }
    }


    class SMART_ACTION_SET_INGAME_PHASE_ID : SmartAction
    {
        public SMART_ACTION_SET_INGAME_PHASE_ID() : base(44, "ACTION_SET_INGAME_PHASE_ID")
        {
            SetParameter(0, new Parameter("Phase ID"));
        }

        public override string GetReadableString()
        {
            return "{target}: Set phase id to {pram1}";
        }
    }


    class SMART_ACTION_SET_DATA : SmartAction
    {
        public SMART_ACTION_SET_DATA() : base(45, "ACTION_SET_DATA")
        {
            SetParameter(0, new Parameter("Field"));
            SetParameter(1, new Parameter("Data"));
        }

        public override string GetReadableString()
        {
            return "{target}: Set creature data #{pram1} to {pram2}";
        }
    }


    class SMART_ACTION_MOVE_FORWARD : SmartAction
    {
        public SMART_ACTION_MOVE_FORWARD() : base(46, "ACTION_MOVE_FORWARD")
        {
            SetParameter(0, new Parameter("Distance"));
        }

        public override string GetReadableString()
        {
            return "Self: Move forward {pram1} yards";
        }
    }


    class SMART_ACTION_SET_VISIBILITY : SmartAction
    {
        public SMART_ACTION_SET_VISIBILITY() : base(47, "ACTION_SET_VISIBILITY")
        {
            SetParameter(0, new BoolParameter("Visible"));
        }

        public override string GetReadableString()
        {
            if (parameters[0].GetValue() == 0)
                return "Self: Set invisible";
            else
                return "Self: Set visible";
        }
    }


    class SMART_ACTION_SET_ACTIVE : SmartAction
    {
        public SMART_ACTION_SET_ACTIVE() : base(48, "ACTION_SET_ACTIVE")
        {
        }

        public override string GetReadableString()
        {
            return "Self: Set active (are you sure?)";
        }
    }


    class SMART_ACTION_ATTACK_START : SmartAction
    {
        public SMART_ACTION_ATTACK_START() : base(49, "ACTION_ATTACK_START")
        {
        }

        public override string GetReadableString()
        {
            return "Self: Attack {target}";
        }
    }


    class SMART_ACTION_SUMMON_GO : SmartAction
    {
        public SMART_ACTION_SUMMON_GO() : base(50, "ACTION_SUMMON_GO")
        {
            SetParameter(0, new Parameter("Game Object"));
            SetParameter(1, new Parameter("Despawn Time","In ms"));
        }

        public override string GetReadableString()
        {
            if (target is SMART_TARGET_POSITION)
                return "Self: Summon gameobject {pram1} at {targetcoords} and despawn in {pram2} ms";
            return "{target}: Summon gameobject {pram1} at my position, moved by offset {targetcoords} and despawn in {pram2} ms";
        }
    }


    class SMART_ACTION_KILL_UNIT : SmartAction
    {
        public SMART_ACTION_KILL_UNIT() : base(51, "ACTION_KILL_UNIT")
        {
        }

        public override string GetReadableString()
        {
            return "{target}: Kill self ({target})";
        }
    }


    class SMART_ACTION_ACTIVATE_TAXI : SmartAction
    {
        public SMART_ACTION_ACTIVATE_TAXI() : base(52, "ACTION_ACTIVATE_TAXI")
        {
            SetParameter(0, new Parameter("Taxi ID"));
        }

        public override string GetReadableString()
        {
            return "{target}: Take taxi {pram1}";
        }
    }


    class SMART_ACTION_WP_START : SmartAction
    {
        public SMART_ACTION_WP_START() : base(53, "ACTION_WP_START")
        {
            SetParameter(0, new SwitchParameter("Run/Walk", new[] {"Walk", "Run"}));
            SetParameter(1, new Parameter("Path ID"));
            SetParameter(2, new SwitchParameter("Repeat", new[] { "No", "Yes" }));
            SetParameter(3, new QuestParameter("Quest", "Quest to complete when path ends (escort quest)"));
            SetParameter(4, new Parameter("Despawn time", "Despawn time after finishing the path"));
            SetParameter(5, new SwitchParameter("ReactState", new [] {"Passive", "Defensive", "Aggressive", "Assist"}));
        }

        public override string GetReadableString()
        {
            return "Self: Start path #{pram2}, {pram1value:choose(0):walk|run}, {pram3value:choose(0):do not repeat|repeat}, {pram6}";
        }
    }


    class SMART_ACTION_WP_PAUSE : SmartAction
    {
        public SMART_ACTION_WP_PAUSE() : base(54, "ACTION_WP_PAUSE")
        {
            SetParameter(0, new Parameter("Duration", "Miliseconds to wait before resuming path"));
        }

        public override string GetReadableString()
        {
            return "Self: Pause path for {pram1} ms";
        }
    }


    class SMART_ACTION_WP_STOP : SmartAction
    {
        public SMART_ACTION_WP_STOP() : base(55, "ACTION_WP_STOP")
        {
            SetParameter(0, new Parameter("Despawn Time", "Time to wait before despawn"));
            SetParameter(1, new QuestParameter("Quest", "Quest id to mark as complete or failed"));
            SetParameter(2, new SwitchParameter("Fail/Complete", "Wuest will be marked as either complete or failed", new[] {"Complete", "Fail"}));
        }

        public override string GetReadableString()
        {
            return "Self: Stop path{pram1value:choose(0):, despawn instantly|, despawn in {pram1} ms}{pram2value:choose(0):|{pram3value:choose(0):, complete|, fail} quest {pram2}}";
        }
    }


    class SMART_ACTION_ADD_ITEM : SmartAction
    {
        public SMART_ACTION_ADD_ITEM() : base(56, "ACTION_ADD_ITEM")
        {
            SetParameter(0, new ItemParameter("Item"));
            SetParameter(1, new Parameter("Count"));
        }

        public override string GetReadableString()
        {
            return "{target}: Add item {pram2}x{pram1}";
        }
    }


    class SMART_ACTION_REMOVE_ITEM : SmartAction
    {
        public SMART_ACTION_REMOVE_ITEM() : base(57, "ACTION_REMOVE_ITEM")
        {
            SetParameter(0, new ItemParameter("Item"));
            SetParameter(1, new Parameter("Count"));
        }

        public override string GetReadableString()
        {
            return "{target}: Remove item {pram2}x{pram1}";
        }
    }


    class SMART_ACTION_INSTALL_AI_TEMPLATE : SmartAction
    {
        public SMART_ACTION_INSTALL_AI_TEMPLATE() : base(58, "ACTION_INSTALL_AI_TEMPLATE")
        {
            SetParameter(0, new Parameter("AITemplateID"));
        }

        public override string GetReadableString()
        {
            return "Self: Install AI Template {pram1}";
        }
    }


    class SMART_ACTION_SET_RUN : SmartAction
    {
        public SMART_ACTION_SET_RUN() : base(59, "ACTION_SET_RUN")
        {
            SetParameter(0, new SwitchParameter("Movement Type", new [] {"Walk", "Run"}));
        }

        public override string GetReadableString()
        {
            return "Self: {pram1value:choose(0):Set walk|Set run}";
        }
    }


    class SMART_ACTION_SET_FLY : SmartAction
    {
        public SMART_ACTION_SET_FLY() : base(60, "ACTION_SET_FLY")
        {
            SetParameter(0, new BoolParameter("Enable", "If true, flight will be enabled"));
        }

        public override string GetReadableString()
        {
            return "Self: {pram1value:choose(0):Disable|Enable} flight";
        }
    }


    class SMART_ACTION_SET_SWIM : SmartAction
    {
        public SMART_ACTION_SET_SWIM() : base(61, "ACTION_SET_SWIM")
        {
            SetParameter(0, new BoolParameter("Enable", "If true, swimming will be enabled"));
        }

        public override string GetReadableString()
        {
            return "Self: {pram1value:choose(0):Disable|Enable} swimming";
        }
    }


    class SMART_ACTION_TELEPORT : SmartAction
    {
        public SMART_ACTION_TELEPORT() : base(62, "ACTION_TELEPORT")
        {
            SetParameter(0, new ZoneAreaParameter("Map ID"));
        }

        public override string GetReadableString()
        {
            return "Teleport {target} to {targetcoords} on map {pram1}";
        }
    }


    class SMART_ACTION_STORE_VARIABLE_DECIMAL : SmartAction
    {
        public SMART_ACTION_STORE_VARIABLE_DECIMAL() : base(63, "ACTION_STORE_VARIABLE_DECIMAL")
        {
            SetParameter(0, new Parameter("Variable ID"));
            SetParameter(1, new Parameter("Value"));
        }

        public override string GetReadableString()
        {
            return "Self: Variable_{pram1} = {pram2}";
        }
    }


    class SMART_ACTION_STORE_TARGET_LIST : SmartAction
    {
        public SMART_ACTION_STORE_TARGET_LIST() : base(64, "ACTION_STORE_TARGET_LIST")
        {
            SetParameter(0, new Parameter("Variable ID"));
        }

        public override string GetReadableString()
        {
            return "Self: Target_{pram1} = {target}";
        }
    }


    class SMART_ACTION_WP_RESUME : SmartAction
    {
        public SMART_ACTION_WP_RESUME() : base(65, "ACTION_WP_RESUME")
        {
        }

        public override string GetReadableString()
        {
            return "Self: Resume path";
        }
    }


    class SMART_ACTION_SET_ORIENTATION : SmartAction
    {
        public SMART_ACTION_SET_ORIENTATION() : base(66, "ACTION_SET_ORIENTATION")
        {
        }

        public override string GetReadableString()
        {
            return "Self: {targetid:choose(1|8):Look at home position|Look at {targetcoords}|Look at {target}}";
        }
    }


    class SMART_ACTION_CREATE_TIMED_EVENT : SmartAction
    {
        public SMART_ACTION_CREATE_TIMED_EVENT() : base(67, "ACTION_CREATE_TIMED_EVENT")
        {
            SetParameter(0, new Parameter("Timed event id", "This timed event will be triggered"));
            SetParameter(1, new Parameter("Initial Min", "In miliseconds"));
            SetParameter(2, new Parameter("Initial Max", "In miliseconds"));
            SetParameter(3, new Parameter("Repeat Min", "(only if it repeats)"));
            SetParameter(4, new Parameter("Repeat Max", "(only if it repeats)"));
            SetParameter(5, new PercentageParameter("Chance"));
            AddConditional(new ParameterConditionalCompareValue(parameters[1], parameters[2], CompareType.LOWER_OR_EQUALS));
            AddConditional(new ParameterConditionalCompareValue(parameters[3], parameters[4], CompareType.LOWER_OR_EQUALS));
        }

        public override string GetReadableString()
        {
            return "Trigger timed event #{pram1} in {pram2} - {pram3} ms{pram4value:choose(0):| and then trigger every {pram4} - {pram5} ms}{pram6value:choose(0):| with {pram6} chance}";
        }
    }


    class SMART_ACTION_PLAYMOVIE : SmartAction
    {
        public SMART_ACTION_PLAYMOVIE() : base(68, "ACTION_PLAYMOVIE")
        {
            SetParameter(0, new MovieParameter("Entry"));
        }

        public override string GetReadableString()
        {
            return "{target}: Play movie {pram1}";
        }
    }


    class SMART_ACTION_MOVE_TO_POS : SmartAction
    {
        public SMART_ACTION_MOVE_TO_POS() : base(69, "ACTION_MOVE_TO_POS")
        {
            SetParameter(0, new Parameter("PointId"));
        }

        public override string GetReadableString()
        {
            return "Self: Move to {targetid:choose(8):position {targetcoords}|{target}} (point id {pram1})";
        }
    }


    class SMART_ACTION_RESPAWN_TARGET : SmartAction
    {
        public SMART_ACTION_RESPAWN_TARGET() : base(70, "ACTION_RESPAWN_TARGET")
        {
        }

        public override string GetReadableString()
        {
            return "{target}: Respawn";
        }
    }


    class SMART_ACTION_EQUIP : SmartAction
    {
        public SMART_ACTION_EQUIP() : base(71, "ACTION_EQUIP")
        {
            SetParameter(0, new Parameter("Eqipment ID", "Entry from table creature_equip_template"));
            SetParameter(1, new Parameter("Slotmask", "If 0 (or 7) all items will be equiped, 1, 2, 4 are bits for next three items (you can add them)"));
            SetParameter(2, new ItemParameter("Slot 1", "Only if equipement id is 0"));
            SetParameter(3, new ItemParameter("Slot 2", "Only if equipement id is 0"));
            SetParameter(4, new ItemParameter("Slot 3", "Only if equipement id is 0"));
        }

        public override string GetReadableString()
        {
            return "{target}: {pram1value:choose(0):Equip items {pram3}, {pram4}, {pram5}|Eqipt items from creature {pram1}}";
        }
    }


    class SMART_ACTION_CLOSE_GOSSIP : SmartAction
    {
        public SMART_ACTION_CLOSE_GOSSIP() : base(72, "ACTION_CLOSE_GOSSIP")
        {
        }

        public override string GetReadableString()
        {
            return "{target}: Close gossip";
        }
    }


    class SMART_ACTION_TRIGGER_TIMED_EVENT : SmartAction
    {
        public SMART_ACTION_TRIGGER_TIMED_EVENT() : base(73, "ACTION_TRIGGER_TIMED_EVENT")
        {
            SetParameter(0, new Parameter("ID", "Must be greater or equal than 1"));
        }

        public override string GetReadableString()
        {
            return "Self: Trigger timed event {pram1}";
        }
    }


    class SMART_ACTION_REMOVE_TIMED_EVENT : SmartAction
    {
        public SMART_ACTION_REMOVE_TIMED_EVENT() : base(74, "ACTION_REMOVE_TIMED_EVENT")
        {
            SetParameter(0, new Parameter("ID", "Must be greater or equal than 1"));
        }

        public override string GetReadableString()
        {
            return "Self: Remove timed event {pram1}";
        }
    }


    class SMART_ACTION_ADD_AURA : SmartAction
    {
        public SMART_ACTION_ADD_AURA() : base(75, "ACTION_ADD_AURA")
        {
            SetParameter(0, new SpellParameter("Spell"));
        }

        public override string GetReadableString()
        {
            return "{target}: Add aura {pram1}";
        }
    }


    class SMART_ACTION_OVERRIDE_SCRIPT_BASE_OBJECT : SmartAction
    {
        public SMART_ACTION_OVERRIDE_SCRIPT_BASE_OBJECT() : base(76, "ACTION_OVERRIDE_SCRIPT_BASE_OBJECT") { }

        public override string GetReadableString()
        {
            return "SMART_ACTION_OVERRIDE_SCRIPT_BASE_OBJECT (WARNING: CAN CRASH CORE)";
        }
    }


    class SMART_ACTION_RESET_SCRIPT_BASE_OBJECT : SmartAction
    {
        public SMART_ACTION_RESET_SCRIPT_BASE_OBJECT() : base(77, "ACTION_RESET_SCRIPT_BASE_OBJECT") { }

        public override string GetReadableString()
        {
            return "SMART_ACTION_RESET_SCRIPT_BASE_OBJECT";
        }
    }


    class SMART_ACTION_CALL_SCRIPT_RESET : SmartAction
    {
        public SMART_ACTION_CALL_SCRIPT_RESET() : base(78, "ACTION_CALL_SCRIPT_RESET")
        {
        }

        public override string GetReadableString()
        {
            return "Self: Call OnReset() event";
        }
    }


    class SMART_ACTION_SET_RANGED_MOVEMENT : SmartAction
    {
        public SMART_ACTION_SET_RANGED_MOVEMENT() : base(79, "ACTION_SET_RANGED_MOVEMENT")
        {
            SetParameter(0, new Parameter("Distance"));
            SetParameter(1, new Parameter("angle"));
        }

        public override string GetReadableString()
        {
            return "{target}: Chase its victim in {pram1} yards (angle {pram2}°)";
        }
    }


    class SMART_ACTION_CALL_TIMED_ACTIONLIST : SmartAction
    {
        public SMART_ACTION_CALL_TIMED_ACTIONLIST() : base(80, "ACTION_CALL_TIMED_ACTIONLIST")
        {
            SetParameter(0, new Parameter("ID","overwrites already running actionlist?"));
        }

        public override string GetReadableString()
        {
            return "Start timed action list id #{pram1}";
        }
    }


    class SMART_ACTION_SET_NPC_FLAG : SmartAction
    {
        public static Dictionary<int, string> flags = new Dictionary<int, string>() { { 0x00000000, "UNIT_NPC_FLAG_NONE" }, { 0x00000001, "UNIT_NPC_FLAG_GOSSIP" }, { 0x00000002, "UNIT_NPC_FLAG_QUESTGIVER" }, { 0x00000004, "UNIT_NPC_FLAG_UNK1" }, { 0x00000008, "UNIT_NPC_FLAG_UNK2" }, { 0x00000010, "UNIT_NPC_FLAG_TRAINER" }, { 0x00000020, "UNIT_NPC_FLAG_TRAINER_CLASS" }, { 0x00000040, "UNIT_NPC_FLAG_TRAINER_PROFESSION" }, { 0x00000080, "UNIT_NPC_FLAG_VENDOR" }, { 0x00000100, "UNIT_NPC_FLAG_VENDOR_AMMO" }, { 0x00000200, "UNIT_NPC_FLAG_VENDOR_FOOD" }, { 0x00000400, "UNIT_NPC_FLAG_VENDOR_POISON" }, { 0x00000800, "UNIT_NPC_FLAG_VENDOR_REAGENT" }, { 0x00001000, "UNIT_NPC_FLAG_REPAIR" }, { 0x00002000, "UNIT_NPC_FLAG_FLIGHTMASTER" }, { 0x00004000, "UNIT_NPC_FLAG_SPIRITHEALER" }, { 0x00008000, "UNIT_NPC_FLAG_SPIRITGUIDE" }, { 0x00010000, "UNIT_NPC_FLAG_INNKEEPER" }, { 0x00020000, "UNIT_NPC_FLAG_BANKER" }, { 0x00040000, "UNIT_NPC_FLAG_PETITIONER" }, { 0x00080000, "UNIT_NPC_FLAG_TABARDDESIGNER" }, { 0x00100000, "UNIT_NPC_FLAG_BATTLEMASTER" }, { 0x00200000, "UNIT_NPC_FLAG_AUCTIONEER" }, { 0x00400000, "UNIT_NPC_FLAG_STABLEMASTER" }, { 0x00800000, "UNIT_NPC_FLAG_GUILD_BANKER" }, { 0x01000000, "UNIT_NPC_FLAG_SPELLCLICK" }, { 0x02000000, "UNIT_NPC_FLAG_PLAYER_VEHICLE" }, { 0x04000000, "UNIT_NPC_FLAG_MAILBOX" }, { 0x08000000, "UNIT_NPC_FLAG_REFORGER" }, { 0x10000000, "UNIT_NPC_FLAG_TRANSMOGRIFIER" }, { 0x20000000, "UNIT_NPC_FLAG_VAULTKEEPER" } };
        public SMART_ACTION_SET_NPC_FLAG() : base(81, "ACTION_SET_NPC_FLAG")
        {
            SetParameter(0, new FlagParameter("Flags", flags));
        }

        public override string GetReadableString()
        {
            return "{target}: Set npc flags {pram1}";
        }
    }


    class SMART_ACTION_ADD_NPC_FLAG : SmartAction
    {
        public SMART_ACTION_ADD_NPC_FLAG() : base(82, "ACTION_ADD_NPC_FLAG")
        {
            SetParameter(0, new FlagParameter("Flags", SMART_ACTION_SET_NPC_FLAG.flags));
        }

        public override string GetReadableString()
        {
            return "{target}: Add npc flags {pram1}";
        }
    }


    class SMART_ACTION_REMOVE_NPC_FLAG : SmartAction
    {
        public SMART_ACTION_REMOVE_NPC_FLAG() : base(83, "ACTION_REMOVE_NPC_FLAG")
        {
            SetParameter(0, new FlagParameter("Flags", SMART_ACTION_SET_NPC_FLAG.flags));
        }

        public override string GetReadableString()
        {
            return "{target}: Remove npc flags {pram1}";
        }
    }


    class SMART_ACTION_SIMPLE_TALK : SmartAction
    {
        public SMART_ACTION_SIMPLE_TALK() : base(84, "ACTION_SIMPLE_TALK")
        {
            SetParameter(0, new Parameter("Group ID", "Makes target say its line from creature_text. If player, line from creature_text from this entry will be said. Doesn't trigger TEXT_OVER"));
        }

        public override string GetReadableString()
        {
            return "{target}: Talk {pram1}";
        }
    }


    class SMART_ACTION_INVOKER_CAST : SmartAction
    {
        public SMART_ACTION_INVOKER_CAST() : base(85, "ACTION_INVOKER_CAST")
        {
            SetParameter(0, new SpellParameter("Spell"));
            SetParameter(1, new CastFlagsParameter("Cast Flags"));
        }

        public override string GetReadableString()
        {
            return "Invoker: Cast spell {pram1} to {target}";
        }
    }


    class SMART_ACTION_CROSS_CAST : SmartAction
    {
        public SMART_ACTION_CROSS_CAST() : base(86, "ACTION_CROSS_CAST")
        {
            SetParameter(0, new SpellParameter("Spell"));
            SetParameter(1, new CastFlagsParameter("Cast Flags"));
            SetParameter(2, new SwitchParameter("Caster Target Type", new [] {"SMART_TARGET_NONE", "SMART_TARGET_SELF", "SMART_TARGET_VICTIM", "SMART_TARGET_HOSTILE_SECOND_AGGRO", "SMART_TARGET_HOSTILE_LAST_AGGRO", "SMART_TARGET_HOSTILE_RANDOM", "SMART_TARGET_HOSTILE_RANDOM_NOT_TOP", "SMART_TARGET_ACTION_INVOKER", "SMART_TARGET_POSITION", "SMART_TARGET_CREATURE_RANGE", "SMART_TARGET_CREATURE_GUID", "SMART_TARGET_CREATURE_DISTANCE", "SMART_TARGET_STORED", "SMART_TARGET_GAMEOBJECT_RANGE", "SMART_TARGET_GAMEOBJECT_GUID", "SMART_TARGET_GAMEOBJECT_DISTANCE", "SMART_TARGET_INVOKER_PARTY", "SMART_TARGET_PLAYER_RANGE", "SMART_TARGET_PLAYER_DISTANCE", "SMART_TARGET_CLOSEST_CREATURE", "SMART_TARGET_CLOSEST_GAMEOBJECT", "SMART_TARGET_CLOSEST_PLAYER", "SMART_TARGET_ACTION_INVOKER_VEHICLE", "SMART_TARGET_OWNER_OR_SUMMONER", "SMART_TARGET_THREAT_LIST"}));
            SetParameter(3, new Parameter("Caster Target param 1"));
            SetParameter(4, new Parameter("Caster Target param 2"));
            SetParameter(5, new Parameter("Caster Target param 3","+the orignal target fields as Destination target; CasterTargets will cast spellID on all Targets (use with caution if targeting multiple * multiple units)"));
        }

        public override string GetReadableString()
        {
            SmartTarget caster = TargetsFactory.Factory(parameters[2].GetValue());
            for (int i = 0; i < 3; ++i )
                caster.UpdateParams(i, parameters[3+i].GetValue());
            return caster.GetReadableString()+": Cast spell {pram1}{pram2value:choose(0):| with flags {pram2}} at {target}";
        }
    }


    class SMART_ACTION_CALL_RANDOM_TIMED_ACTIONLIST : SmartAction
    {
        public SMART_ACTION_CALL_RANDOM_TIMED_ACTIONLIST() : base(87, "ACTION_CALL_RANDOM_TIMED_ACTIONLIST")
        {
            SetParameter(0, new Parameter("Action List ID 1"));
            SetParameter(1, new Parameter("Action List ID 2"));
            SetParameter(2, new Parameter("Action List ID 3"));
            SetParameter(3, new Parameter("Action List ID 4"));
            SetParameter(4, new Parameter("Action List ID 5"));
            SetParameter(5, new Parameter("Action List ID 6"));
        }

        public override string GetReadableString()
        {
            return "{target}: Call random timed action list: {pram1}, {pram2}, {pram3}, {pram4}, {pram5}, {pram6}";
        }
    }


    class SMART_ACTION_CALL_RANDOM_RANGE_TIMED_ACTIONLIST : SmartAction
    {
        public SMART_ACTION_CALL_RANDOM_RANGE_TIMED_ACTIONLIST() : base(88, "ACTION_CALL_RANDOM_RANGE_TIMED_ACTIONLIST")
        {
            SetParameter(0, new Parameter("Action List ID min"));
            SetParameter(1, new Parameter("Action List ID max"));
            AddConditional(new ParameterConditionalCompareValue(parameters[0], parameters[1], CompareType.LOWER_OR_EQUALS));
        }

        public override string GetReadableString()
        {
            return "{target}: Call random timed action list between range {pram1} and {pram2}";
        }
    }


    class SMART_ACTION_RANDOM_MOVE : SmartAction
    {
        public SMART_ACTION_RANDOM_MOVE() : base(89, "ACTION_RANDOM_MOVE")
        {
            SetParameter(0, new Parameter("Distance", "If 0, creature will stay idle in place"));
        }

        public override string GetReadableString()
        {
            return "{target}: {pram1value:choose(0):Stay in place|Move in radius {pram1} yards}";
        }
    }


    class SMART_ACTION_SET_UNIT_FIELD_BYTES_1 : SmartAction
    {
        public SMART_ACTION_SET_UNIT_FIELD_BYTES_1() : base(90, "ACTION_SET_UNIT_FIELD_BYTES_1")
        {
            SetParameter(0, new Parameter("Bytes"));
        }

        public override string GetReadableString()
        {
            return "{target}: Set UNIT_FIELD_BYTES_1 to {pram1}";
        }
    }


    class SMART_ACTION_REMOVE_UNIT_FIELD_BYTES_1 : SmartAction
    {
        public SMART_ACTION_REMOVE_UNIT_FIELD_BYTES_1() : base(91, "ACTION_REMOVE_UNIT_FIELD_BYTES_1")
        {
            SetParameter(0, new Parameter("Bytes"));
        }

        public override string GetReadableString()
        {
            return "{target}: Remove {pram1} bytes from UNIT_FIELD_BYTES_1";
        }
    }

    class SMART_ACTION_INTERRUPT_SPELL : SmartAction
    {
        public SMART_ACTION_INTERRUPT_SPELL() : base(92, "ACTION_INTERRUPT_SPELL")
        {
        }

        public override string GetReadableString()
        {
            return "{target}: Interrupt casted spell";
        }
    }

    class SMART_ACTION_SEND_GO_CUSTOM_ANIM : SmartAction
    {
        public SMART_ACTION_SEND_GO_CUSTOM_ANIM() : base(93, "ACTION_SEND_GO_CUSTOM_ANIM")
        {
            SetParameter(0, new Parameter("Animation id"));
        }

        public override string GetReadableString()
        {
            return "{target}: Play custom animation (only gameobject)";
        }
    }


    class SMART_ACTION_SET_DYNAMIC_FLAG : SmartAction
    {
        public static Dictionary<int, string> flags = new Dictionary<int, string>() { { 0x0000, "UNIT_DYNFLAG_NONE" }, { 0x0001, "UNIT_DYNFLAG_LOOTABLE" }, { 0x0002, "UNIT_DYNFLAG_TRACK_UNIT" }, { 0x0004, "UNIT_DYNFLAG_TAPPED" }, { 0x0008, "UNIT_DYNFLAG_TAPPED_BY_PLAYER" }, { 0x0010, "UNIT_DYNFLAG_SPECIALINFO" }, { 0x0020, "UNIT_DYNFLAG_DEAD" }, { 0x0040, "UNIT_DYNFLAG_REFER_A_FRIEND" }, { 0x0080, "UNIT_DYNFLAG_TAPPED_BY_ALL_THREAT_LIST" }};
        public SMART_ACTION_SET_DYNAMIC_FLAG() : base(94, "ACTION_SET_DYNAMIC_FLAG")
        {
            SetParameter(0, new FlagParameter("Flags", flags));
        }

        public override string GetReadableString()
        {
            return "{target}: Set dynamic flags to {pram1}";
        }
    }


    class SMART_ACTION_ADD_DYNAMIC_FLAG : SmartAction
    {
        public SMART_ACTION_ADD_DYNAMIC_FLAG() : base(95, "ACTION_ADD_DYNAMIC_FLAG")
        {
            SetParameter(0, new FlagParameter("Flags", SMART_ACTION_SET_DYNAMIC_FLAG.flags));
        }

        public override string GetReadableString()
        {
            return "{target}: Add {pram1} dynamic flag";
        }
    }


    class SMART_ACTION_REMOVE_DYNAMIC_FLAG : SmartAction
    {
        public SMART_ACTION_REMOVE_DYNAMIC_FLAG() : base(96, "ACTION_REMOVE_DYNAMIC_FLAG")
        {
            SetParameter(0, new FlagParameter("Flags", SMART_ACTION_SET_DYNAMIC_FLAG.flags));
        }

        public override string GetReadableString()
        {
            return "{target}: Remove {pram1} dynamic flags";
        }
    }


    class SMART_ACTION_JUMP_TO_POS : SmartAction
    {
        public SMART_ACTION_JUMP_TO_POS() : base(97, "ACTION_JUMP_TO_POS")
        {
            SetParameter(0, new Parameter("Speed XY"));
            SetParameter(1, new Parameter("Speed Z"));
        }

        public override string GetReadableString()
        {
            return "Self: Jump to pos {targetcoords} with speed XY {pram1} and speed Z {pram2}";
        }
    }


    class SMART_ACTION_SEND_GOSSIP_MENU : SmartAction
    {
        public SMART_ACTION_SEND_GOSSIP_MENU() : base(98, "ACTION_SEND_GOSSIP_MENU")
        {
            SetParameter(0, new Parameter("Menu Id", "From gossip_menu"));
            SetParameter(1, new Parameter("Text Id", "From npc_text"));
        }

        public override string GetReadableString()
        {
            return "{target}: Send gossip ID {pram1} with text {pram2}";
        }
    }


    class SMART_ACTION_GO_SET_LOOT_STATE : SmartAction
    {
        public SMART_ACTION_GO_SET_LOOT_STATE() : base(99, "ACTION_GO_SET_LOOT_STATE")
        {
            SetParameter(0, new SwitchParameter("State", new string[] {"GO_NOT_READY", "GO_READY", "GO_ACTIVATED", "GO_JUST_DEACTIVATED"}));
        }

        public override string GetReadableString()
        {
            return "{target}: Set loot state {pram1} (only gameobjects)";
        }
    }

    class SMART_ACTION_SEND_TARGET_TO_TARGET : SmartAction
    {
        public SMART_ACTION_SEND_TARGET_TO_TARGET() : base(100, "SCTION_SEND_TARGET_TO_TARGET")
        {
            SetParameter(0, new Parameter("Variable ID"));
        }

        public override string GetReadableString()
        {
            return "Self: Send stored target {pram1} to {target}";
        }
    }

    class SMART_ACTION_SET_HOME_POS : SmartAction
    {
        public SMART_ACTION_SET_HOME_POS() : base(101, "ACTION_SET_HOME_POS") { }
        public override string GetReadableString()
        {
            return "Self: Set home position to {targetid:choose(1|8):current position|{targetcoords}|position of {target}}";
        }
    }

    class SMART_ACTION_SET_HEALTH_REGEN : SmartAction
    {
        public SMART_ACTION_SET_HEALTH_REGEN() : base(102, "ACTION_SET_HEALTH_REGEN") 
        {
            SetParameter(0, new BoolParameter("Regenerate HP"));
        }
        public override string GetReadableString()
        {
            return "{target}: {pram1value:choose(0):Do not regenerate HP|Regenerate HP}";
        }
    }

    class SMART_ACTION_SET_ROOT : SmartAction
    {
        public SMART_ACTION_SET_ROOT() : base(103, "ACTION_SET_ROOT") 
        {
        }
        public override string GetReadableString()
        {
            return "{target}: Set rooted";
        }
    }

    class SMART_ACTION_SET_GO_FLAG : SmartAction
    {
        public static Dictionary<int, string> flags = new Dictionary<int,string>()
        {
            {0x00000001, "GO_FLAG_IN_USE"}, 
            {0x00000002, "GO_FLAG_LOCKED"}, 
            {0x00000004, "GO_FLAG_INTERACT_COND"}, 
            {0x00000008, "GO_FLAG_TRANSPORT"}, 
            {0x00000010, "GO_FLAG_NOT_SELECTABLE"}, 
            {0x00000020, "GO_FLAG_NODESPAWN"}, 
            {0x00000040, "GO_FLAG_TRIGGERED"}, 
            {0x00000200, "GO_FLAG_DAMAGED"}, 
            {0x00000400, "GO_FLAG_DESTROYED"}, 
        };
        public SMART_ACTION_SET_GO_FLAG() : base(104, "ACTION_SET_GO_FLAG") 
        {
            SetParameter(0, new FlagParameter("Flags", flags));
        }
        public override string GetReadableString()
        {
            return "{target}: Set game object flags {pram1}";
        }
    }
    class SMART_ACTION_ADD_GO_FLAG : SmartAction
    {

        public SMART_ACTION_ADD_GO_FLAG() : base(105, "ACTION_ADD_GO_FLAG") 
        {
            SetParameter(0, new FlagParameter("Flags", SMART_ACTION_SET_GO_FLAG.flags));
        }
        public override string GetReadableString()
        {
            return "{target}: Add game object flag {pram1}";
        }
    }

    class SMART_ACTION_REMOVE_GO_FLAG : SmartAction
    {
        public SMART_ACTION_REMOVE_GO_FLAG() : base(106, "ACTION_REMOVE_GO_FLAG") 
        {
            SetParameter(0, new FlagParameter("Flags", SMART_ACTION_SET_GO_FLAG.flags));
        }
        public override string GetReadableString()
        {
            return "{target}: Remove game object flag {pram1}";
        }
    }

    class SMART_ACTION_SUMMON_CREATURE_GROUP : SmartAction
    {

        public SMART_ACTION_SUMMON_CREATURE_GROUP() : base(107, "ACTION_SUMMON_CREATURE_GROUP") 
        {
            SetParameter(0, new Parameter("Summon group"));
            SetParameter(1, new BoolParameter("Attack me"));
        }
        public override string GetReadableString()
        {
            return "Self: Summon creature group {pram1}";
        }
    }

    class SMART_ACTION_SET_POWER : SmartAction
    {
        public static string[] powers = new [] {"POWER_MANA", "POWER_RAGE", "POWER_FOCUS", "POWER_ENERGY", "POWER_UNUSED", "POWER_RUNES",  "POWER_RUNIC_POWER", "POWER_SOUL_SHARDS", "POWER_ECLIPSE", "POWER_HOLY_POWER", "POWER_ALTERNATE_POWER"};
        public SMART_ACTION_SET_POWER() : base(108, "ACTION_SET_POWER") 
        {
            SetParameter(0, new SwitchParameter("Power Type", powers));
        }
        public override string GetReadableString()
        {
            return "{target}: Set power type to {pram1}";
        }
    }

    class SMART_ACTION_ADD_POWER : SmartAction
    {
        public SMART_ACTION_ADD_POWER() : base(109, "ACTION_ADD_POWER") 
        {
            SetParameter(0, new SwitchParameter("Power Type", SMART_ACTION_SET_POWER.powers));
            SetParameter(1, new Parameter("Value"));
        }
        public override string GetReadableString()
        {
            return "{target}: Add {pram2} to power {pram1}";
        }
    }

    class SMART_ACTION_REMOVE_POWER : SmartAction
    {
        public SMART_ACTION_REMOVE_POWER() : base(110, "ACTION_REMOVE_POWER") 
        {
            SetParameter(0, new SwitchParameter("Power Type", SMART_ACTION_SET_POWER.powers));
            SetParameter(1, new Parameter("Value"));
        }
        public override string GetReadableString()
        {
            return "{target}: Remove {pram2} from power {pram1}";
        }
    }

    class SMART_ACTION_GAME_EVENT_STOP : SmartAction
    {
        public SMART_ACTION_GAME_EVENT_STOP() : base(111, "ACTION_GAME_EVENT_STOP") 
        {
            SetParameter(0, new Parameter("Event ID"));
        }
        public override string GetReadableString()
        {
            return "{target}: Stop game event {pram1}";
        }
    }

    class SMART_ACTION_GAME_EVENT_START : SmartAction
    {
        public SMART_ACTION_GAME_EVENT_START() : base(112, "ACTION_GAME_EVENT_START") 
        {
            SetParameter(0, new Parameter("Event ID"));
        }
        public override string GetReadableString()
        {
            return "{target}: Start game event {pram1}";
        }
    }

    class SMART_ACTION_START_CLOSEST_WAYPOINT : SmartAction
    {
        public SMART_ACTION_START_CLOSEST_WAYPOINT() : base(113, "ACTION_START_CLOSEST_WAYPOINT") 
        {
            SetParameter(0, new Parameter ("Path 1"));
            SetParameter(1, new Parameter ("Path 2"));
            SetParameter(2, new Parameter ("Path 3"));
            SetParameter(3, new Parameter ("Path 4"));
            SetParameter(4, new Parameter ("Path 5"));
            SetParameter(5, new Parameter ("Path 6"));
        }
        public override string GetReadableString()
        {
            return "{target}: Start closest path: {pram1}, {pram2}, {pram3}, {pram4}, {pram5}, {pram6}";
        }
    }
}
