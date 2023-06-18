using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using GameStore.Domain.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Dtos
{
    public class ProdutoDto
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public double PrecoUnitario { get; set; }

        [Display(Name = "Preço Unitário")]
        public string PrecoUnitarioStr { get; set; }

        [Display(Name = "Categoria")]
        public string Categoria { get; set; }
        public CategoriaProduto CategoriaProduto { get; set; }
        [Display(Name = "URL Blog Storage")]
        public string UrlImagem { get; set; }
        public string UrlBlobStorage { get; set; }
        public ImagemProduto ImagemProduto { get; set; }

        [Display(Name = "Arquivo")]
        public IFormFile? Arquivo { get; set; }

        public string Database64Content { get; set; }
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

        public void ConfigurarPrecoProduto()
        {
            if (!string.IsNullOrEmpty(this.PrecoUnitarioStr))
            {
                this.PrecoUnitario = Double.Parse(this.PrecoUnitarioStr.Replace(".", ","));
            }

            if (this.PrecoUnitario > 0 && string.IsNullOrEmpty(this.PrecoUnitarioStr))
            {
                this.PrecoUnitarioStr = this.PrecoUnitario.ToString();
            }
        }
        public Produto ConvertToEntity()
        {
            CategoriaProduto = ConversorEnums.ConvertToEnum(this.Categoria);
            ImagemProduto imagemProduto = new ImagemProduto(this.UrlImagem);

            var result = new Produto(this.Id, this.Descricao, this.PrecoUnitario, CategoriaProduto, this.ImagemProduto);
            result.SetImagemProduto(imagemProduto);

            result.SetUrl(imagemProduto.GetUrl());
            result.SetUrlBlobStorage(imagemProduto.GetUrlBlobStorage());

            return result;
        }
        public static Produto ConvertToEntity(ProdutoDto produto)
        {
            CategoriaProduto categoriaProduto = ConversorEnums.ConvertToEnum(produto.Categoria);
            ImagemProduto imagemProduto = new ImagemProduto(produto.UrlImagem);

            var result = new Produto(produto.Id, produto.Descricao, produto.PrecoUnitario, categoriaProduto, imagemProduto);
            result.SetImagemProduto(imagemProduto);

            result.SetUrl(imagemProduto.GetUrl());
            result.SetUrlBlobStorage(imagemProduto.GetUrlBlobStorage());

            return result;
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
