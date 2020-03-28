using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PUCMinasTCC.Api.Filter;
using PUCMinasTCC.Domain.Entity.AuthData;
using PUCMinasTCC.Facade.Interfaces;
using PUCMinasTCC.Util.Util;

namespace PUCMinasTCC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HandleJsonException]

    public class AuthController : ControllerBase
    {
        private readonly IAuthFacade _authFacade;
        public AuthController(IAuthFacade authFacade)
        {
            _authFacade = authFacade;
        }

        [HttpPost(nameof(LogIn))]
        public async Task<IAuthenticationResponse> LogIn([FromBody]object data,
                                                        [FromServices]IAppSettings appSettings,
                                                        [FromServices]TokenConfigurations tokenConfigurations,
                                                        [FromServices]SigningConfigurations signingConfigurations)
        {

            var response = await _authFacade.LogIn(data, appSettings.KeyCrypto);
            if (response != null && response.Logged)
            {
                response.Token = TokenHelper.GenerateJwtToken(response.User.IdUsuario, tokenConfigurations, signingConfigurations);

            }

            return response;
        }
    }
}