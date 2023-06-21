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

            SetSeedConfiguration(app);

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

                    momentoCriacao = "tabela categoria.";
                    app.Logger.LogInformation("criando a " + momentoCriacao);
                    var seedImplemented = produtoRepository.SeedAsync("categoria");

                    momentoCriacao = "tabela imagemProduto.";
                    app.Logger.LogInformation("criando a " + momentoCriacao);
                    seedImplemented = produtoRepository.SeedAsync("imagemProduto");

                    momentoCriacao = "tabela produto.";
                    app.Logger.LogInformation("criando a " + momentoCriacao);
                    seedImplemented = produtoRepository.SeedAsync("produto");
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "Ocorreu um erro para criar a " + momentoCriacao);
                }
            }
        }
    }
}