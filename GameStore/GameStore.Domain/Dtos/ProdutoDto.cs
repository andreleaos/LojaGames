using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Dtos
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        public CategoriaProduto Categoria { get; set; }
        public ImagemProduto ImagemProduto { get; set; }

        public Produto ConvertToEntity()
        {
            return new Produto
            {
                Id = this.Id,
                Descricao = this.Descricao,
                PrecoUnitario = this.PrecoUnitario,
                Categoria = this.Categoria,
                ImagemProduto = this.ImagemProduto
            };
        }
        public static Produto ConvertToEntity(ProdutoDto produto)
        {
            return new Produto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                PrecoUnitario = produto.PrecoUnitario,
                Categoria = produto.Categoria,
                ImagemProduto = produto.ImagemProduto
            };
        }
        public static List<Produto> ConvertToEntity(List<ProdutoDto> produtosDto)
        {
            var produtos = new List<Produto>();

            foreach(ProdutoDto item in produtosDto)
            {
                produtos.Add(ConvertToEntity(item));
            }

            return produtos;
        }
    }
}
