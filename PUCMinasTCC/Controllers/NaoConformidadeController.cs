using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.Facade.Interfaces;
using PUCMinasTCC.Models;
using PUCMinasTCC.Utils;

namespace PUCMinasTCC.Controllers
{
    public class NaoConformidadeController : BaseController
    {
        private readonly INaoConformidadeFacade naoConformidadeFacade;

        public NaoConformidadeController(INaoConformidadeFacade naoConformidadeFacade, IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory) : base(httpContextAccessor, clientFactory)
        {
            this.naoConformidadeFacade = naoConformidadeFacade;
        }

        public async Task<IActionResult> Index()
        {
            var model = new NaoConformidadeModel
            {
                Itens = await naoConformidadeFacade.ToListAsync(null).ToPagedListAsync(PAGE_SIZE, 1)
            };
            model.OrigemNc = CodeUtil.PopulaComboComEnum(model.Filtro.OrigemNc);
            model.Status = CodeUtil.PopulaComboComEnum(model.Filtro.Status);
            return View(model);
        }

        /// <summary>
        /// Filtra os Usuários
        /// </summary>
        /// <param name="i">Id</param>
        /// <param name="d">Descrição</param>
        /// <param name="s">Status</param>
        /// <param name="s">Origem</param>
        /// <param name="p">Pagina atual</param>
        /// <param name="pz">Tamanho da página</param>
        /// <returns>Partial com a lista dos registros</returns>
        public async Task<IActionResult> Pesquisar(int i, string d, int o, int s, int? p, int pz = 100)
        {
            var itens = await naoConformidadeFacade.ToListAsync(new NaoConformidade
            {
                IdNaoConformidade = i,
                Descricao = d,
                OrigemNc = (enumOrigemNC) o,
                Status = (enumStatus)s
            }).ToPagedListAsync(pz, p ?? 1);

            return PartialView("_ListaItens", itens);
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            var model = new NaoConformidadeModel();
            model.OrigemNc = CodeUtil.PopulaComboComEnum(model.Filtro.OrigemNc);
            model.Status = CodeUtil.PopulaComboComEnum(model.Detalhe.Status, enumStatus.Todos);
            if (id.HasValue)
            {
                model.Detalhe = await naoConformidadeFacade.Get(id.Value);
                if (model.Detalhe == null) return NotFound();
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Detalhes(NaoConformidadeModel model)
        {
            model.OrigemNc = CodeUtil.PopulaComboComEnum(model.Filtro.OrigemNc);
            model.Status = CodeUtil.PopulaComboComEnum(model.Detalhe.Status, enumStatus.Todos);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.Detalhe.IdUsuarioOperacao = SharedValues.UsuarioLogado.IdUsuario;
                naoConformidadeFacade.Gerenciar(model.Detalhe);
                ShowSuccessMessage("Registros processado com sucesso");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
                return View(model);
            }

            return RedirectToAction(nameof(Detalhes), new { id = model.Detalhe.IdNaoConformidade });
        }
    }
}