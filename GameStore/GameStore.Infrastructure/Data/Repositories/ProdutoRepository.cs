using Dapper;
using GameStore.Domain.ConfigApp;
using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using GameStore.Infrastructure.Config;
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
        private readonly IConfigParameters _configParameters;
        private readonly IConfiguration _configuration;

        private static string _connectionString = string.Empty;
        public ProdutoRepository(IConfiguration configuration, IConfigParameters configParameters)
        {
            _configuration = configuration; 
            _configParameters = configParameters;
            SetConnectionConfig();
        }

        public void Create(Produto produto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = SqlManager.GetSql(TSqlQuery.CADASTRAR_IMAGEM);
                        var imagemId = connection.ExecuteScalar<int>(sql, produto, transaction);

                        produto.SetImagemProdutoId(imagemId);
                        produto.SetImagemId(imagemId);

                        sql = SqlManager.GetSql(TSqlQuery.CADASTRAR_PRODUTO);
                        connection.Execute(
                            sql,
                            new
                            {
                                Descricao = produto.GetDescricao(),
                                PrecoUnitario = produto.GetPrecoUnitario(),
                                CategoriaId = produto.CategoriaId,
                                ImagemId = produto.GetImagemId()
                            },
                            transaction
                        );

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Erro ao realizar o cadastro. {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public bool Delete(int id)
        {
            if (id > 0)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = SqlManager.GetSql(TSqlQuery.PESQUISAR_PRODUTO);
                    var produtoPesquisado = connection.QueryFirstOrDefault<Produto>(sql, new { Id = id });

                    if (produtoPesquisado != null)
                    {
                        sql = SqlManager.GetSql(TSqlQuery.EXCLUIR_PRODUTO);
                        connection.Execute(sql, new { Id = produtoPesquisado.GetId() });

                        sql = SqlManager.GetSql(TSqlQuery.EXCLUIR_IMAGEM);
                        connection.Execute(sql, new { Id = produtoPesquisado.GetImagemId() });
                        return true;
                    }
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
            var produtoPesquisado = GetById(produto.GetId());

            if (produtoPesquisado != null)
            {
                produto.SetImagemId(produtoPesquisado.GetImagemId());
                produto.SetImagemProdutoId(produtoPesquisado.GetImagemId());

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var sql = SqlManager.GetSql(TSqlQuery.ATUALIZAR_PRODUTO);
                            connection.Execute(
                                sql, 
                                new { Descricao = produto.GetDescricao(), PrecoUnitario = produto.GetPrecoUnitario(), Categoria = produto.CategoriaId }, 
                                transaction
                            );

                            sql = SqlManager.GetSql(TSqlQuery.ATUALIZAR_IMAGEM);
                            connection.Execute(
                                sql, 
                                new { Url = produto.Url_path, UrlBlobStorage = produto.Url_Blob_Storage, id = produto.GetImagemId() },
                                transaction
                            );

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Erro ao realizar a atualizacao. {ex.Message}");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
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
            var result = produto.CreateProductByObjectCopy();

            if (produto.GetImagemProduto() != null)
            {
                result.SetImagemProduto(produto.GetImagemProduto().CreateImageByObjectCopy());
                result.GetImagemProduto().CleanStreamData();
            }

            return result;
        }
        private void CompleteData(Produto produto)
        {
            if (produto != null)
                produto.SetImagemProduto(new ImagemProduto(produto.GetImagemId(), produto.GetUrl()));
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

        private void SetConnectionConfig()
        {
            _configParameters.SetGeneralConfig();
            if (GeneralConfigApp.ENABLE_CONNECTION_LOCAL_DB)
                _connectionString = _configuration.GetConnectionString("lojaGamesDB_local");
            else
                _connectionString = _configuration.GetConnectionString("lojaGamesDB");
        }


    }
}
