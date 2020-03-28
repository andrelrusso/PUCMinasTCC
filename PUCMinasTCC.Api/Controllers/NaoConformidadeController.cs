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
    [Authorize]
    public class NaoConformidadeController : ControllerBase
    {

        private readonly INaoConformidadeFacade _ncFacade;
        public NaoConformidadeController(INaoConformidadeFacade ncFacade)
        {
            _ncFacade = ncFacade;
        }
        [HttpGet]
        public Task<NaoConformidade> Get(int id) => _ncFacade.Get(id);

        [HttpPost(nameof(Buscar))]
        public Task<IList<NaoConformidade>> Buscar([FromBody]NaoConformidade filtro) => _ncFacade.ToListAsync(filtro);

        [HttpPost(nameof(Gerenciar))]
        public void Gerenciar([FromBody]NaoConformidade value) => _ncFacade.Gerenciar(value);
    }
}