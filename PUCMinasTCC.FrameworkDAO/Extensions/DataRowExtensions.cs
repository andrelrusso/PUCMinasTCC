using PUCMinasTCC.FrameworkDAO.Attributes;
using PUCMinasTCC.FrameworkDAO.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PUCMinasTCC.FrameworkDAO.Extensions
{
    public static class DataRowExtensions
    {
        public static IList<T> ToList<T>(this IEnumerable<DataRow> rows) where T : new()
        {
            return rows.Select(row => row.ToEntity<T>()).ToList();
        }
        public static T ToEntity<T>(this DataRow dr) where T : new()
        {
            T entidade = default;

            if (dr != null)
            {
                entidade = new T();
                entidade = (T)BindToObject(dr, entidade);
            }

            return entidade;
        }

        public static object ToEntity(this DataRow dr, Type type)
        {
            object entidade = null;

            if (dr != null)
            {
                entidade = Activator.CreateInstance(type);
                entidade = BindToObject(dr, entidade);
            }

            return entidade;
        }

        private static object BindToObject(DataRow dr, object entidade)
        {
            //Pega as propriedades da entidade
            PropertyInfo[] classe = entidade.GetType().GetProperties();

            foreach (PropertyInfo propriedade in classe)
            {
                var associacao = Manipulation.GetAttribute<EntityFromSameQueryAttribute>(propriedade);

                if (associacao != default(EntityFromSameQueryAttribute))
                {
                    propriedade.SetValue(entidade, BindToObject(dr, Activator.CreateInstance(propriedade.PropertyType)), null);
                }
                else
                {
                    string nomeCampo = Manipulation.GetDescription(propriedade);
                    var atributo = Manipulation.GetAttribute<ColumnAttribute>(propriedade) ?? null;

                    if (dr.Table.Columns.Contains(nomeCampo.ToUpper()) && !dr[nomeCampo.ToUpper()].Equals(DBNull.Value))
                    {
                        if (propriedade.PropertyType.BaseType == typeof(Enum) && atributo != null)
                            propriedade.SetValue(entidade, Manipulation.Typing(dr[nomeCampo.ToUpper()], propriedade.PropertyType, atributo.DataType), null);
                        else
                            propriedade.SetValue(entidade, Manipulation.Typing(dr[nomeCampo.ToUpper()], propriedade.PropertyType), null);
                    }
                }

            }

            return entidade;
        }
    }
}
