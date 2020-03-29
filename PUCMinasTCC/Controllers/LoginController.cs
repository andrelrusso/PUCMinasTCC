using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PUCMinasTCC.Domain.Entity.AuthData;
using PUCMinasTCC.Facade.Interfaces;
using PUCMinasTCC.Models;
using PUCMinasTCC.Util.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PUCMinasTCC.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IAuthFacade authService;
        private readonly IAppSettings settings;
        public LoginController(IAuthFacade authService,
                                IAppSettings settings, IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory) : base(httpContextAccessor, clientFactory)
        {
            this.authService = authService;
            this.settings = settings;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model,
                                                string returnUrl,
                                                [FromServices]TokenConfigurations tokenConfigurations,
                                                [FromServices]SigningConfigurations signingConfigurations)
        {
            if (!ModelState.IsValid)
                return View(model);

            ModelState.Clear();


            try
            {
                var data = new AuthDataFromPassPhrase();
                data.UserIdentity = model.Login;
                data.KeyContent = model.Senha;

                var response = await authService.LogIn(JsonConvert.SerializeObject(data), settings.KeyCrypto);
                if (response != null && response.Logged)
                {
                    response.Token = TokenHelper.GenerateJwtToken(response.User.IdUsuario, tokenConfigurations, signingConfigurations);
                    SharedValues.Session = SharedValues.Session ?? HttpContext.Session;
                    SharedValues.UsuarioLogado = response.User;
                    SharedValues.SuccessMessage = string.Empty;
                    SharedValues.ErrorMessage = string.Empty;
                  
                    //Defina pelo menos um conjunto de claims...
                    var claims = new List<Claim>
                    {
                        //Atributos do usuário ...
                        new Claim(ClaimTypes.Name, response.User.Login),
                        new Claim(ClaimTypes.Role, "Admin"),
                        //new Claim("Nome", response.User.Nome),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(2),
                        IsPersistent = true
                    };

                    //Loga de fato
                    var login = HttpContext.SignInAsync(
                          CookieAuthenticationDefaults.AuthenticationScheme,
                          new ClaimsPrincipal(identity), authProperties
                    );

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    throw new Exception(string.Join(" - ", response.Errors.Select(r => r.ToString())));
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }

            return View();

        }

        [HttpGet]
        public IActionResult AlterarSenha()
        {
            return View(new AlterarSenhaModel());
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (!ModelState.IsValid)
                return View(model);

            ModelState.Clear();

            try
            {
                var data = new AuthDataFromPassPhrase
                {
                    UserIdentity = model.Login,
                    KeyContent = model.Senha,
                    SystemCode = settings.IdSistema
                };

                await authService.ChangePassword(data, model.NovaSenha, model.ConfirmacaoSenha, settings.KeyCrypto);
                ShowSuccessMessage("Senha alterada com sucesso");
                return View();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            SharedValues.UsuarioLogado = null;
            return RedirectToAction(nameof(Login));
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : (IActionResult)RedirectToAction(nameof(HomeController.Index), "Home");
        }


    }
}