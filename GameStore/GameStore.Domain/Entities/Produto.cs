using GameStore.Domain.Dtos;
using GameStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        public CategoriaProduto Categoria { get; set; }
        public ImagemProduto ImagemProduto { get; set; }

        public ProdutoDto ConvertToDto()
        {
            return new ProdutoDto
            {
                Id = this.Id,
                Descricao = this.Descricao,
                PrecoUnitario = this.PrecoUnitario,
                Categoria = this.Categoria,
                ImagemProduto = this.ImagemProduto
            };
        }
        public static ProdutoDto ConvertToDto(Produto produto)
        {
            return new ProdutoDto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                PrecoUnitario = produto.PrecoUnitario,
                Categoria = produto.Categoria,
                ImagemProduto = produto.ImagemProduto
            };
        }
        public static List<ProdutoDto> ConvertToDto(List<Produto> produtos)
        {
            var produtosDto = new List<ProdutoDto>();

            foreach (Produto item in produtos)
            {
                produtosDto.Add(ConvertToDto(item));
            }

            return produtosDto;
        }

    }
}
