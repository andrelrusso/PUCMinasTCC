using PUCMinasTCC.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Domain.Repository
{
    public interface IIncidenteRepository
    {
        Task<Incidente> Get(int id);
        Task<IList<Incidente>> ToListAsync(Incidente filtro);
        void Gerenciar(Incidente value);
    }
}
