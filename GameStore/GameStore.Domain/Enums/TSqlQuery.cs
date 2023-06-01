using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Enums
{
    public enum TSqlQuery
    {
        LISTAR_PRODUTO,
        PESQUISAR_PRODUTO,
        CADASTRAR_PRODUTO,
        ATUALIZAR_PRODUTO,
        EXCLUIR_PRODUTO,

        CADASTRAR_IMAGEM,
        PESQUISAR_IMAGEM_PELA_URL
    }
}
