﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PUCMinasTCC;
using PUCMinasTCC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUCMinasTCC.Controllers
{
    [HandleJsonException]
    [Authorize]
    public class BaseController : Controller
    {
        protected const int PAGE_SIZE = 5;
        protected const string DESCRIPTION_ALL = "Todos";

        public void ShowErrorMessage(string message) => ShowErrorMessage(new Exception(message));
        public void ShowErrorMessage(Exception ex)
        {
            SharedValues.Session = SharedValues.Session ?? HttpContext.Session;
            SharedValues.ErrorMessage = ex.Message;
        }

        public void ShowSuccessMessage(string message)
        {
            SharedValues.Session = SharedValues.Session ?? HttpContext.Session;
            SharedValues.SuccessMessage = message;
        }
    }
}
