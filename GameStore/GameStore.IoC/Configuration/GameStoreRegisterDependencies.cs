using GameStore.Domain.ConfigApp;
using GameStore.Infrastructure.Config;
using GameStore.Infrastructure.Data.Repositories;
using GameStore.Service.Client;
using GameStore.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.IoC.Configuration
{
    public static class GameStoreRegisterDependencies
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            AddInfrastructureDependencies(services);
            AddServicesDependencies(services);
            AddAutoMapper(services);
            ConfigureDatabase(configuration);
        }

        private static void AddInfrastructureDependencies(IServiceCollection services)
        {
            services.AddScoped<IConfigParameters, ConfigParameters>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }

        private static void AddServicesDependencies(IServiceCollection services)
        {
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IProdutoClientService, ProdutoClientService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ICategoriaClientService, CategoriaClientService>();
        }

        private static void ConfigureDatabase(IConfiguration configuration)
        {
            var connectionParameter = configuration["EnableLocalExecution"];
            var enableConnectionLocal = Boolean.Parse(connectionParameter);
            EnableConnectionLocal(enableConnectionLocal);
        }

        public static void EnableConnectionLocal(bool enabled)
        {
            GeneralConfigApp.ENABLE_CONNECTION_LOCAL_DB = enabled;
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
