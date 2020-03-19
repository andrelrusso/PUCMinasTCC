using PUCMinasTCC.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUCMinasTCC.Facade.Interfaces
{
    public interface IIncidenteFacade
    {
        Task<Incidente> Get(int id);
        Task<IList<Incidente>> ToListAsync(Incidente filtro);
        void Gerenciar(Incidente value);
    }
}
