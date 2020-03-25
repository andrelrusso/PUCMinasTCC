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
    public class DespesaFacade : IDespesaFacade
    {
        private readonly IDespesaRepository despesaRepository;
        public DespesaFacade(IDespesaRepository despesaRepository)
        {
            this.despesaRepository = despesaRepository;
        }
        public void Gerenciar(Despesa value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            despesaRepository.Gerenciar(value);
        }
        public async Task<Despesa> Get(int id) => await despesaRepository.Get(id);
        public async Task<IList<Despesa>> ToListAsync(Despesa filtro) => await despesaRepository.ToListAsync(filtro);
    }
}
