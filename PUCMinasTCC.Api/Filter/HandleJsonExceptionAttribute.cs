using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using PUCMinasTCC.Util.Util;
using System;
using System.Net;

namespace PUCMinasTCC.Api.Filter
{
    public class HandleJsonExceptionAttribute : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Exception == null) return;
            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            var error = new
            {
                context.Exception.Message,
                //context.Exception.StackTrace
            };
            response.WriteAsync(JsonConvert.SerializeObject(error));
        }

        

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //if (!context.HttpContext.Request.Headers.ContainsKey("token"))
            //{
            //    throw new UnauthorizedAccessException("Não autorizado!! É necessário fornecer um token válido");
            //}
            //var token = context.HttpContext.Request.Headers["token"];

            //var tokenConfigs = (TokenConfigurations) context.HttpContext.RequestServices.GetService(typeof(TokenConfigurations));
            //var siginConfigs = (SigningConfigurations)context.HttpContext.RequestServices.GetService(typeof(SigningConfigurations));

            //TokenHelper.ValidateToken(token, tokenConfigs, siginConfigs);
        }

        public void OnException(ExceptionContext context)
        {
            var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax && context.Exception != null)
            {
                context.ExceptionHandled = true;
                HttpResponse response = context.HttpContext.Response;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ContentType = "application/json";
                var error = new
                {
                    context.Exception.Message,
                   // context.Exception.StackTrace
                };
                response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        }
    }
}
