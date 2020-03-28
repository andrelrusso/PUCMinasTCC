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
    [Authorize]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioFacade _usuarioFacade;
        public UsuarioController(IUsuarioFacade usuarioFacade)
        {
            _usuarioFacade = usuarioFacade;
        }
        [HttpGet]
        public Task<Usuario> Get(int id) => _usuarioFacade.Get(id);

        [HttpPost(nameof(Buscar))]
        public Task<IList<Usuario>> Buscar([FromBody]Usuario filtro) => _usuarioFacade.ToListAsync(filtro);

        [HttpPost(nameof(Gerenciar))]
        public void Gerenciar([FromBody]Usuario value) => _usuarioFacade.Gerenciar(value);
    }
}