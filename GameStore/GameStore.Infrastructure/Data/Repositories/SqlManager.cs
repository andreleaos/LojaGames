using GameStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Data.Repositories
{
    public static class SqlManager
    {
        public static string GetSql(TSqlQuery sqlQuery)
        {
            string sql = string.Empty;

            switch (sqlQuery)
            {
                case TSqlQuery.LISTAR_PRODUTO:
                    sql = "select p.id, p.descricao, precoUnitario, categoriaId, c.descricao 'categoria', imagemId 'ImagemId', url_path 'Url' " +
                          "from produto p inner join imagemProduto ip on p.imagemId = ip.id inner join categoria c on p.categoriaId = c.id";
                    break;

                case TSqlQuery.PESQUISAR_PRODUTO:
                    sql = "select p.id, p.descricao, precoUnitario, categoriaId, c.descricao 'categoria', imagemId 'ImagemId', url_path 'Url' " +
                          "from produto p inner join imagemProduto ip on p.imagemId = ip.id inner join categoria c on p.categoriaId = c.id where p.id = @id";
                    break;

                case TSqlQuery.CADASTRAR_PRODUTO:
                    sql = "insert into produto (descricao, precoUnitario, categoriaId, imagemId) values (@descricao, @precoUnitario, @CategoriaId, @ImagemId)";
                    break;

                case TSqlQuery.ATUALIZAR_PRODUTO:
                    sql = "update produto set descricao = @descricao, precoUnitario = @precoUnitario where id = @id";
                    break;

                case TSqlQuery.EXCLUIR_PRODUTO:
                    sql = "delete from produto where id = @id";
                    break;

                case TSqlQuery.CADASTRAR_IMAGEM:
                    sql = "insert into imagemProduto (url_path) values (@Url_path)";
                    break;

                case TSqlQuery.ATUALIZAR_IMAGEM:
                    sql = "update imagemProduto set url_path = @Url where id = @id";
                    break;

                case TSqlQuery.PESQUISAR_IMAGEM_PELA_URL:
                    sql = "select id 'Id', url_path 'Url' from imagemProduto where url_path = @Url_path";
                    break;
            }

            return sql;
        }
    }
}
