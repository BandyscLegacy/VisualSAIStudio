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
                case "SpellParameter":
                    return new SpellParameter("");
                case "EmoteParameter":
                    return new EmoteParameter("");
                case "CreatureParameter":
                    return new CreatureParameter("");
                case "QuestParameter":
                    return new QuestParameter("");
                case "SoundParameter":
                    return new SoundParameter("");
                case "MovieParameter":
                    return new MovieParameter("");
                case "ZoneAreaParameter":
                    return new ZoneAreaParameter("");
                case "BoolParameter":
                    return new BoolParameter("");
                case "SwitchParameter":
                    return new SwitchParameter("");
                case "FlagParameter":
                    return new FlagParameter("");
                case "Parameter":
                    return new Parameter("");
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

    public class SwitchParameter : Parameter
    {
        public Dictionary<int, SelectOption> select;

        public SwitchParameter(String name, Dictionary<int, SelectOption> select) : this(name, null, select, false) { }

        public SwitchParameter(String name, String description, Dictionary<int, SelectOption> select) : this(name, description, select, false) { }

        public SwitchParameter(String name, Dictionary<int, SelectOption> select, bool required) : this(name, null, select, required) { }

        public SwitchParameter(String name, String description, Dictionary<int, SelectOption> select, bool required) : base(name, description, required) 
        {
            this.select = select;
        }

        public SwitchParameter(String name, String[] values) : this(name, null, values) { }

        public SwitchParameter(String name, String description, String[] values) : base(name, description)
        {
            select = new Dictionary<int, SelectOption>();
            for (int i = 0; i < values.Length; ++i)
                select.Add(i, new SelectOption(values[i]));
        }

        public SwitchParameter(String name, String description) : base(name, description) {  }

        public SwitchParameter(String name) : base(name) { }

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

        public FlagParameter(String name, Dictionary<int, SelectOption> select) : base(name, select) { }

        public FlagParameter(String name, String description, Dictionary<int, SelectOption> select) : base(name, description, select)  { }

        public FlagParameter(String name, String[] values) : this(name, null, values) { }

        public FlagParameter(String name, String description, String[] values) : base(name, description)
        {
            select = new Dictionary<int, SelectOption>();
            for (int i = 0; i < values.Length; ++i)
                select.Add((int)Math.Pow(2, i), new SelectOption(values[i]));
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
        public SummonTypeParameter(string name) : base (name, types) { }
        public SummonTypeParameter(string name, bool required) : base(name, types, required) { }
    }

    public class BoolParameter : SwitchParameter
    {
        private static Dictionary<int, SelectOption> values = new Dictionary<int, SelectOption>() { { 0,new SelectOption( "False") }, { 1, new SelectOption("True") } };
        public BoolParameter(String name) : base(name, values) { }
        public BoolParameter(String name, String description) : base(name, description, values) { }
    }

    public class PercentageParameter : Parameter
    {
        public PercentageParameter(String name) : base(name) { }
        public PercentageParameter(String name, String description) : base(name, description) { }
        public override string ToString()
        {
            return value + "%";
        }
    }

    abstract class StringParameter : Parameter
    {
        private string str;
        protected StorageType storageType;
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
            str = StringsDB.GetInstance().Get(storageType, value);
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

    //this should be deleted I guess...
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
