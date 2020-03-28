using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PUCMinasTCC.IoC;
using PUCMinasTCC.Util.Util;

namespace PUCMinasTCC.Api
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
            services.AddControllers();


            services.AddSingleton(new SigningConfigurations());
            var tokenConfigurations = Configuration.GetSection(nameof(TokenConfigurations));
            services.Configure<TokenConfigurations>(tokenConfigurations).AddSingleton(tokenConfigurations.Get<TokenConfigurations>());

            try
            {
                if (string.IsNullOrEmpty(CryptoUtils.ReadKey()))
                    CryptoUtils.WriteKey("TGS|SICCA");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler/criar a chave", ex);
            }

            try
            {
                Bootstrapper.Initialize(services, Configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inicializar o Bootstrapper", ex);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.MapControllerRoute(
                //   name: "defaultApi",
                //   pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseEndpoints(endpoints =>
            //{
               
            //});
        }
    }
}
