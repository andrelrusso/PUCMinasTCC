using PUCMinasTCC.Domain.Context;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Repository.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbContext context;
        public AuthRepository(IDbContext context)
        {
            this.context = context;
        }
        public async Task<Usuario> LogIn(string userName, long systemCode)
        {
            context.InitProcedure("SpLogarUsuario");
            context.AddParameter("Identidade", userName);
            //context.AddParameter("IdSistema", systemCode);
            return await context.GetAsync<Usuario>();
        }

        public void LogarTentativaAcesso(string identidade, long idSistema, bool status, int? idUsuario = null, string erro = null)
        {
            if (string.IsNullOrWhiteSpace(identidade)) throw new ArgumentNullException(nameof(identidade));
            //if (idSistema == 0) throw new ArgumentNullException(nameof(idSistema));

            context.InitProcedure("SpTentativasLogin");
            context.AddParameter("Identidade", identidade);
            //context.AddParameter("IdSistema", idSistema);
            context.AddParameter("CodStatusTentativa", status);
            context.AddParameter("IdUsuario", idUsuario, p => p.HasValue);
            context.AddParameter("DescErro", erro, p => !string.IsNullOrWhiteSpace(p));
            context.Execute();
        }

        //public async Task<IList<Permissao>> ListaPermissoes(Permissao filtro)
        //{
        //    if (filtro == null) throw new ArgumentNullException(nameof(filtro));
        //    context.InitProcedure("SpBuscarAcessosUsuario");
        //    context.AddParameter("IdUsuario", filtro.Usuario?.IdUsuario);
        //    context.AddParameter("IdEmpresa", filtro.Empresa.IdEmpresa);
        //    context.AddParameter("IdSistema", filtro.Sistema.IdSistema);
        //    return await context.ListAsync<Permissao>();
        //}
        public void AlterarSenha(int idUsuario, string novaSenha)
        {
            context.InitProcedure("SpAlterarSenha");
            context.AddParameter("IdUsuario", idUsuario);
            context.AddParameter("DescSenha", novaSenha);
            context.Execute();
        }
    }
}
