using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PUCMinasTCC.Domain.Context;
using PUCMinasTCC.Domain.Repository;
using PUCMinasTCC.Facade.Facades;
using PUCMinasTCC.Facade.Interfaces;
using PUCMinasTCC.Repository.Repositories;

namespace PUCMinasTCC.IoC
{
    public class Bootstrapper
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbContext>(new DataContext(configuration.GetConnectionString("Database")));
            //services.AddTransient<IEmpresaService, EmpresaService>();
            //services.AddTransient<ISistemaService, SistemaService>();
            //services.AddTransient<IPerfilService, PerfilService>();
            //services.AddTransient<IAcessoService, AcessoService>();
            services.AddTransient<IUsuarioFacade, UsuarioFacade>();
            services.AddTransient<IAuthFacade, AuthFacade>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            //services.AddTransient<IAcessoRepository, AcessoRepository>();
            //services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            //services.AddTransient<ISistemaRepository, SistemaRepository>();
            //services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddSingleton<IAppSettings, AppSettings>();
        }
    }
}
