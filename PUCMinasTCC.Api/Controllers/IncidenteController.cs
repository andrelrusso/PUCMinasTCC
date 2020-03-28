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
    public class IncidenteController : ControllerBase
    {
        private readonly IIncidenteFacade _incidenteFacade;
        public IncidenteController(IIncidenteFacade incidenteFacade)
        {
            _incidenteFacade = incidenteFacade;
        }
        [HttpGet]
        public Task<Incidente> Get(int id) => _incidenteFacade.Get(id);

        [HttpPost(nameof(Buscar))]
        public Task<IList<Incidente>> Buscar([FromBody]Incidente filtro) => _incidenteFacade.ToListAsync(filtro);

        [HttpPost(nameof(Gerenciar))]
        public void Gerenciar([FromBody]Incidente value) => _incidenteFacade.Gerenciar(value);
    }
}