using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using Newtonsoft.Json;
using PUCMinasTCC.Controllers;

namespace PUCMinasTCC.Filters
{
    public class HandleJsonExceptionAttribute : Attribute, IResultFilter, IExceptionFilter
    {
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
                    context.Exception.StackTrace
                };
                response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            //var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (context.Exception == null) return;
            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            var error = new
            {
                context.Exception.Message,
                context.Exception.StackTrace
            };
            response.WriteAsync(JsonConvert.SerializeObject(error));
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var response = context.HttpContext.Response;
            var request = context.HttpContext.Request;
            if (SharedValues.UsuarioLogado == null && !(context.Controller is LoginController) && !(context.Controller is DespesaController) && !(context.Controller is ReceitaController))
            {
                response.Redirect($"/Login/Login?returnUrl={request.Path.ToString()}");
                return;
            }

            if (SharedValues.UsuarioLogado?.PerfilUsuario != Domain.Enums.enumPerfilUsuario.Administrador && (request.Path.ToString().ToLower().Contains("detalhes")))
            {
                throw new Exception("Perfil de usuário não permitido para este acesso");
            }

        }
    }
}
