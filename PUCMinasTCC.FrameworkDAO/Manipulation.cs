using PUCMinasTCC.FrameworkDAO.Attributes;
using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace PUCMinasTCC.FrameworkDAO
{
    public class Manipulation
    {
        public static string GetTableName<TEntity>() where TEntity : new()
        {
            try
            {
                if (typeof(TEntity).GetCustomAttributes(typeof(TableNameAttribute), false).Length > 0)
                    return ((TableNameAttribute)typeof(TEntity).GetCustomAttributes(typeof(TableNameAttribute), false)[0]).Name;
                else if (typeof(TEntity).GetCustomAttributes(typeof(DescriptionAttribute), false).Length > 0)
                    return ((DescriptionAttribute)typeof(TEntity).GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
                else
                    return typeof(TEntity).Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T GetAttribute<T>(PropertyInfo property)
        {
            if (property.GetCustomAttributes(typeof(T), false).Length > 0)
                return (T)property.GetCustomAttributes(typeof(T), false)[0];

            return default;
        }

        public static string GetDescription(PropertyInfo valor)
        {
            if (valor != null)
            {
                if (valor.GetCustomAttributes(typeof(ColumnAttribute), false).Length > 0)
                    return ((ColumnAttribute)valor.GetCustomAttributes(typeof(ColumnAttribute), false)[0]).Name;
                else if (valor.GetCustomAttributes(typeof(DescriptionAttribute), false).Length > 0)
                    return ((DescriptionAttribute)valor.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
                else
                    return valor.Name;
            }
            else
                return "Nulo";
        }

        public static object Typing(object valor, Type tipo)
        {
            try
            {
                if (valor != null && tipo != null)
                {
                    var isNullable = Nullable.GetUnderlyingType(tipo) != null;

                    if (isNullable)
                        return Convert.ChangeType(valor, Nullable.GetUnderlyingType(tipo));
                    else
                    {
                        if (tipo.BaseType == typeof(Enum))
                            return Convert.ChangeType(valor, typeof(int));
                        else
                            return Convert.ChangeType(valor, tipo);
                    }
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static object Typing(object valor, Type tipo, DbType dataTipo)
        {
            try
            {
                if (valor != null && tipo != null)
                {
                    var isNullable = Nullable.GetUnderlyingType(tipo) != null;

                    if (isNullable)
                        return Convert.ChangeType(valor, Nullable.GetUnderlyingType(tipo));
                    else
                    {
                        if (tipo.BaseType == typeof(Enum))
                        {
                            if (dataTipo == DbType.String)
                                return Enum.Parse(tipo, valor.ToString());
                            else
                                return Convert.ChangeType(valor, typeof(int));
                        }
                        else
                            return Convert.ChangeType(valor, tipo);
                    }
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
