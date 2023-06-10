using GameStore.Domain.ConfigApp;
using GameStore.Infrastructure.Config;
using GameStore.Infrastructure.Data.Repositories;
using GameStore.Service.Api;
using GameStore.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.IoC.Configuration
{
    public static class GameStoreRegisterDependencies
    {
        public static void Configure(IServiceCollection services)
        {
            AddInfrastructureDependencies(services);
            AddServicesDependencies(services);
        }

        private static void AddInfrastructureDependencies(IServiceCollection services)
        {
            services.AddScoped<IConfigParameters, ConfigParameters>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }

        private static void AddServicesDependencies(IServiceCollection services)
        {
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IApiProdutoService, ApiProdutoService>();
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

    }
}
