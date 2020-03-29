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
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;

namespace PUCMinasTCC.Controllers
{
    public class ReceitaController : BaseController
    {
        private readonly IReceitaFacade receitaFacade;
        public ReceitaController(IReceitaFacade receitaFacade, IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory) : base(httpContextAccessor, clientFactory)
        {
            this.receitaFacade = receitaFacade;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = new ReceitaModel();
            model.Itens = await receitaFacade.ToListAsync(null).ToPagedListAsync(PAGE_SIZE, 1);
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
        [AllowAnonymous]
        public async Task<IActionResult> Pesquisar(int i, string m, string c, int? p, int pz = 5)
        {
            var itens = await receitaFacade.ToListAsync(new Receita
            {
                IdReceita = i,
                Ano = m,
                CNPJ = c != null ? Convert.ToInt64(c.Replace("/","").Replace(".","").Replace("-","")):0
            }).ToPagedListAsync(pz, p ?? 1);

            return PartialView("_ListaItens", itens);
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            var model = new ReceitaModel();
            if (id.HasValue)
            {
                model.Detalhe = await receitaFacade.Get(id.Value);
                if (model.Detalhe == null) return NotFound();

            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Detalhes(ReceitaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                 model.Detalhe.IdUsuarioOperacao = SharedValues.UsuarioLogado.IdUsuario;
                receitaFacade.Gerenciar(model.Detalhe);
                ShowSuccessMessage("Registros processado com sucesso");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
                return View(model);
            }

            return RedirectToAction(nameof(Detalhes), new { id = model.Detalhe.IdReceita });
        }

      

    }
}