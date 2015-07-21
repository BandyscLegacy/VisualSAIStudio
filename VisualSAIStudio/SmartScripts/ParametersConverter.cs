using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace VisualSAIStudio.SmartScripts
{
    public class ParametersConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
           // return sourceType == typeof(Parameter[]) || base.CanConvertFrom(context, sourceType);
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context,
                                  System.Type destinationType)
        {
            return true;
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                         System.Globalization.CultureInfo culture,
                         object value, Type destType)
        {

            if (destType == typeof(string) && value is Parameter[])
            {
                Parameter[] elems = (Parameter[])value;
                return "Check while typing:" + false +
                       ", check CAPS: " + true +
                       ", suggest corrections: " + 3;
            }
            System.Windows.Forms.MessageBox.Show(value.ToString() + " "+destType);
            return base.ConvertTo(context, culture, value, destType);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string stringValue;
            object result;

            result = null;
            stringValue = value as string;

           /* if (!string.IsNullOrEmpty(stringValue))
            {
                int nonDigitIndex;

                nonDigitIndex = stringValue.IndexOf(stringValue.FirstOrDefault(char.IsLetter));

                if (nonDigitIndex > 0)
                {
                    result = new Length
                    {
                        Value = Convert.ToSingle(stringValue.Substring(0, nonDigitIndex)),
                        Unit = (Unit)Enum.Parse(typeof(Unit), stringValue.Substring(nonDigitIndex), true)
                    };
                }
            }*/

            return result ?? base.ConvertFrom(context, culture, value);
        }
    }
}
