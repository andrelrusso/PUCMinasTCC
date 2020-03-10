
using PUCMinasTCC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUCMinasTCC.Models
{
    public class BaseModel<T>
    {
        public PagedList<T> Itens { get; set; }
        public virtual T Filtro { get; set; }
        public T Detalhe { get; set; }

    }
}
