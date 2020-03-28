using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PUCMinasTCC.Api.Filter;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Facade.Interfaces;

namespace PUCMinasTCC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HandleJsonException]
    //[Authorize]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesaFacade _despesaFacade;
        public DespesaController(IDespesaFacade despesaFacade)
        {
            _despesaFacade = despesaFacade;
        }
        [HttpGet]
        public Task<Despesa> Get(int id) => _despesaFacade. Get(id);

        [HttpPost(nameof(Buscar))]
        public Task<IList<Despesa>> Buscar([FromBody]Despesa filtro) => _despesaFacade.ToListAsync(filtro);

        [Authorize]
        [HttpPost(nameof(Gerenciar))]
        public void Gerenciar([FromBody]Despesa value) => _despesaFacade.Gerenciar(value);

    }
}