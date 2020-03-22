using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PUCMinasTCC.Domain.Repository;
using PUCMinasTCC.Util.Util;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Facade.Interfaces;

namespace PUCMinasTCC.Facade.Facades
{
    public class UsuarioFacade : IUsuarioFacade
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IAppSettings appSettings;
        public UsuarioFacade(IUsuarioRepository usuarioRepository, IAppSettings appSettings)
        {
            this.usuarioRepository = usuarioRepository;
            this.appSettings = appSettings;
        }
        public void Gerenciar(Usuario value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.IdUsuario == 0)
            {
                if (!Helper.IsCPF(value.CPF.ToString()))
                    throw new Exception("CPF inválido");
                value.DescSenha = CryptoUtils.Encrypt("1234", appSettings.KeyCrypto);
            }
            usuarioRepository.Gerenciar(value);
        }

        //public void GerenciarVinculoEmpresa(UsuarioEmpresa value)
        //{
        //    if (value == null) throw new ArgumentNullException(nameof(value));
        //    usuarioRepository.GerenciarVinculoEmpresa(value);
        //}

        //public void GerenciarVinculoPerfil(PerfilUsuario value)
        //{
        //    if (value == null) throw new ArgumentNullException(nameof(value));

        //    if (value.UsuarioEmpresa?.IdUsuarioEmp == 0)
        //    {
        //        value.UsuarioEmpresa = ListarEmpresas(
        //            new UsuarioEmpresa
        //            {
        //                Empresa = value.UsuarioEmpresa.Empresa,
        //                Usuario = value.UsuarioEmpresa.Usuario,
        //                Status = Domain.Types.enumStatus.Ativo
        //            }).Result.FirstOrDefault();
        //    }

        //    usuarioRepository.GerenciarVinculoPerfil(value);
        //}

        public async Task<Usuario> Get(int id) => await usuarioRepository.Get(id);

        //public async Task<IList<UsuarioEmpresa>> ListarEmpresas(UsuarioEmpresa filtro) => await usuarioRepository.ListarEmpresas(filtro);

        //public async Task<IList<PerfilUsuario>> ListarPerfis(PerfilUsuario filtro) => await usuarioRepository.ListarPerfis(filtro);

        public async Task<IList<Usuario>> ToListAsync(Usuario filtro, int? idPerfilSistema = null) => await usuarioRepository.ToListAsync(filtro, idPerfilSistema);
    }
}
