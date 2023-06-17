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
    public class ProdutoFormDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Preco { get; set; }
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

        public IFormFile Arquivo { get; set; }

        public void ConfigurarPrecoProduto()
        {
            if (!string.IsNullOrEmpty(this.Preco))
            {
                this.PrecoUnitario = Double.Parse(this.Preco.Replace(".", ","));
            }

            if (this.PrecoUnitario > 0 && string.IsNullOrEmpty(this.Preco))
            {
                this.Preco = this.PrecoUnitario.ToString();
            }
        }
    }
}
