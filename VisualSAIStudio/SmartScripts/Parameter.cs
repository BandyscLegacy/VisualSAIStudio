using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public class Parameter
    {
        protected int value;
        public string name { get; set; }
        public string description { get; set; }
        protected List<SmartScripts.ParameterConditional> validators = new List<SmartScripts.ParameterConditional>();

        public Parameter() : this(null) { }

        public Parameter(String name) : this(name, null) {  }

        public Parameter(String name, String description) : this(name, description, 0, false) {  }

        public Parameter(String name, int value) : this(name, null, value, false) { }

        public Parameter(String name, string description, bool required) : this(name, description, 0, required) { }

        public Parameter (String name, String description, int value, bool required)
        {
            this.name = name;
            this.description = description;
            this.SetValue(value);
            if (required)
                validators.Add(new SmartScripts.ParameterConditionalCompareValue(this, 0, SmartScripts.CompareType.NOT_EQUALS, name + " is required"));
        }

        public virtual int GetValue()
        {
            return value;
        }

        public virtual void SetValue(int value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public virtual List<SmartScripts.ParameterConditional> GetValidators()
        {
            return validators;
        }

        public static Parameter Factory(string param)
        {
            switch (param)
            {
                case "CastFlagsParameter":
                    return new CastFlagsParameter();
                case "SpellParameter":
                    return new StringParameter(StorageType.Spell);
                case "EmoteParameter":
                    return new StringParameter(StorageType.Emote);
                case "CreatureParameter":
                    return new StringParameter(StorageType.Creature);
                case "QuestParameter":
                    return new StringParameter(StorageType.Quest);
                case "SoundParameter":
                    return new StringParameter(StorageType.Sound);
                case "MovieParameter":
                    return new StringParameter(StorageType.Movie);
                case "ZoneAreaParameter":
                    return new StringParameter(StorageType.Area);
                case "MapParameter":
                    return new StringParameter(StorageType.Map);
                case "ClassParameter":
                    return new StringParameter(StorageType.Class);
                case "GameobjectParameter":
                    return new StringParameter(StorageType.GameObject);
                case "SkillParameter":
                    return new StringParameter(StorageType.Skill);
                case "GameEventParameter":
                    return new StringParameter(StorageType.GameEvent);
                case "RaceParameter":
                    return new StringParameter(StorageType.Race);
                case "CreatureTypeParameter":
                    return new StringParameter(StorageType.CreatureType);
                case "PhaseParameter":
                    return new StringParameter(StorageType.Phase);
                case "AchievementParameter":
                    return new StringParameter(StorageType.Achievement);
                case "GameobjectGUIDParameter":
                    return new StringParameter(StorageType.GameObjectGuid);
                case "BoolParameter":
                    return new BoolParameter();
                case "SwitchParameter":
                    return new SwitchParameter();
                case "FlagParameter":
                    return new FlagParameter();
                case "SpellSchoolParameter":
                    return new SpellSchoolParameter();
                case "PercentageParameter":
                    return new PercentageParameter();
                case "SummonTypeParameter":
                    return new SummonTypeParameter();
                case "CreatureGUIDParameter":
                    return new StringParameter(StorageType.CreatureGuid);
                case "Parameter":
                    return new Parameter();
            }
            return new NullParameter();
        }
    }

    public class NullParameter : Parameter
    {
        public NullParameter() : base("") 
        {
            validators.Add(new SmartScripts.ParameterConditionalCompareValue(this, 0, SmartScripts.CompareType.EQUALS, "Unused parameter has value"));
        }
    }

    public  class SwitchParameter : Parameter
    {
        public Dictionary<int, SelectOption> select;

        public SwitchParameter(String name, Dictionary<int, SelectOption> select) : this(name, null, select, false) { }

        public SwitchParameter(String name, String description, Dictionary<int, SelectOption> select) : this(name, description, select, false) { }

        public SwitchParameter(String name, Dictionary<int, SelectOption> select, bool required) : this(name, null, select, required) { }

        public SwitchParameter(String name, String description, Dictionary<int, SelectOption> select, bool required) : base(name, description, required) 
        {
            this.select = select;
            this.validators.Add(new SmartScripts.ParameterConditionalCompareValue(this, select.Keys.Min(), select.Keys.Max()));
        }

        public SwitchParameter(String name, String[] values) : this(name, null, values) { }

        public SwitchParameter(String name, String description, String[] values) : base(name, description)
        {
            select = new Dictionary<int, SelectOption>();
            for (int i = 0; i < values.Length; ++i)
                select.Add(i, new SelectOption(values[i]));

            this.validators.Add(new SmartScripts.ParameterConditionalCompareValue(this, 0, values.Length-1));
        }

        public SwitchParameter(String name, String description) : base(name, description) {  }

        public SwitchParameter(String name) : base(name) { }

        public SwitchParameter() : base(null) { }

        public void AddValuesToProperty(DynamicTypeDescriptor.CustomPropertyDescriptor property)
        {
            DynamicTypeDescriptor.StandardValueAttribute value = null; 
            foreach (int key in select.Keys)
            {
                value = new DynamicTypeDescriptor.StandardValueAttribute(key, select[key].name);
                value.Description = select[key].description;
                property.StatandardValues.Add(value);
            }
        }

        public override string ToString()
        {
            if (select.ContainsKey(this.value))
                return select[this.value].name;
            return value.ToString() +" (unknown value)";
        }
    }

    public class FlagParameter : SwitchParameter
    {
        public FlagParameter(String name) : base(name) { }

        public FlagParameter() : base(null) { }

        public FlagParameter(String name, Dictionary<int, SelectOption> select) : base(name) 
        {
            this.select = select;
            this.validators.Add(new SmartScripts.ParameterConditionalFlag(this, select.Keys.ToList()));            
        }

        public FlagParameter(String name, String description, Dictionary<int, SelectOption> select) : base(name, description) 
        {
            this.select = select;
            this.validators.Add(new SmartScripts.ParameterConditionalFlag(this, select.Keys.ToList()));
        }

        public FlagParameter(String name, String[] values) : this(name, null, values) { }

        public FlagParameter(String name, String description, String[] values) : base(name, description)
        {
            select = new Dictionary<int, SelectOption>();
            for (int i = 0; i < values.Length; ++i)
                select.Add((int)Math.Pow(2, i), new SelectOption(values[i]));
            this.validators.Add(new SmartScripts.ParameterConditionalFlag(this, select.Keys.ToList()));
        }

        public override string ToString()
        {
            if (value == 0)
                return (select.ContainsKey(0)?select[0].name:"NONE");
            List<string> flags = new List<string>();
            foreach (int key in select.Keys)
            {
                if ((value & key) > 0)
                    flags.Add(select[key].name);
            }
            return String.Join(", ", flags);
        }
    }

    public class CastFlagsParameter : FlagParameter
    {
        private static Dictionary<int, SelectOption> flags = new Dictionary<int, SelectOption>() {
            {1, new SelectOption("SMARTCAST_INTERRUPT_PREVIOUS")}, 
            {2, new SelectOption("SMARTCAST_TRIGGERED")}, 
            {0x20, new SelectOption("SMARTCAST_AURA_NOT_PRESENT")}, 
            {0x40, new SelectOption("SMARTCAST_COMBAT_MOVE")}
        };
        public CastFlagsParameter(string name) : base (name, flags) { }
        public CastFlagsParameter() : base(null, flags) { }
    }

    public class SpellSchoolParameter : FlagParameter
    {
        private static Dictionary<int, SelectOption> flags = new Dictionary<int, SelectOption>() {
            {0, new SelectOption("SPELL_SCHOOL_MASK_NONE")}, 
            {1, new SelectOption("SPELL_SCHOOL_MASK_NORMAL")}, 
            {2, new SelectOption("SPELL_SCHOOL_MASK_HOLY")}, 
            {4, new SelectOption("SPELL_SCHOOL_MASK_FIRE")}, 
            {8, new SelectOption("SPELL_SCHOOL_MASK_NATURE")}, 
            {16, new SelectOption("SPELL_SCHOOL_MASK_FROST")}, 
            {32, new SelectOption("SPELL_SCHOOL_MASK_SHADOW")}, 
            {64, new SelectOption("SPELL_SCHOOL_MASK_ARCANE")}, 
            {124, new SelectOption("SPELL_SCHOOL_MASK_SPELL")}, 
            {126, new SelectOption("SPELL_SCHOOL_MASK_MAGIC")}, 
            {127, new SelectOption("SPELL_SCHOOL_MASK_ALL")}
        };
        public SpellSchoolParameter(string name) : base(name, flags) { }
        public SpellSchoolParameter() : base(null, flags) { }
    }

    public class SummonTypeParameter : SwitchParameter
    {
        private static Dictionary<int, SelectOption> types = new Dictionary<int, SelectOption>() {
            {1, new SelectOption("TEMPSUMMON_TIMED_OR_DEAD_DESPAWN", "despawns after a specified time OR when the creature disappears")},
            {2, new SelectOption("TEMPSUMMON_TIMED_OR_CORPSE_DESPAWN", "despawns after a specified time OR when the creature dies")},
            {3, new SelectOption("TEMPSUMMON_TIMED_DESPAWN", "despawns after a specified time")},
            {4, new SelectOption("TEMPSUMMON_TIMED_DESPAWN_OUT_OF_COMBAT", "despawns after a specified time after the creature is out of combat")},
            {5, new SelectOption("TEMPSUMMON_CORPSE_DESPAWN", "despawns instantly after death")},
            {6, new SelectOption("TEMPSUMMON_CORPSE_TIMED_DESPAWN", "despawns after a specified time after death")},
            {7, new SelectOption("TEMPSUMMON_DEAD_DESPAWN", "despawns when the creature disappears")},
            {8, new SelectOption("TEMPSUMMON_MANUAL_DESPAWN", "despawns when UnSummon() is called")},
        };
        public SummonTypeParameter() : base(null, types) { }
        public SummonTypeParameter(string name) : base (name, types) { }
        public SummonTypeParameter(string name, bool required) : base(name, types, required) { }
    }

    public class BoolParameter : SwitchParameter
    {
        private static Dictionary<int, SelectOption> values = new Dictionary<int, SelectOption>() { { 0,new SelectOption( "False") }, { 1, new SelectOption("True") } };
        public BoolParameter() : base(null, values) { }
        public BoolParameter(String name) : base(name, values) { }
        public BoolParameter(String name, String description) : base(name, description, values) { }
    }

    public class PercentageParameter : Parameter
    {
        public PercentageParameter() : base(null) { }
        public PercentageParameter(String name) : base(name) { }
        public PercentageParameter(String name, String description) : base(name, description) { }
        public override string ToString()
        {
            return value + "%";
        }
    }

    public class StringParameter : Parameter
    {
        private string str;
        public StorageType storageType { get; protected set; }
        public StringParameter(StorageType type) : this(null, type) { }
        public StringParameter(String name, StorageType storageType) : this(name, null, storageType)  { }
        public StringParameter(String name, String description, StorageType storageType) : this(name, description, storageType, false) { }

        public StringParameter(String name, String description, StorageType storageType, bool required) : base(name, description, required) 
        {
            this.storageType = storageType;
            validators.Add(new SmartScripts.ParameterConditionalDBExists(this, storageType));
        }

        public override void SetValue(int value)
        {
            base.SetValue(value);
            str = DB.GetInstance().GetString(storageType, value);
            if (str == null)
                str = value.ToString();
            else
                str += " (" + value.ToString() +")";
        }

        public override string ToString()
        {
            return str;
        }
    }

    //this should be deleted once actions are moved to custom_actions
    class SpellParameter : StringParameter
    {
        public SpellParameter(String name) : base(name, StorageType.Spell) {}
        public SpellParameter(String name, String description) : base(name, description, StorageType.Spell) { }
        public SpellParameter(String name, String description, bool required) : base(name, description, StorageType.Spell, required) { }
    }

    class EmoteParameter : StringParameter
    {
        public EmoteParameter(String name) : base(name, StorageType.Emote) { }
        public EmoteParameter(String name, String description) : base(name, description, StorageType.Emote) { }
        public EmoteParameter(String name, String description, bool required) : base(name, description, StorageType.Emote, required) { }
    }

    class CreatureParameter : StringParameter
    {
        public CreatureParameter(String name) : base(name, StorageType.Creature) { }
        public CreatureParameter(String name, String description) : base(name, description, StorageType.Creature) { }
        public CreatureParameter(String name, String description, bool required) : base(name, description, StorageType.Creature, required) { }
    }

    class QuestParameter : StringParameter
    {
        public QuestParameter(String name) : base(name, StorageType.Quest) { }
        public QuestParameter(String name, String description) : base(name, description, StorageType.Quest) { }
        public QuestParameter(String name, String description, bool required) : base(name, description, StorageType.Quest, required) { }
    }

    class SoundParameter : StringParameter
    {
        public SoundParameter(String name) : base(name, StorageType.Sound) { }
        public SoundParameter(String name, String description) : base(name, description, StorageType.Sound) { }
        public SoundParameter(String name, String description, bool required) : base(name, description, StorageType.Sound, required) { }
    }

    class ItemParameter : StringParameter
    {
        public ItemParameter(String name) : base(name, StorageType.Item) { }
        public ItemParameter(String name, String description) : base(name, description, StorageType.Item) { }
        public ItemParameter(String name, String description, bool required) : base(name, description, StorageType.Item, required) { }
    }

    class MovieParameter : StringParameter
    {
        public MovieParameter(String name) : base(name, StorageType.Movie) { }
        public MovieParameter(String name, String description) : base(name, description, StorageType.Movie) { }
        public MovieParameter(String name, String description, bool required) : base(name, description, StorageType.Movie, required) { }
    }


    class ZoneAreaParameter : StringParameter
    {
        public ZoneAreaParameter(String name) : base(name, StorageType.Area) { }
        public ZoneAreaParameter(String name, String description) : base(name, description, StorageType.Area) { }
        public ZoneAreaParameter(String name, String description, bool required) : base(name, description, StorageType.Area, required) { }
    }

    class GameObjectParameter : StringParameter
    {
        public GameObjectParameter(String name) : base(name, StorageType.GameObject) { }
        public GameObjectParameter(String name, String description) : base(name, description, StorageType.GameObject) { }
        public GameObjectParameter(String name, String description, bool required) : base(name, description, StorageType.GameObject, required) { }
    }

    class SkillParameter : StringParameter
    {
        public SkillParameter(String name) : base(name, StorageType.Skill) { }
        public SkillParameter(String name, String description) : base(name, description, StorageType.Skill) { }
        public SkillParameter(String name, String description, bool required) : base(name, description, StorageType.Skill, required) { }
    }

    public class SelectOption
    {
        public string name {get;set;}
        public string description {get; set;}
        public SelectOption(string name, string description)
        {
            this.name = name;
            this.description =description;
        }
        public SelectOption(string name) : this (name, null) { }
        public SelectOption() { }
    }
}
