using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PUCMinasTCC.FrameworkDAO.Extensions
{
    public static class DataSetExtensions
    {
        /// <summary>
        /// Verifica se o data set contém valores
        /// </summary>
        /// <param name="ds">Dataset em questão</param>
        /// <returns>Retorna Verdadeiro se contém valores e falso caso não contenha</returns>
        public static bool IsDataEmpty(this DataSet ds)
        {
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return false;
            else
                return true;
        }

        public static IList<T> ToList<T>(this DataSet ds) where T : new()
        {
            if (!ds.IsDataEmpty())
            {
                return ds.Tables[Manipulation.GetTableName<T>()].Rows.OfType<DataRow>().ToList<T>();
            }

            return null;
        }

        public static T GetFirstEntity<T>(this DataSet ds) where T : new()
        {
            if (!ds.IsDataEmpty())
            {
                return ds.Tables[Manipulation.GetTableName<T>()].Rows[0].ToEntity<T>();
            }
            else
                return default;
        }
    }
}
