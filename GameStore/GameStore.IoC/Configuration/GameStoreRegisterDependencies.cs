using GameStore.Infrastructure.Data.Repositories;
using GameStore.Infrastructure.Services;
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
            AddRepositoriesDependencies(services);
            AddServicesDependencies(services);
        }

        private static void AddRepositoriesDependencies(IServiceCollection services)
        {
            //TODO: Ajustar o repo para scoped quando incluir o BD
            services.AddSingleton<IProdutoRepository, ProdutoRepository>();
        }

        private static void AddServicesDependencies(IServiceCollection services)
        {
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IApiProdutoService, ApiProdutoService>();
        }
    }
}
