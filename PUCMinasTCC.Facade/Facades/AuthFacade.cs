using Newtonsoft.Json;
using PUCMinasTCC.Domain.Entity.AuthData;
using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.Domain.Repository;
using PUCMinasTCC.Facade.Interfaces;
using PUCMinasTCC.Util.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUCMinasTCC.Facade.Facades
{
    public class AuthFacade : IAuthFacade
    {
        private readonly IAuthRepository authRepository;
        private readonly IUsuarioRepository usuarioRepository;
        public AuthFacade(IAuthRepository authRepository, IUsuarioRepository usuarioRepository)
        {
            this.authRepository = authRepository;
            this.usuarioRepository = usuarioRepository;
        }
        public async Task<IAuthenticationResponse> LogIn(object userData, string key)
        {
            var authData = JsonConvert.DeserializeObject<AuthData>(userData.ToString());

           //if (authData.SystemCode == 0) throw new Exception("Código do sistema não informado");
            if (authData.UserIdentity == null) throw new ArgumentNullException(nameof(IAuthData.UserIdentity));
            if (authData.KeyContent == null) throw new ArgumentNullException(nameof(IAuthData.KeyContent));

            IAuthenticationResponse response = null;

            IAuthData data = null;

            switch (authData.LoginType)
            {
                default:
                case enumLoginType.PassPhrase:
                    data = JsonConvert.DeserializeObject<AuthDataFromPassPhrase>(userData.ToString());
                    response = await AuthWithLogin(data, key);
                    break;
            }

            return response;
        }

        private async Task<IAuthenticationResponse> AuthWithLogin(IAuthData authData, string key)
        {
            var data = (AuthDataFromPassPhrase)authData;
            var response = new AuthenticationResponse();
            try
            {
                response.User = await authRepository.LogIn(data.UserIdentity.ToString(), data.SystemCode);
                if (response.User != null)
                {
                    var senha = CryptoUtils.Decrypt(response.User.DescSenha, key);
                    response.Logged = string.Equals(senha, authData.KeyContent.ToString());
                    if (response.Logged)
                    {
                        response.User.DescSenha = string.Empty;
                        authRepository.LogarTentativaAcesso(authData.UserIdentity.ToString(), authData.SystemCode, response.Logged, response.User.IdUsuario);
                    }
                    else
                        throw new Exception("Usuário/Senha inválido!");
                }
                else
                    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                response.Logged = false;
                authRepository.LogarTentativaAcesso(authData.UserIdentity.ToString(), authData.SystemCode, response.Logged, response.User?.IdUsuario, ex.Message);
                response.User = null;
                response.Errors = response.Errors ?? new List<string>();
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        //public async Task<IList<Permissao>> ListaPermissoes(Permissao filtro)
        //{
        //    return await authRepository.ListaPermissoes(filtro);
        //}

        //public async Task<IList<Empresa>> ListaEmpresas(UsuarioEmpresa filtro)
        //{
        //    var list = await usuarioRepository.ListarEmpresas(filtro);
        //    return list.Select(r => r.Empresa).ToList();
        //}

        public async Task<bool> ChangePassword(AuthDataFromPassPhrase userData, string newPassword, string confirmPassword, string key)
        {
            if (userData.SystemCode == 0) throw new Exception("Código do sistema não informado");
            if (userData.UserIdentity == null) throw new ArgumentNullException(nameof(IAuthData.UserIdentity));
            if (userData.KeyContent == null) throw new ArgumentNullException(nameof(IAuthData.KeyContent));

            var response = new AuthenticationResponse();
            response.User = await authRepository.LogIn(userData.UserIdentity.ToString(), userData.SystemCode);
            if (response.User != null)
            {
                var senha = CryptoUtils.Decrypt(response.User.DescSenha, key);
                response.Logged = string.Equals(senha, userData.KeyContent.ToString());
                if (response.Logged)
                {
                    if (!newPassword.Equals(confirmPassword))
                        throw new Exception("Nova senha e confirmação não podem ser diferentes");

                    if (senha.Equals(newPassword))
                        throw new Exception("Nova senha tem que ser diferente da anterior");

                    authRepository.AlterarSenha(response.User.IdUsuario, CryptoUtils.Encrypt(newPassword, key));
                }
                else
                    throw new Exception("Usuário/Senha inválido!");
            }
            else
                throw new Exception("Usuário não encontrado");

            return true;
        }
    }
}
