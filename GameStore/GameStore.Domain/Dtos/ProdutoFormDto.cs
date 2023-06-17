using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Dtos
{
    public class ProdutoFormDto
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        
        private decimal precoUnitario;
        public decimal PrecoUnitario { get; set; }

        private string precoUnitarioStr;

        [Display(Name = "Preço Unitário")]
        public string PrecoUnitarioStr
        {
            get { return precoUnitarioStr; }
            set 
            { 
                precoUnitarioStr = value;
                Decimal.TryParse(precoUnitarioStr, out precoUnitario);
                PrecoUnitario = precoUnitario;
            }
        }


        [Display(Name = "Categoria")]
        public string Categoria { get; set; }
        public CategoriaProduto CategoriaProduto { get; set; }

        [Display(Name = "URL Blog Storage")]
        public string UrlImagem { get; set; }

        public string UrlBlobStorage { get; set; }
        public ImagemProduto ImagemProduto { get; set; }


        [Display(Name = "Arquivo")]
        public IFormFile? Arquivo { get; set; }

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
