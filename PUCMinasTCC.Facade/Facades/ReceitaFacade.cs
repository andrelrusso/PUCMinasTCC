using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PUCMinasTCC.Domain.Repository;
using PUCMinasTCC.Util.Util;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Facade.Interfaces;

namespace PUCMinasTCC.Facade.Facades
{
    public class ReceitaFacade : IReceitaFacade
    {
        private readonly IReceitaRepository receitaRepository;
        public ReceitaFacade(IReceitaRepository receitaRepository)
        {
            this.receitaRepository = receitaRepository;
        }
        public void Gerenciar(Receita value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            receitaRepository.Gerenciar(value);
        }
        public async Task<Receita> Get(int id) => await receitaRepository.Get(id);
        public async Task<IList<Receita>> ToListAsync(Receita filtro) => await receitaRepository.ToListAsync(filtro);
    }
}
