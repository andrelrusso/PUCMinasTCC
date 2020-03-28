using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PUCMinasTCC.IoC;
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
                    CryptoUtils.WriteKey("TGS|SICCA");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler/criar a chave", ex);
            }

            services.AddSingleton(new SigningConfigurations());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var tokenConfigurations = Configuration.GetSection(nameof(TokenConfigurations));
            services.Configure<TokenConfigurations>(tokenConfigurations).AddSingleton(tokenConfigurations.Get<TokenConfigurations>());
            try
            {
                Bootstrapper.Initialize(services, Configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inicializar o Bootstrapper", ex);
            }


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login";
                    //options.LogoutPath = "/Login/Logout";
                    options.ExpireTimeSpan = new TimeSpan(0,10, 0);
                });
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            services.AddHttpClient();

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
