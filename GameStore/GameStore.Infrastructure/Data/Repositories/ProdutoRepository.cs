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
    public class ProdutoRepository : IProdutoRepository
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

                sql = SqlManager.GetSql(TSqlQuery.PESAQUISAR_IMAGEM_PELA_URL);
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql = SqlManager.GetSql(TSqlQuery.ATUALIZAR_PRODUTO);
                connection.Execute(sql, produto);
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
                PrecoUnitario = produto.PrecoUnitario
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
            produto.ImagemProduto = new ImagemProduto(produto.ImagemId, produto.Url);
        } 
    }
}
