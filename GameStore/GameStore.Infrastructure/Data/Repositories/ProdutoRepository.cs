using Dapper;
using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Data.Repositories
{
    public class ProdutoRepository : SqlServerBaseRepository, IProdutoRepository
    {
        private static string _connectionString = string.Empty;
        public ProdutoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("lojaGamesDB");
        }

        public void Create(Produto produto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sql = SqlManager.GetSql(TSqlQuery.CADASTRAR_IMAGEM);
                connection.Execute(sql, produto);

                sql = SqlManager.GetSql(TSqlQuery.PESQUISAR_IMAGEM_PELA_URL);
                var imagemPesquisa = connection.QueryFirstOrDefault<ImagemProduto>(sql, produto);

                produto.ImagemProduto.Id = imagemPesquisa.Id;
                produto.ImagemId = imagemPesquisa.Id;
                sql = SqlManager.GetSql(TSqlQuery.CADASTRAR_PRODUTO);
                connection.Execute(sql, produto);
            }
        }

        public bool Delete(int id)
        {
            if (id > 0)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = SqlManager.GetSql(TSqlQuery.EXCLUIR_PRODUTO);
                    connection.Execute(sql, new { Id = id });

                    sql = SqlManager.GetSql(TSqlQuery.EXCLUIR_IMAGEM);
                    connection.Execute(sql, new { Id = id });
                    return true;
                }
            }

            return false;
        }

        public List<Produto> GetAll()
        {
            List<Produto> result = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = SqlManager.GetSql(TSqlQuery.LISTAR_PRODUTO);
                result = connection.Query<Produto>(sql).ToList();
            }

            result = CleanerImageContentData(result);
            return result;
        }
        public Produto? GetById(int id)
        {
            Produto? produto = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = SqlManager.GetSql(TSqlQuery.PESQUISAR_PRODUTO);
                produto = connection.QueryFirstOrDefault<Produto>(sql, new { Id = id });
            }

            CompleteData(produto);
            return produto;
        }

        public void Update(Produto produto)
        {
            var produtoPesquisado = GetById(produto.Id);

            if (produtoPesquisado != null)
            {
                produto.ImagemId = produtoPesquisado.ImagemId;
                produto.ImagemProduto.Id = produtoPesquisado.ImagemId;

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = SqlManager.GetSql(TSqlQuery.ATUALIZAR_PRODUTO);
                    connection.Execute(sql, produto);

                    sql = SqlManager.GetSql(TSqlQuery.ATUALIZAR_IMAGEM);
                    connection.Execute(sql, produto.ImagemProduto);
                }
            }
        }

        private List<Produto> CleanerImageContentData(List<Produto> produtos)
        {
            var result = new List<Produto>();

            foreach (var produto in produtos)
            {
                var item = CleanerImageContentData(produto);
                result.Add(item);
            }

            return result;
        }
        private Produto CleanerImageContentData(Produto produto)
        {
            var result = new Produto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                Categoria = produto.Categoria,
                PrecoUnitario = produto.PrecoUnitario,
                UrlBlobStorage = produto.UrlBlobStorage
            };

            if (produto.ImagemProduto != null)
            {
                result.ImagemProduto = new ImagemProduto
                {
                    Id = produto.ImagemProduto.Id,
                    Url = produto.ImagemProduto.Url,
                    DataStream = produto.ImagemProduto.DataStream,
                    Database64Content = produto.ImagemProduto.Database64Content
                };

                result.ImagemProduto.DataStream = null;
                result.ImagemProduto.Database64Content = null;
            }

            return result;
        }

        private void CompleteData(Produto produto)
        {
            if (produto != null)
                produto.ImagemProduto = new ImagemProduto(produto.ImagemId, produto.Url);
        }

        public async Task<bool> SeedAsync(string nomeTabela, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string nomeArquivoSql = string.Empty;
                    string pasta = "DDL";

                    switch (nomeTabela)
                    {
                        case "categoria":
                            nomeArquivoSql = "Create Table - 01 - categoria";
                            break;

                        case "imagemProduto":
                            nomeArquivoSql = "Create Table - 02 - imagemProduto";
                            break;

                        case "produto":
                            nomeArquivoSql = "Create Table - 03 - produto";
                            break;

                        default:
                            break;
                    }


                    var query = await Task.Run(() => ReadResource(pasta, nomeArquivoSql));
                    var result = await connection.ExecuteAsync(query);

                    int resultInsert = 0;
                    if (nomeTabela.Equals("categoria"))
                    {
                        pasta = "DML";
                        query = await Task.Run(() => ReadResource(pasta, "Insert categoria"));
                        resultInsert = await connection.ExecuteAsync(query);
                        return result > 0 && resultInsert > 0;
                    }

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                //logger.LogError(ex.Message);
                await SeedAsync(nomeTabela, retryForAvailability);
                throw;
            }
        }


    }
}
