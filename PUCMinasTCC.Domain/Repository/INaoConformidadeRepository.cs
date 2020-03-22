using PUCMinasTCC.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Domain.Repository
{
    public interface INaoConformidadeRepository
    {
        Task<NaoConformidade> Get(int id);
        Task<IList<NaoConformidade>> ToListAsync(NaoConformidade filtro);
        void Gerenciar(NaoConformidade value);
    }
}
