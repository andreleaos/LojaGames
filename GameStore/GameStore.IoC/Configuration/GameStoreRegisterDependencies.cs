using GameStore.Domain.ConfigApp;
using GameStore.Infrastructure.Config;
using GameStore.Infrastructure.Data.Repositories;
using GameStore.Service.Client;
using GameStore.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

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
            SetCurrentCulture(services, configuration);
        }

        private static void SetCurrentCulture(IServiceCollection services, IConfiguration configuration)
        {
            string culture = configuration.GetSection("CurrentCulture").Value;

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(culture);
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo(culture) };
                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo(culture) };
            });
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
