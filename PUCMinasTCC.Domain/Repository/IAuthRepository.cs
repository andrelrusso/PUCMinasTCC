using PUCMinasTCC.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUCMinasTCC.Domain.Repository
{
    public interface IAuthRepository
    {
        Task<Usuario> LogIn(string userName, long systemCode);
        void LogarTentativaAcesso(string identidade, long idSistema, bool status, int? idUsuario = null, string erro = null);
        //Task<IList<Permissao>> ListaPermissoes(Permissao filtro);
        void AlterarSenha(int idUsuario, string novaSenha);
    }
}
