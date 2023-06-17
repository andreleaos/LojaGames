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
        public static void Configure(IServiceCollection services)
        {
            AddInfrastructureDependencies(services);
            AddServicesDependencies(services);
            AddAutoMapper(services);
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
        }

        private static void ConfigureDatabase(IConfiguration configuration)
        {
            var connectionParameter = configuration["FeatureFlags:enable_connection_local_db"];
            var enableConnectionLocal = Boolean.Parse(connectionParameter);
            GeneralConfigApp.ENABLE_CONNECTION_LOCAL_DB = enableConnectionLocal;
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
