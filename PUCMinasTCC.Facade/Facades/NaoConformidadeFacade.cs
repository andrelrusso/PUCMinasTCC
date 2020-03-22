using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Repository;
using PUCMinasTCC.Facade.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Facade.Facades
{
    public class NaoConformidadeFacade : INaoConformidadeFacade
    {
        private readonly INaoConformidadeRepository repository;
        public NaoConformidadeFacade(INaoConformidadeRepository repository)
        {
            this.repository = repository;
        }
        public void Gerenciar(NaoConformidade value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            repository.Gerenciar(value);
        }

        public async Task<NaoConformidade> Get(int id) => await repository.Get(id);
        public async Task<IList<NaoConformidade>> ToListAsync(NaoConformidade filtro) => await repository.ToListAsync(filtro);
    }
}
