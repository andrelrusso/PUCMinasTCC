using PUCMinasTCC.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Domain.Repository
{
    public interface IReceitaRepository
    {
        Task<Receita> Get(int id);
        Task<IList<Receita>> ToListAsync(Receita filtro);
        void Gerenciar(Receita value);
    }
}
