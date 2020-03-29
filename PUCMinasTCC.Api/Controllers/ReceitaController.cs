using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PUCMinasTCC.Api.Filter;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Facade.Interfaces;

namespace PUCMinasTCC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HandleJsonException]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaFacade _receitaFacade;
        public ReceitaController(IReceitaFacade receitaFacade)
        {
            _receitaFacade = receitaFacade;
        }
        [HttpGet]
        public Task<Receita> Get(int id) => _receitaFacade. Get(id);

        [HttpPost(nameof(Buscar))]
        public Task<IList<Receita>> Buscar([FromBody]Receita filtro) => _receitaFacade.ToListAsync(filtro);

        [Authorize]
        [HttpPost(nameof(Gerenciar))]
        public void Gerenciar([FromBody]Receita value) => _receitaFacade.Gerenciar(value);

    }
}