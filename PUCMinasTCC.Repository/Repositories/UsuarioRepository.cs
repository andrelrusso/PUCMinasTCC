using PUCMinasTCC.Domain.Context;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Repository.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbContext context;
        public UsuarioRepository(IDbContext context)
        {
            this.context = context;
        }
        public void Gerenciar(Usuario value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            context.InitProcedure("SpGerenciarUsuarios");
            context.AddParameter("IdUsuario", value.IdUsuario, p => p != 0);

            if (value.IdUsuario == 0)
                context.AddParameter("DescSenha", value.DescSenha);

            context.AddParameter("NomeUsuario", value.Nome);
            context.AddParameter("Login", value.Login);
            context.AddParameter("CPF", value.CPF);
            context.AddParameter("Email", value.Email, p => !string.IsNullOrWhiteSpace(p));
            context.AddParameter("CodStatus", (int)value.Status);
            context.AddParameter("IdUsuarioOperacao", value.IdUsuarioOperacao);
            value.IdUsuario = context.ExecuteScalar<int>();
        }

        //public void GerenciarVinculoEmpresa(UsuarioEmpresa value)
        //{
        //    if (value == null) throw new ArgumentNullException(nameof(value));
        //    context.InitProcedure("SpGerenciarUsuariosEmpresas");
        //    if (value.IdUsuarioEmp != 0)
        //        context.AddParameter("IdUsuarioEmp", value.IdUsuarioEmp, p => p != 0);
        //    else
        //    {
        //        context.AddParameter("IdUsuario", value.Usuario?.IdUsuario, p => p.HasValue);
        //        context.AddParameter("IdEmpresa", value.Empresa?.IdEmpresa, p => p.HasValue);
        //    }
        //    context.AddParameter("CodStatus", (int)value.Status);
        //    context.AddParameter("IdUsuarioOperacao", value.IdUsuarioOperacao);
        //    value.IdUsuarioEmp = context.ExecuteScalar<int>();
        //}

        //public void GerenciarVinculoPerfil(PerfilUsuario value)
        //{
        //    if (value == null) throw new ArgumentNullException(nameof(value));
        //    context.InitProcedure("SpGerenciarPerfisUsuarios");
        //    if (value.IdPerfilUsuario != 0)
        //        context.AddParameter("IdPerfilUsuario", value.IdPerfilUsuario, p => p != 0);
        //    else
        //    {
        //        context.AddParameter("IdUsuarioEmp", value.UsuarioEmpresa?.IdUsuarioEmp, p => p.HasValue);
        //        context.AddParameter("IdPerfilSistema", value.PerfilSistema?.IdPerfilSistema, p => p.HasValue);
        //    }
        //    context.AddParameter("CodStatus", (int)value.Status);
        //    context.AddParameter("IdUsuarioOperacao", value.IdUsuarioOperacao);
        //    value.IdPerfilUsuario = context.ExecuteScalar<int>();
        //}

        public async Task<Usuario> Get(int id)
        {
            if (id == 0) throw new ArgumentNullException(nameof(id));
            context.InitProcedure("SpBuscarUsuarios");
            context.AddParameter("IdUsuario", id);
            return await context.GetAsync<Usuario>();
        }

        //public async Task<IList<UsuarioEmpresa>> ListarEmpresas(UsuarioEmpresa filtro)
        //{
        //    if (filtro == null) throw new ArgumentNullException(nameof(filtro));
        //    context.InitProcedure("SpBuscarUsuariosEmpresas");
        //    context.AddParameter("IdUsuarioEmp", filtro.IdUsuarioEmp, p => p != 0);
        //    context.AddParameter("IdUsuario", filtro.Usuario?.IdUsuario, p => p.HasValue);
        //    context.AddParameter("IdEmpresa", filtro.Empresa?.IdEmpresa, p => p.HasValue);
        //    context.AddParameter("CodStatus", (int)filtro.Status, p => p != (int)SICCA.Domain.Types.enumStatus.Todos);
        //    return await context.ListAsync<UsuarioEmpresa>();
        //}

        //public async Task<IList<PerfilUsuario>> ListarPerfis(PerfilUsuario filtro)
        //{
        //    if (filtro == null) throw new ArgumentNullException(nameof(filtro));
        //    context.InitProcedure("SpBuscarPerfisUsuarios");
        //    context.AddParameter("IdPerfilUsuario", filtro.IdPerfilUsuario, p => p != 0);
        //    context.AddParameter("IdUsuarioEmp", filtro.UsuarioEmpresa.IdUsuarioEmp, p => p != 0);
        //    context.AddParameter("IdUsuario", filtro.UsuarioEmpresa?.Usuario?.IdUsuario, p => p.HasValue);
        //    context.AddParameter("IdEmpresa", filtro.UsuarioEmpresa?.Empresa?.IdEmpresa, p => p.HasValue);
        //    context.AddParameter("IdPerfil", filtro.PerfilSistema?.Perfil.IdPerfil, p => p.HasValue);
        //    context.AddParameter("CodStatus", (int)filtro.Status, p => p != (int)SICCA.Domain.Types.enumStatus.Todos);
        //    return await context.ListAsync<PerfilUsuario>();
        //}

        public async Task<IList<Usuario>> ToListAsync(Usuario filtro, int? idPerfilSistema = null)
        {
            context.InitProcedure("SpBuscarUsuarios");
            if (filtro != null)
            {
                context.AddParameter("IdUsuario", filtro.IdUsuario, p => p != 0);
                context.AddParameter("NomeUsuario", filtro.Nome, p => !string.IsNullOrWhiteSpace(p));
                context.AddParameter("LoginUsuario", filtro.Login, p => !string.IsNullOrWhiteSpace(p));
                context.AddParameter("CPF", filtro.CPF, p => p != 0);
                context.AddParameter("Email", filtro.Email, p => !string.IsNullOrWhiteSpace(p));
                context.AddParameter("CodStatus", (int)filtro.Status, p => p != (int)enumStatus.Todos);
                context.AddParameter("IdPerfilSistema", idPerfilSistema, p => p.HasValue);
            }
            return await context.ListAsync<Usuario>();
        }
    }
}
