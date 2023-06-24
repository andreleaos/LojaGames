using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Dtos
{
    public class ProdutoViewDto
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo descrição é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo Nome deve ter no máximo 150 caracteres.")]
        public string Descricao { get; set; }

        public double PrecoUnitario { get; set; }

        [Display(Name = "Preço Unitário")]
        [Required(ErrorMessage = "Campo Preco Unitario é obrigatório")]
        public string PrecoUnitarioStr { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Campo Categoria é obrigatório")]
        public string Categoria { get; set; }
        public CategoriaProduto CategoriaProduto { get; set; }

        [Display(Name = "URL Blog Storage")]
        [Required(ErrorMessage = "Campo URL é obrigatório")]
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
                string precoFormat = PrecoUnitario.ToString("C2");
                return $"{precoFormat}";
            }
        }

        public void MapperCategoriaEnumToCategoria()
        {
            if (CategoriaProduto != null)
                Categoria = CategoriaProduto.ToString();
        }

        public void MapperCategoriaToCategoriaEnum()
        {
            if (!string.IsNullOrEmpty(Categoria))
                CategoriaProduto = (CategoriaProduto)Enum.Parse(typeof(CategoriaProduto), Categoria);
        }

    }
}
