using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PUCMinasTCC.Facade.Interfaces;
using PUCMinasTCC.Models;
using PUCMinasTCC.Utils;
using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace PUCMinasTCC.Controllers
{
    public class UsuarioController : BaseController
    {
        //private readonly IEmpresaService empresaService;
        private readonly IUsuarioFacade usuarioFacade;
        private readonly IAppSettings appSettings;
        //private IList<Empresa> empresas = null;
        public UsuarioController(//IEmpresaService empresaService,
                                IUsuarioFacade usuarioFacade,
                                IAppSettings appSettings, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
       
        {
            //this.empresaService = empresaService;
            this.usuarioFacade = usuarioFacade;
            this.appSettings = appSettings;
            //empresas = empresaService.ToListAsync(null).Result;
        }
        public async Task<IActionResult> Index()
        {
            var model = new UsuarioModel();
           // model.Empresas = empresas.AddAllToList(nameof(Empresa.NomeFantasia));
            model.Status = CodeUtil.PopulaComboComEnum(model.Filtro.Status);
            model.Itens = await usuarioFacade.ToListAsync(null).ToPagedListAsync(PAGE_SIZE, 1);
            return View(model);
        }

        /// <summary>
        /// Filtra os Usuários
        /// </summary>
        /// <param name="i">Id</param>
        /// <param name="d">Descrição</param>
        /// <param name="s">Status</param>
        /// <param name="p">Pagina atual</param>
        /// <param name="pz">Tamanho da página</param>
        /// <returns>Partial com a lista dos registros</returns>
        public async Task<IActionResult> Pesquisar(int i, string d, int? idps, int s, int? p, int pz = 5)
        {
            var itens = await usuarioFacade.ToListAsync(new Usuario
            {
                IdUsuario = i,
                Nome = d,
                Status = (enumStatus)s
            }, idps != 0 ? idps : null).ToPagedListAsync(pz, p ?? 1);

            return PartialView("_ListaItens", itens);
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            var model = new UsuarioModel();
            model.Status = CodeUtil.PopulaComboComEnum(model.Detalhe.Status, enumStatus.Todos);
           // model.Empresas = empresas.AddAllToList(nameof(Empresa.NomeFantasia));

            if (id.HasValue)
            {
                model.Detalhe = await usuarioFacade.Get(id.Value);
                if (model.Detalhe == null) return NotFound();

                //await BuscarVinculos(model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Detalhes(UsuarioModel model)
        {
            //model.Empresas = empresas;
            model.Status = CodeUtil.PopulaComboComEnum(model.Detalhe.Status, enumStatus.Todos);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.Detalhe.IdUsuarioOperacao = SharedValues.UsuarioLogado.IdUsuario;
                usuarioFacade.Gerenciar(model.Detalhe);
                ShowSuccessMessage("Registros processado com sucesso");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
                return View(model);
            }

            return RedirectToAction(nameof(Detalhes), new { id = model.Detalhe.IdUsuario });
        }

        //[HttpPost]
        //public async Task<IActionResult> VincularEmpresa(UsuarioEmpresa value)
        //{
        //    value.IdUsuarioOperacao = SharedValues.UsuarioLogado.IdUsuario;
        //    usuarioService.GerenciarVinculoEmpresa(value);
        //    value.Status = enumStatus.Ativo;

        //    var model = new UsuarioModel();
        //    model.Detalhe = value.Usuario;
        //    model.Empresas = empresas.AddAllToList(nameof(Empresa.NomeFantasia));
        //    await BuscarVinculos(model);
        //    return PartialView("_ListaEmpresas", model);
        //}

        //public async Task<IActionResult> ListaPerfis(int idPerfil, int idUsuario)
        //{
        //    var model = new UsuarioModel();
        //    model.Detalhe = await usuarioFacade.Get(idUsuario);
        //   // await BuscarVinculos(model);
        //    return PartialView("_ListaPerfis", model);
        //}

        //[HttpPost]
        //public IActionResult VincularPerfil(PerfilUsuario value)
        //{
        //    value.IdUsuarioOperacao = SharedValues.UsuarioLogado.IdUsuario;
        //    usuarioService.GerenciarVinculoPerfil(value);
        //    return RedirectToAction(nameof(ListaPerfis), new { idUsuario = value.UsuarioEmpresa.Usuario.IdUsuario });
        //}

        //private async Task BuscarVinculos(UsuarioModel model)
        //{
        //    model.Detalhe.Empresas = await usuarioService.ListarEmpresas(new UsuarioEmpresa
        //    {
        //        Usuario = model.Detalhe,
        //        Status = enumStatus.Ativo
        //    });

        //    model.Detalhe.Perfis = await usuarioService.ListarPerfis(new PerfilUsuario
        //    {
        //        UsuarioEmpresa = new UsuarioEmpresa { Usuario = model.Detalhe },
        //        Status = enumStatus.Ativo
        //    });

        //    if (model.Empresas != null && model.Detalhe.Empresas != null)
        //    {
        //        model.Empresas.RemoveAll(r => model.Detalhe.Empresas.Any(p => p.Empresa.IdEmpresa == r.IdEmpresa));
        //    }
        //}
    }
}