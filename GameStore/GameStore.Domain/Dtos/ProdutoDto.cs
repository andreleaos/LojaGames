using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using GameStore.Domain.Utils;
using Microsoft.AspNetCore.Http;
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
        public string Categoria { get; set; }
        public CategoriaProduto CategoriaProduto { get; set; }
        public string UrlImagem { get; set; }
        public string UrlBlobStorage { get; set; }
        public ImagemProduto ImagemProduto { get; set; }
        public string PrecoCurrencyFormat
        {
            get
            {
                string precoFormat = Math.Round(PrecoUnitario, 2).ToString();
                return $"R$ {precoFormat}";
            }
        }
        public ImagemProduto AtualizarImagemProdutoBase64()
        {
            return ImagemProduto = new ImagemProduto(this.UrlImagem);
        }
        public string Database64Content { get; set; }

        public Produto ConvertToEntity()
        {
            CategoriaProduto = ConversorEnums.ConvertToEnum(this.Categoria);

            return new Produto(this.Id, this.Descricao, this.PrecoUnitario, CategoriaProduto, this.ImagemProduto);
        }

        //public void ConfigurarPrecoProduto()
        //{
        //    if (!string.IsNullOrEmpty(this.Preco))
        //    {
        //        this.PrecoUnitario = Double.Parse(this.Preco.Replace(".", ","));
        //    }

        //    if(this.PrecoUnitario > 0 && string.IsNullOrEmpty(this.Preco))
        //    {
        //        this.Preco = this.PrecoUnitario.ToString();
        //    }
        //}

        public static Produto ConvertToEntity(ProdutoDto produto)
        {
            CategoriaProduto categoriaProduto = ConversorEnums.ConvertToEnum(produto.Categoria);
            ImagemProduto imagemProduto = new ImagemProduto(produto.UrlImagem);

            //                 UrlBlobStorage = produto.UrlBlobStorage

            return new Produto(produto.Id, produto.Descricao, produto.PrecoUnitario, categoriaProduto, imagemProduto);
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
