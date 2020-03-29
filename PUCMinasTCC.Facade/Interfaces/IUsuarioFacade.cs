using PUCMinasTCC.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Facade.Interfaces
{
    public interface IUsuarioFacade
    {
        Task<Usuario> Get(int id);
        Task<IList<Usuario>> ToListAsync(Usuario filtro, int? idPerfilSistema = null);
        void Gerenciar(Usuario value);
    }
}
