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
            services.AddTransient<IUsuarioFacade, UsuarioFacade>();
            services.AddTransient<IAuthFacade, AuthFacade>();
            services.AddTransient<IIncidenteFacade, IncidenteFacade>();
            services.AddTransient<IDespesaFacade, DespesaFacade>();
            services.AddTransient<IReceitaFacade, ReceitaFacade>();
            services.AddTransient<INaoConformidadeFacade, NaoConformidadeFacade>();

            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IIncidenteRepository, IncidenteRepository>();
            services.AddTransient<IDespesaRepository, DespesaRepository>();
            services.AddTransient<IReceitaRepository, ReceitaRepository>();
            services.AddTransient<INaoConformidadeRepository, NaoConformidadeRepository>();
            services.AddSingleton<IAppSettings, AppSettings>();
        }
    }
}
