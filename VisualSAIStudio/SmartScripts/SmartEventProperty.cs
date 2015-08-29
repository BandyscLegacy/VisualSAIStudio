using DynamicTypeDescriptor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{

    


    public class SmartElementProperty
    {
        protected SmartElement element;
        public DynamicCustomTypeDescriptor m_dctd = null;

        [ReadOnly(true)]
        public string name
        {
            get
            {
                return element.name;
            }
        }

        [CategoryAttribute("Parameters"),
        Id(0, 0)]
        [Browsable(false)]
        public int pram1 {
            get { return element.parameters[0].GetValue(); }
            set { element.UpdateParams(0, value); }
        }

        [CategoryAttribute("Parameters"),
        Id(1, 0)]
        [Browsable(false)]
        public int pram2
        {
            get { return element.parameters[1].GetValue(); }
            set { element.UpdateParams(1, value); }
        }

        [CategoryAttribute("Parameters")]
        [Id(2, 0)]
        [Browsable(false)]
        public int pram3
        {
            get { return element.parameters[2].GetValue(); }
            set { element.UpdateParams(2, value); }
        }

        [CategoryAttribute("Parameters")]
        [Id(3, 0)]
        [Browsable(false)]
        public int pram4
        {
            get { return (element.parameters.Length>=4?element.parameters[3].GetValue():0); }
            set { element.UpdateParams(3, value); }
        }

        [CategoryAttribute("Parameters")]
        [Id(4, 0)]
        [Browsable(false)]
        public int pram5
        {
            get { return (element.parameters.Length>=5?element.parameters[4].GetValue():0); }
            set { element.UpdateParams(4, value); }
        }

        [CategoryAttribute("Parameters")]
        [Browsable(false)]
        [Id(5, 0)]
        public int pram6
        {
            get { return (element.parameters.Length>=6?element.parameters[5].GetValue():0); }
            set { element.UpdateParams(5, value); }
        }

        public SmartElementProperty(SmartElement element)
        {
            this.element = element;
            m_dctd = ProviderInstaller.Install(this);
            m_dctd.PropertySortOrder = CustomSortOrder.AscendingById;
            TypeDescriptor.Refresh(this);
            Parameter[] parameters = element.parameters;

            for (int i = 0; i < parameters.Length; ++i)
            {
                CustomPropertyDescriptor property = m_dctd.GetProperty("pram" + (i + 1));
                Init(property, parameters[i]);
            }
        }
        protected void Init(CustomPropertyDescriptor property, Parameter parameter)
        {
            if (!(parameter is NullParameter))
            {
                property.SetIsBrowsable(true);
                property.SetDescription(parameter.description);
                property.SetDisplayName(parameter.name);
                if (parameter is SwitchParameter)
                {
                    property.AddAttribute(new EditorAttribute(typeof(StandardValueEditor), typeof(UITypeEditor)));
                    ((SwitchParameter)parameter).AddValuesToProperty(property);
                }
                if (parameter is FlagParameter)
                    property.PropertyFlags |= PropertyFlags.IsFlag;

                if (parameter is StringParameter)
                {
                    property.AddAttribute(new TypeConverterAttribute(typeof(DataFromDBConverter)));
                    property.AddAttribute(new StorageTypeAttribute(((StringParameter)parameter).storageType));
                    property.AddAttribute(new EditorAttribute(typeof(ModalEditor), typeof(UITypeEditor)));
                }
            } else if ( parameter.GetValue()>0)
            {
                property.SetIsBrowsable(true);
                property.SetDisplayName("unused (" + property.DisplayName+")");
            }
        }
    }


    public class SmartEventProperty : SmartElementProperty
    {
        [CategoryAttribute("Event"),
        DisplayName("Phasemask")]
        public SmartPhaseMask phasemask { 
            get 
            {
                return ((SmartEvent)element).phasemask;
            }
            set
            {
                ((SmartEvent)element).phasemask = value; element.Invalide();
            } 
        }

        [CategoryAttribute("Event"),
        DisplayName("Flags")]
        public SmartEventFlag flags
        { 
            get 
            {
                return ((SmartEvent)element).flags;
            }
            set
            {
                ((SmartEvent)element).flags = value; element.Invalide();
            } 
        }
        

        [CategoryAttribute("Event"),
        DisplayName("Chance")]
        public int chance 
        {
            get
            {
                return ((SmartEvent)element).chance;
            }
            set 
            {
                if (value > 100 )
                    value = 100;
                else if (value < 0)
                    value = 0;
                ((SmartEvent)element).chance = value; element.Invalide();
            }
        }

        public SmartEventProperty(SmartEvent ev) : base (ev)
        {
            m_dctd.GetProperty("name").SetCategory("Event");
            m_dctd.GetProperty("name").SetDisplayName("Event name");
        }
    }

    public class SmartActionProperty : SmartElementProperty
    {
        SmartAction action;
        [CategoryAttribute("Action"),
        DisplayName("Comment")]
        public string comment
        {
            get { return ((SmartAction)element).Comment; }
            set { action.Comment = value; action.Invalide(); }
        }

        [CategoryAttribute("Target Position"),
         DisplayName("X")]
        public float target_x
        {
            get { return ((SmartAction)element).Target.position[0]; }
            set { action.Target.position[0] = value; action.Target.Invalide(); action.Invalide(); }
        }

        [CategoryAttribute("Target Position"),
        DisplayName("Y")]
        public float target_y
        {
            get { return ((SmartAction)element).Target.position[1]; }
            set { action.Target.position[1] = value; action.Target.Invalide();  action.Invalide(); }
        }

        [CategoryAttribute("Target Position")]
        [DisplayName("Z")]
        public float target_z
        {
            get { return ((SmartAction)element).Target.position[2]; }
            set { action.Target.position[2] = value; action.Target.Invalide(); action.Invalide(); }
        }

        [CategoryAttribute("Target Position")]
        [DisplayName("O")]
        public float target_o
        {
            get { return ((SmartAction)element).Target.position[3]; }
            set { action.Target.position[3] = value; action.Target.Invalide(); action.Invalide(); }
        }



        [CategoryAttribute("Target"),
        Id(0, 0)]
        [Browsable(false)]
        public int targetpram1
        {
            get { return ((SmartAction)element).Target.parameters[0].GetValue(); }
            set { ((SmartAction)element).Target.UpdateParams(0, value); element.Invalide(); }
        }

        [CategoryAttribute("Target"),
        Id(1, 0)]
        [Browsable(false)]
        public int targetpram2
        {
            get { return ((SmartAction)element).Target.parameters[1].GetValue(); }
            set { ((SmartAction)element).Target.UpdateParams(1, value); element.Invalide(); }
        }

        [CategoryAttribute("Target")]
        [Id(2, 0)]
        [Browsable(false)]
        public int targetpram3
        {
            get { return ((SmartAction)element).Target.parameters[2].GetValue(); }
            set { ((SmartAction)element).Target.UpdateParams(2, value); element.Invalide(); }
        }


        public SmartActionProperty(SmartAction action)
            : base(action)
        {
            this.action = action;
            m_dctd.GetProperty("name").SetCategory("Action");
            m_dctd.GetProperty("name").SetDisplayName("Action name");
            Parameter[] parameters = action.Target.parameters;
            for (int i = 0; i < 3; ++i)
            {
                CustomPropertyDescriptor property = m_dctd.GetProperty("targetpram" + (i + 1));
                Init(property, parameters[i]);
            }
        }
    }

    public class SmartConditionProperty : SmartElementProperty
    {
        [CategoryAttribute("Condition"),
        DisplayName("invert")]
        public bool invert
        {
            get
            {
                return ((SmartCondition)element).invert;
            }
            set
            {
                ((SmartCondition)element).invert = value; element.Invalide();
            }
        }

        [CategoryAttribute("Condition"),
        DisplayName("Target")]
        public ConditionTarget target
        {
            get
            {
                return ((SmartCondition)element).target;
            }
            set
            {
                ((SmartCondition)element).target = value; element.Invalide();
            }
        }

        public SmartConditionProperty(SmartCondition ev)
            : base(ev)
        {
            m_dctd.GetProperty("name").SetCategory("Condition");
            m_dctd.GetProperty("name").SetDisplayName("Condition name");
        }
    }

}
