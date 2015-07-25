using SmartFormat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{
    public abstract class ParameterConditional
    {
        public Parameter compareTo { get; set; }
        public Parameter compared { get; set; }
        public WarningType warningType { get; set; }
        public string description { get; set; }

        public ParameterConditional(WarningType warningType, string description) 
        {
            this.warningType = warningType;
            this.description = description;
        }

        public ParameterConditional(Parameter compared, WarningType warningType)
        {
            this.compared = compared;
            this.warningType = warningType;
        }

        public ParameterConditional(Parameter compared, Parameter compareTo, WarningType warningType, string description = null)
        {
            this.compared = compared;
            this.compareTo = compareTo;
            this.warningType = warningType;
            this.description = description;
        }

        public ParameterConditional(Parameter compared, int value, WarningType warningType, string description = null) : this(compared, new Parameter(value.ToString(), value), warningType, description)
        { 
        }

        public void SetCompareTo(Parameter parameter)
        {
            compareTo = parameter;
        }

        public void SetCompareTo(int parametr)
        {
            SetCompareTo(new Parameter(parametr.ToString(), parametr));
        }

        public void SetDescription(string descripion)
        {
            this.description = Smart.Format(description, new { compared = compared.name, compareto = compareTo.name });
        }

        public abstract bool Validate();

        public static ParameterConditional Factory(string type, Parameter parameter)
        {
            switch (type)
            {
                case "CompareValue":
                    return new ParameterConditionalCompareValue(parameter);
            }
            return null;
        }
    }

    public class ParameterConditionalCompareValue : ParameterConditional
    {
        private CompareType compareType;
        private int max;

        public ParameterConditionalCompareValue(Parameter compared) : base(compared, WarningType.INVALID_VALUE) { }

        public ParameterConditionalCompareValue(Parameter compared, Parameter compareTo, CompareType compareType)
            : base(compared, compareTo, WarningType.INVALID_PARAMETER)
        {
            this.compareType = compareType;
            this.description = compared.name + " must be " + compareType.GetDescription() + " " + compareTo.name;
        }

        public ParameterConditionalCompareValue(Parameter compared, int compareTo, CompareType compareType)
            : base(compared, compareTo, WarningType.INVALID_PARAMETER)
        {
            this.compareType = compareType;
            this.description = compared.name + " must be " + compareType.GetDescription() + " " + compareTo;
        }

        public ParameterConditionalCompareValue(Parameter compared, int compareTo, CompareType compareType, string description)
            : base(compared, compareTo, WarningType.INVALID_PARAMETER)
        {
            this.compareType = compareType;
            this.description = description;
        }

        public ParameterConditionalCompareValue(Parameter compared, int min, int max)
            : base(compared, min, WarningType.INVALID_PARAMETER)
        {
            this.compareType = CompareType.BETWEEN;
            this.max = max;
            this.description = compared.name + " must be between "+min+" and "+max;
        }


        public void SetCompareType(CompareType compareType)
        {
            this.compareType = compareType;
            this.description = compared.name + " must be " + compareType.GetDescription() + " " + compareTo.name;
        }

        public override bool Validate()
        {
            switch (compareType)
            {
                case CompareType.EQUALS:
                    return compared.GetValue() == compareTo.GetValue();
                case CompareType.NOT_EQUALS:
                    return compared.GetValue() != compareTo.GetValue();
                case CompareType.LOWER_THAN:
                    return compared.GetValue() < compareTo.GetValue();
                case CompareType.GREATER_THAN:
                    return compared.GetValue() > compareTo.GetValue();
                case CompareType.LOWER_OR_EQUALS:
                    return compared.GetValue() <= compareTo.GetValue();
                case CompareType.GREATER_OR_EQUALS:
                    return compared.GetValue() >= compareTo.GetValue();
                case CompareType.BETWEEN:
                    return compared.GetValue() >= compareTo.GetValue() && compared.GetValue() <= max;
            }
            return false;
        }
    }

    public class ParameterConditionalFlag : ParameterConditional
    {
        private List<int> flags;

        public ParameterConditionalFlag(Parameter compared, List<int> flags) : base(compared, WarningType.INVALID_VALUE)
        {
            this.flags = flags;
        }

        public override bool Validate()
        {
            int value = compared.GetValue();
            foreach (int flag in flags)
                value = value & ~ flag;
            if (value > 0)
            {
                List<int> unsupported_flags = new List<int>();
                string bits = Convert.ToString(value, 2);
                for (int i = 0; i < bits.Length; ++i)
                {
                    if (bits[i]=='1')
                    {
                        unsupported_flags.Add((int)Math.Pow(2, (bits.Length-i-1)));
                    }
                }
                this.description = compared.name + " contains unsupported flags: "+String.Join(", ", unsupported_flags);
                return false;
            }
            return true;
        }
    }

    public class ParameterConditionalGroup : ParameterConditional
    {
        private List<ParameterConditional> conditionals;
        public ParameterConditionalGroup(List<ParameterConditional> conditionals, string description)
            : base(WarningType.INVALID_PARAMETER, description)
        {
            this.conditionals = conditionals;
        }
        public override bool Validate()
        {
            foreach (ParameterConditional conditional in conditionals)
            {
                if (!conditional.Validate())
                    return false;
            }
            return true;
        }
    }

    public class ParameterConditionalDBExists : ParameterConditional
    {
        private StorageType storage;
        public ParameterConditionalDBExists(Parameter compared, StorageType storage)
            : base(compared, WarningType.INVALID_VALUE)
        {
            this.storage = storage;
            this.description = storage.ToString() + " doesn't exist";
        }

        public override bool Validate()
        {
            if (compared.GetValue() == 0)
                return true;
            return StringsDB.GetInstance().Exists(storage, compared.GetValue());
        }
    }

    public class ParameterConditionalInversed : ParameterConditional
    {
        private ParameterConditional conditional;
        public ParameterConditionalInversed (ParameterConditional conditional) : base (conditional.compared, conditional.compareTo, conditional.warningType, conditional.description)
        {
            this.conditional = conditional;
        }

        public override bool Validate()
        {
            return !conditional.Validate();
        }
    }
    public enum CompareType
    {
        [Description("equal")]
        EQUALS,
        [Description("not equal")]
        NOT_EQUALS,
        [Description("lower than")]
        LOWER_THAN,
        [Description("greater than")]
        GREATER_THAN,
        [Description("lower or equal")]
        LOWER_OR_EQUALS,
        [Description("greater or equal")]
        GREATER_OR_EQUALS,
        [Description("between")]
        BETWEEN,
    }
}
