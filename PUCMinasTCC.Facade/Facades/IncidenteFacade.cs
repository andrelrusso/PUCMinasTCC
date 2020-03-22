using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Repository;
using PUCMinasTCC.Facade.Interfaces;

namespace PUCMinasTCC.Facade.Facades
{
    public class IncidenteFacade : IIncidenteFacade
    {
        private readonly IIncidenteRepository repository;
        public IncidenteFacade(IIncidenteRepository repository)
        {
            this.repository = repository;
        }
        public void Gerenciar(Incidente value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            repository.Gerenciar(value);
        }

        public async Task<Incidente> Get(int id) => await repository.Get(id);
        public async Task<IList<Incidente>> ToListAsync(Incidente filtro) => await repository.ToListAsync(filtro);
    }
}
