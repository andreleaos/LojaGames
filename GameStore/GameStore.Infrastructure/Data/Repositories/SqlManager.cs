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
                    sql = "EXECUTE SP_SEL_LISTAR_PRODUTO";
                    break;

                case TSqlQuery.PESQUISAR_PRODUTO:
                    sql = "EXECUTE SP_SEL_PESQUISAR_PRODUTO @id";
                    break;

                case TSqlQuery.CADASTRAR_PRODUTO:
                    sql = "EXECUTE SP_INS_CADASTRAR_PRODUTO @descricao, @precoUnitario, @CategoriaId, @ImagemId";
                    break;

                case TSqlQuery.ATUALIZAR_PRODUTO:
                    sql = "EXECUTE SP_UPD_ATUALIZAR_PRODUTO @descricao, @precoUnitario, @id";
                    break;

                case TSqlQuery.EXCLUIR_PRODUTO:
                    sql = "EXECUTE SP_DEL_EXCLUIR_PRODUTO @id";
                    break;

                case TSqlQuery.EXCLUIR_IMAGEM:
                    sql = "EXECUTE SP_DEL_EXCLUIR_IMAGEM @id";
                    break;

                case TSqlQuery.CADASTRAR_IMAGEM:
                    sql = "EXECUTE SP_INS_CADASTRAR_IMAGEM @Url_Blob_Storage";
                    break;

                case TSqlQuery.ATUALIZAR_IMAGEM:
                    sql = "EXECUTE SP_UPD_ATUALIZAR_IMAGEM @UrlBlobStorage, @id";
                    break;

                case TSqlQuery.PESQUISAR_IMAGEM_PELA_URL:
                    sql = "EXECUTE SP_SEL_PESQUISAR_IMAGEM_PELA_URL @Url_Blob_Storage";
                    break;
                case TSqlQuery.SEED_PRODUTO:

                    break;
            }

            return sql;
        }

    }
}
