using PUCMinasTCC.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Domain.Repository
{
    public interface IDespesaRepository
    {
        Task<Despesa> Get(int id);
        Task<IList<Despesa>> ToListAsync(Despesa filtro);
        void Gerenciar(Despesa value);
        //void GerenciarVinculoPerfil(PerfilUsuario value);
        //Task<IList<PerfilUsuario>> ListarPerfis(PerfilUsuario filtro);
        //void GerenciarVinculoEmpresa(UsuarioEmpresa value);
        //Task<IList<UsuarioEmpresa>> ListarEmpresas(UsuarioEmpresa filtro);
    }
}
