using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PUCMinasTCC;
using PUCMinasTCC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PUCMinasTCC.Controllers
{
    [HandleJsonException]
    [Authorize]
    public class BaseController : Controller
    {
        protected const int PAGE_SIZE = 100;
        protected const string DESCRIPTION_ALL = "Todos";
        private readonly IHttpClientFactory _clientFactory;

        public BaseController(IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory)
        {
            SharedValues.Session = httpContextAccessor.HttpContext.Session;
            _clientFactory = clientFactory;
        }

        public void ShowErrorMessage(string message) => ShowErrorMessage(new Exception(message));
        public void ShowErrorMessage(Exception ex)
        {
            SharedValues.ErrorMessage = ex.Message;
        }

        public void ShowSuccessMessage(string message)
        {
            SharedValues.SuccessMessage = message;
        }
    }
}
