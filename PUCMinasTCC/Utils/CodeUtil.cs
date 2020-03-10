using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace PUCMinasTCC.Utils
{
    public class CodeUtil
    {
        public static SelectList PopulaComboComEnum<T>() =>  PopulaComboComEnum(default(T));
        public static SelectList PopulaComboComEnum<T>(T selectedItem)
        {
            var itens = GetEnumValue<T>().Select(r => new SelectListItem { Text = r.Value, Value = r.Key.ToString() }).ToList();

            if (selectedItem != null)
                return new SelectList(itens, "Value", "Text", selectedItem);
            else
                return new SelectList(itens, "Value", "Text");
        }
        public static SelectList PopulaComboComEnum<T>(T selectedItem, T removedItem)
        {
            var itens = GetEnumValue(removedItem).Select(r => new SelectListItem { Text = r.Value, Value = r.Key.ToString() }).ToList();

            if (selectedItem != null)
                return new SelectList(itens, "Value", "Text", selectedItem);
            else
                return new SelectList(itens, "Value", "Text");
        }
        public static SelectList PopulaComboComEnum(Dictionary<int, string> values) => PopulaComboComEnum(values, null);
        public static SelectList PopulaComboComEnum(Dictionary<int, string> values, object selectedItem)
        {
            var itens = values.Select(r => new SelectListItem { Text = r.Value, Value = r.Key.ToString() }).ToList();

            if (selectedItem != null)
                return new SelectList(itens, "Value", "Text", selectedItem);
            else
                return new SelectList(itens, "Value", "Text");
        }

        public static Dictionary<int, string> GetEnumValue<T>()
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(x => (int)Enum.Parse(x.GetType(), x.ToString()), v => AttributeHelper.GetEnumDescription(v));
            return list;
        }
        public static Dictionary<int, string> GetEnumValue<T>(T removeItem)
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(x => (int)Enum.Parse(x.GetType(), x.ToString()), v => AttributeHelper.GetEnumDescription(v));
            list.Remove(Convert.ToInt32(removeItem));
            return list;
        }

    }
}
