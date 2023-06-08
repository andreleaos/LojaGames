using GameStore.Infrastructure.Data.Repositories;
using GameStore.Service.Api;
using GameStore.Service.Services;
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
            AddAutoMapper(services);
        }

        private static void AddInfrastructureDependencies(IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }

        private static void AddServicesDependencies(IServiceCollection services)
        {
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IApiProdutoService, ApiProdutoService>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
