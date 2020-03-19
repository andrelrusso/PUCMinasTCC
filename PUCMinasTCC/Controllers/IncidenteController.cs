using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PUCMinasTCC.Models;
using PUCMinasTCC.Utils;

namespace PUCMinasTCC.Controllers
{
    public class IncidenteController : BaseController
    {/*
        public async Task<IActionResult> Index()
        {
            var model = new IncidenteModel();
            // model.Empresas = empresas.AddAllToList(nameof(Empresa.NomeFantasia));
            model.Status = CodeUtil.PopulaComboComEnum(model.Filtro.Status);
            model.Itens = await usuarioService.ToListAsync(null).ToPagedListAsync(PAGE_SIZE, 1);
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
            var itens = await usuarioService.ToListAsync(new Usuario
            {
                IdUsuario = i,
                Nome = d,
                Status = (enumStatus)s
            }, idps != 0 ? idps : null).ToPagedListAsync(pz, p ?? 1);

            return PartialView("_ListaItens", itens);
        }*/
    }
}