using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUCMinasTCC.Utils
{
    public static class ListExtensions
    {
        public const string DESCRIPTION_ALL = "Todos";
        public static Task<PagedList<T>> ToPagedListAsync<T>(this Task<IList<T>> itens, int pageSize, int pageIndex)
        {
            return Task.Run(() => itens.Result.ToPagedList(pageSize, pageIndex));
        }
        public static PagedList<T> ToPagedList<T>(this Task<IList<T>> itens, int pageSize, int pageIndex)
        {
            return new PagedList<T>(itens.Result, pageSize, pageIndex);
        }
        public static PagedList<T> ToPagedList<T>(this IList<T> itens, int pageSize, int pageIndex)
        {
            return new PagedList<T>(itens, pageSize, pageIndex);
        }
        public static IList<T> AddAllToList<T>(this IList<T> items, string dataTextField, string value = DESCRIPTION_ALL) where T : class, new()
        {
            if (items == null) items = new List<T>();
            var newItems = items.ToList();
            var newItem = new T();
            newItem.GetType().GetProperty(dataTextField).SetValue(newItem, value);
            newItems.Insert(0, newItem);
            return newItems;
        }

        public static void RemoveAll<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list.Any(predicate))
                list.Remove(list.First(predicate));
        }
    }
}
