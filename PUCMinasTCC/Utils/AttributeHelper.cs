using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PUCMinasTCC.Utils
{
    public static class AttributeHelper
    {
        public static string GetEnumDescription(object param)
        {
            string retorno = "";

            var value = (Enum)param;
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null &&
                attributes.Length > 0)
                retorno = attributes[0].Description;
            else
                retorno = param.ToString();


            return retorno;
        }
    }
}
