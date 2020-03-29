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
using System.Net.Http;

namespace PUCMinasTCC.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioFacade usuarioFacade;
        public UsuarioController(
                                IUsuarioFacade usuarioFacade,
                                IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory) : base(httpContextAccessor, clientFactory)

        {
            this.usuarioFacade = usuarioFacade;
        }
        public async Task<IActionResult> Index()
        {
            var model = new UsuarioModel();
            model.Status = CodeUtil.PopulaComboComEnum(model.Filtro.Status);
            model.Perfis = CodeUtil.PopulaComboComEnum(model.Filtro.PerfilUsuario);
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
            model.Perfis = CodeUtil.PopulaComboComEnum(model.Filtro.PerfilUsuario, enumPerfilUsuario.Todos);

            if (id.HasValue)
            {
                model.Detalhe = await usuarioFacade.Get(id.Value);
                if (model.Detalhe == null) return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Detalhes(UsuarioModel model)
        {
            model.Status = CodeUtil.PopulaComboComEnum(model.Detalhe.Status, enumStatus.Todos);
            model.Perfis = CodeUtil.PopulaComboComEnum(model.Filtro.PerfilUsuario, enumPerfilUsuario.Todos);
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
    }
}