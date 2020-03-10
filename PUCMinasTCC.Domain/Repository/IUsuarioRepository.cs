using PUCMinasTCC.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Domain.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Get(int id);
        Task<IList<Usuario>> ToListAsync(Usuario filtro, int? idPerfilSistema = null);
        void Gerenciar(Usuario value);
        //void GerenciarVinculoPerfil(PerfilUsuario value);
        //Task<IList<PerfilUsuario>> ListarPerfis(PerfilUsuario filtro);
        //void GerenciarVinculoEmpresa(UsuarioEmpresa value);
        //Task<IList<UsuarioEmpresa>> ListarEmpresas(UsuarioEmpresa filtro);
    }
}
