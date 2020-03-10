using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUCMinasTCC.Utils
{
    public class PagedList<T> : List<T>
    {
        public int TotalPages => (int)Math.Ceiling((decimal)(Itens.Count / PageSize));
        public int ActualPage { get; private set; }
        public int PageSize { get; private set; } = 10;
        public bool HasPreviousPage => ActualPage > 1;
        public bool HasNextPage => ActualPage < TotalPages;
        public bool IsFirstPage => ActualPage == 1;
        public bool IsLastPage => ActualPage == TotalPages;
        private IList<T> Itens { get; set; }
        public IList<T> CurrentItens
        {
            get => Itens?.Skip((ActualPage - 1) * PageSize).Take(PageSize).ToList();
        }
        public PagedList(IList<T> itens, int _pageSize, int _actualPage)
        {
            PageSize = _pageSize;
            ActualPage = _actualPage;
            Itens = itens;
        }
        public PagedList(Task<IList<T>> itens, int _pageSize, int _actualPage)
        {
            PageSize = _pageSize;
            ActualPage = _actualPage;
            Itens = itens.Result;
        }

    }

}
