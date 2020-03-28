using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PUCMinasTCC.Util.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUCMinasTCC.Api.Filter
{
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("token"))
            {
                throw new UnauthorizedAccessException("Não autorizado!! É necessário fornecer um token válido");
            }
            var token = context.HttpContext.Request.Headers["token"];

            var tokenConfigs = (TokenConfigurations)context.HttpContext.RequestServices.GetService(typeof(TokenConfigurations));
            var siginConfigs = (SigningConfigurations)context.HttpContext.RequestServices.GetService(typeof(SigningConfigurations));

            var isAuthorized = TokenHelper.ValidateToken(token, tokenConfigs, siginConfigs);

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

    }
}
