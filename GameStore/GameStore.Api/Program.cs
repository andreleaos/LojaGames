using GameStore.Infrastructure.Data.Repositories;
using GameStore.IoC.Configuration;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            GameStoreRegisterDependencies.Configure(builder.Services, builder.Configuration);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            SetCreateDataBaseLocal(app);

            SetCreateProceduresIniciais(app);

            SetSeedConfiguration(app);
            
            SetCreateProcedures(app);

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
 
        private static void SetSeedConfiguration(WebApplication? app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;
                string momentoCriacao = string.Empty;
                try
                {
                    var produtoRepository = scopedProvider.GetRequiredService<IProdutoRepository>();
                    produtoRepository.CreateTablesAndSeed();
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "Ocorreu um erro para criar a " + momentoCriacao);
                }
            }
        }

        private static void SetCreateProceduresIniciais(WebApplication? app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;

                try
                {
                    var produtoRepository = scopedProvider.GetRequiredService<IProdutoRepository>();
                    produtoRepository.CreateProceduresIniciais();
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "Ocorreu um erro para criar as Procedures Iniciais.");
                }
            }
        }
        private static void SetCreateProcedures(WebApplication? app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;

                try
                {
                    var produtoRepository = scopedProvider.GetRequiredService<IProdutoRepository>();
                    produtoRepository.CreateProcedures();
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "Ocorreu um erro para criar as Procedures.");
                }
            }
        }
        private static void SetCreateDataBaseLocal(WebApplication? app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;

                try
                {
                    var produtoRepository = scopedProvider.GetRequiredService<IProdutoRepository>();
                    produtoRepository.CreateDataBaseLocal();
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "Ocorreu um erro para criar o banco de dados.");
                }
            }
        }
    }
}