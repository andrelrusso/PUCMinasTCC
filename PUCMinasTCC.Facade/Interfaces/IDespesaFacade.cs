using PUCMinasTCC.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Facade.Interfaces
{
    public interface IDespesaFacade
    {
        Task<Despesa> Get(int id);
        Task<IList<Despesa>> ToListAsync(Despesa filtro);
        void Gerenciar(Despesa value);
    }
}
