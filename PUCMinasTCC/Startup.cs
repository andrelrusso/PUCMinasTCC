using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PUCMinasTCC.Util.Util;
using PUCMinasTCC.Utils;

namespace PUCMinasTCC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            try
            {
                if (string.IsNullOrEmpty(CryptoUtils.ReadKey()))
                    CryptoUtils.WriteKey("PUCMINAS|TCC");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler/criar a chave", ex);
            }

            services.AddSingleton(new SigningConfigurations());

            //var tokenConfigurations = new TokenConfigurations();
            //new ConfigureFromConfigurationOptions<TokenConfigurations>(
            //    Configuration.GetSection(nameof(TokenConfigurations)))
            //        .Configure(tokenConfigurations);
            var tokenConfigurations = Configuration.GetSection(nameof(TokenConfigurations));
            services.Configure<TokenConfigurations>(tokenConfigurations).AddSingleton(tokenConfigurations.Get<TokenConfigurations>());
            //services.AddSingleton(tokenConfigurations) ;
            //try
            //{
            //    Bootstrapper.Initialize(services, Configuration);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Erro ao inicializar o Bootstrapper", ex);
            //}


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login";
                    options.LogoutPath = "/Login/Logout";
                    options.Cookie = new CookieBuilder()
                    {
                        Expiration = new TimeSpan(0, 2, 0)
                    };
                });         
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            //services.AddMvc()
            //      .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            //      .AddJsonOptions(options =>
            //      {
            //          options.SerializerSettings.ContractResolver
            //              = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //      });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
