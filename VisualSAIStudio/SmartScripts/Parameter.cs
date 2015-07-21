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
        public Type type { get; set; }

        public Parameter (String name)
        {
            this.name = name;
            this.description = "";
            this.SetValue(0);
            this.type = typeof(int);
        }

        public Parameter (String name, String description)
        {
            this.name = name;
            this.description = description;
            this.SetValue(0);
            this.type = typeof(int);
        }

        public Parameter (String name, int value)
        {
            this.name = name;
            this.SetValue(value);
            this.type = typeof(int);
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
            return NullParameter.GetInstance();
        }
    }

    public class NullParameter : Parameter
    {
        private static NullParameter instance;
        public NullParameter() : base("") { }
        public static NullParameter GetInstance()
        {
            if (instance == null)
                instance = new NullParameter();
            return instance;
        }
    }

    public class SwitchParameter : Parameter
    {
        public Dictionary<int, string> select;

        public SwitchParameter(String name, Dictionary<int, string> select) : base(name)
        {
            this.select = select;
        }
        public SwitchParameter(String name, String description, Dictionary<int, string> select) : base(name, description) 
        {
            this.select = select;
        }
        public SwitchParameter(String name, String[] values) : base(name)
        {
            select = new Dictionary<int, string>();
            for (int i = 0; i < values.Length; ++i )
                select.Add(i, values[i]);
        }
        public SwitchParameter(String name, String description, String[] values) : base(name, description)
        {
            select = new Dictionary<int, string>();
            for (int i = 0; i < values.Length; ++i)
                select.Add(i, values[i]);
        }

        public SwitchParameter(String name, String description) : base(name, description) {  }

        public SwitchParameter(String name) : base(name) { }

        public void AddValuesToProperty(DynamicTypeDescriptor.CustomPropertyDescriptor property)
        {
            DynamicTypeDescriptor.StandardValueAttribute value = null; 
            foreach (int key in select.Keys)
            {
                value = new DynamicTypeDescriptor.StandardValueAttribute(key, select[key]);
                property.StatandardValues.Add(value);
            }
        }

        public override string ToString()
        {
            if (select.ContainsKey(this.value))
                return select[this.value];
            return value.ToString() +" (unknown value)";
        }
    }

    public class FlagParameter : SwitchParameter
    {
        public FlagParameter(String name) : base(name) { }

        public FlagParameter(String name, Dictionary<int, string> select) : base(name, select) { }

        public FlagParameter(String name, String description, Dictionary<int, string> select) : base(name, description, select)  { }

        public FlagParameter(String name, String[] values) : base(name)
        {
            select = new Dictionary<int, string>();
            for (int i = 0; i < values.Length; ++i)
                select.Add((int)Math.Pow(2, i), values[i]);
        }

        public FlagParameter(String name, String description, String[] values) : base(name, description)
        {
            select = new Dictionary<int, string>();
            for (int i = 0; i < values.Length; ++i)
                select.Add((int)Math.Pow(2, i), values[i]);
        }

        public override string ToString()
        {
            if (value == 0)
                return (select.ContainsKey(0)?select[0]:"NONE");
            List<string> flags = new List<string>();
            foreach (int key in select.Keys)
            {
                if ((value & key) > 0)
                    flags.Add(select[key]);
            }
            return String.Join(", ", flags);
        }
    }

    public class CastFlagsParameter : FlagParameter
    {
        private static Dictionary<int, string> flags = new Dictionary<int, string>() {{1, "SMARTCAST_INTERRUPT_PREVIOUS"}, {2, "SMARTCAST_TRIGGERED"}, {0x20, "SMARTCAST_AURA_NOT_PRESENT"}, {0x40, "SMARTCAST_COMBAT_MOVE"}};
        public CastFlagsParameter(string name) : base (name, flags) { }
    }

    public class BoolParameter : SwitchParameter
    {
        private static Dictionary<int, string> values = new Dictionary<int, string>() { { 0, "False" }, { 1, "True" } };
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
        public StringParameter(String name) : base(name) { }
        public StringParameter(String name, String description) : base(name, description) { }

        protected abstract String StringValue();

        public override void SetValue(int value)
        {
            base.SetValue(value);
            str = StringValue();
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

    class SpellParameter : StringParameter
    {
        public SpellParameter(String name) : base(name) {}
        public SpellParameter(String name, String description) : base(name, description) { }

        protected override string StringValue()
        {
            return StringsDB.GetInstance().GetSpellName(GetValue());
        }
    }

    class EmoteParameter : StringParameter
    {
        public EmoteParameter(String name) : base(name) { }
        public EmoteParameter(String name, String description) : base(name, description) { }

        protected override string StringValue()
        {
            return StringsDB.GetInstance().GetEmoteName(GetValue());
        }
    }

    class CreatureParameter : StringParameter
    {
        public CreatureParameter(String name) : base(name) { }
        public CreatureParameter(String name, String description) : base(name, description) { }

        protected override string StringValue()
        {
            return StringsDB.GetInstance().GetCreatureName(GetValue());
        }
    }

    class QuestParameter : StringParameter
    {
        public QuestParameter(String name) : base(name) { }
        public QuestParameter(String name, String description) : base(name, description) { }

        protected override string StringValue()
        {
            return StringsDB.GetInstance().GetQuestName(GetValue());
        }
    }

    class SoundParameter : StringParameter
    {
        public SoundParameter(String name) : base(name) { }
        public SoundParameter(String name, String description) : base(name, description) { }

        protected override string StringValue()
        {
            return StringsDB.GetInstance().GetSoundName(GetValue());
        }
    }

    class ItemParameter : StringParameter
    {
        public ItemParameter(String name) : base(name) { }
        public ItemParameter(String name, String description) : base(name, description) { }

        protected override string StringValue()
        {
            return StringsDB.GetInstance().GetItemName(GetValue());
        }
    }

    class MovieParameter : StringParameter
    {
        public MovieParameter(String name) : base(name) { }
        public MovieParameter(String name, String description) : base(name, description) { }

        protected override string StringValue()
        {
            return StringsDB.GetInstance().GetMovieName(GetValue());
        }
    }


    class ZoneAreaParameter : StringParameter
    {
        public ZoneAreaParameter(String name) : base(name) { }
        public ZoneAreaParameter(String name, String description) : base(name, description) { }

        protected override string StringValue()
        {
            return StringsDB.GetInstance().GetZoneAreaName(GetValue());
        }
    }
}
