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
    public class IncidenteController : BaseController
    {
        private readonly IIncidenteFacade incidenteFacade;
        private IList<NaoConformidade> naoConformidades = null;
        public IncidenteController(IIncidenteFacade incidenteFacade, INaoConformidadeFacade naoConformidadeFacade, IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory) : 
            base(httpContextAccessor, clientFactory)
        {
            this.incidenteFacade = incidenteFacade;
            naoConformidades = naoConformidadeFacade.ToListAsync(null).Result;
        }

        public async Task<IActionResult> Index()
        {
            var model = new IncidenteModel
            {
                Itens = await incidenteFacade.ToListAsync(null).ToPagedListAsync(PAGE_SIZE, 1)
            };
            model.Estado = CodeUtil.PopulaComboComEnum(model.Detalhe.EstadoIncidente);
            model.NaoConformidades = naoConformidades.AddAllToList(nameof(NaoConformidade.Descricao));
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
        public async Task<IActionResult> Pesquisar(int i, string d, int? idps, int s, int? p, int pz = 100)
        {
            var itens = await incidenteFacade.ToListAsync(new Incidente
            {
                IdIncidente = i,
                Descricao = d,
                EstadoIncidente = (enumEstadoIncidente)s,
                NaoConformidade = idps != null ? new NaoConformidade{ IdNaoConformidade = (int) idps }: null
            }).ToPagedListAsync(pz, p ?? 1);

            return PartialView("_ListaItens", itens);
        }

       

        public async Task<IActionResult> Detalhes(int? id)
        {
            ViewBag.propertydisable = id > 0 ? false : true;
            var model = new IncidenteModel();
            model.Estado = CodeUtil.PopulaComboComEnum(model.Detalhe.EstadoIncidente, enumEstadoIncidente.Todos);
            model.NaoConformidades = naoConformidades.AddAllToList(nameof(NaoConformidade.Descricao));
            if (id.HasValue)
            {
                model.Detalhe = await incidenteFacade.Get(id.Value);
                if (model.Detalhe == null) return NotFound();
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Detalhes(IncidenteModel model)
        {
            model.Estado = CodeUtil.PopulaComboComEnum(model.Detalhe.EstadoIncidente, enumEstadoIncidente.Todos);
            model.NaoConformidades = naoConformidades.AddAllToList(nameof(NaoConformidade.Descricao));
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.Detalhe.IdUsuarioOperacao = SharedValues.UsuarioLogado.IdUsuario;
                incidenteFacade.Gerenciar(model.Detalhe);
                ShowSuccessMessage("Registros processado com sucesso");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
                return View(model);
            }

            return RedirectToAction(nameof(Detalhes), new { id = model.Detalhe.IdIncidente });
        }
    }
}