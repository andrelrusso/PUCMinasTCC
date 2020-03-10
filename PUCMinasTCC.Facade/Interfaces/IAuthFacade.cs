
using PUCMinasTCC.Domain.Entity.AuthData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Facade.Interfaces
{
    public interface IAuthFacade
    {
        Task<bool> ChangePassword(AuthDataFromPassPhrase userData, string newPassword, string confirmPassword, string key);
        Task<IAuthenticationResponse> LogIn(object userData, string key);
        //Task<IList<Permissao>> ListaPermissoes(Permissao filtro);
       // Task<IList<Empresa>> ListaEmpresas(UsuarioEmpresa filtro);
    }
}
