using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public static class Extensions
    {
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            //by Ray Booysen: http://stackoverflow.com/questions/479410/enum-tostring-with-user-friendly-strings
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();

        }

        //http://stackoverflow.com/a/13100389/1616645
        public static T GetAttribute<T>(Enum enumValue) where T : Attribute
        {
            T attribute;

            System.Reflection.MemberInfo memberInfo = enumValue.GetType().GetMember(enumValue.ToString())
                                            .FirstOrDefault();

            if (memberInfo != null)
            {
                attribute = (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                return attribute;
            }
            return null;
        }

        public static R Single<R>(this MySqlDataReader reader, Func<MySqlDataReader, R> selector)
        {
            R result = default(R);
            if (reader.Read())
                result = selector(reader);
            if (reader.Read())
                throw new System.Data.DataException("multiple rows returned from query");
            return result;
        }
    }
}
