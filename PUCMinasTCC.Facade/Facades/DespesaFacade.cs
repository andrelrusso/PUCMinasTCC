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
    class DespesaFacade : IDespesaFacade
    {
        private readonly IDespesaRepository despesaRepository;
        private readonly IAppSettings appSettings;
        public DespesaFacade(IDespesaRepository despesaRepository, IAppSettings appSettings)
        {
            this.despesaRepository = despesaRepository;
            this.appSettings = appSettings;
        }
        public void Gerenciar(Despesa value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            despesaRepository.Gerenciar(value);
        }


        public async Task<Despesa> Get(int id) => await despesaRepository.Get(id);

        //public async Task<IList<UsuarioEmpresa>> ListarEmpresas(UsuarioEmpresa filtro) => await usuarioRepository.ListarEmpresas(filtro);

        //public async Task<IList<PerfilUsuario>> ListarPerfis(PerfilUsuario filtro) => await usuarioRepository.ListarPerfis(filtro);

        public async Task<IList<Despesa>> ToListAsync(Despesa filtro) => await despesaRepository.ToListAsync(filtro);
    }
}
