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
            context.AddParameter("IdPerfilUsuario", (int)value.PerfilUsuario);
            context.AddParameter("IdUsuarioOperacao", value.IdUsuarioOperacao);
            value.IdUsuario = context.ExecuteScalar<int>();
        }
       
        public async Task<Usuario> Get(int id)
        {
            if (id == 0) throw new ArgumentNullException(nameof(id));
            context.InitProcedure("SpBuscarUsuarios");
            context.AddParameter("IdUsuario", id);
            return await context.GetAsync<Usuario>();
        }

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
                context.AddParameter("IdPerfilUsuario", idPerfilSistema, p => p != (int)enumPerfilUsuario.Todos);
            }
            return await context.ListAsync<Usuario>();
        }
    }
}
