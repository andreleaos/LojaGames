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

        public int ImagemId { get; set; }
        public string Url { get; set; }
        public string UrlBlobStorage { get; set; }
        public int CategoriaId { get { return Categoria.GetHashCode(); } }

        public Produto(int id, string descricao, double precoUnitario, CategoriaProduto categoria, ImagemProduto imagem)
            : this(descricao, precoUnitario, categoria, imagem)
        {
            Id = id;
        }

        public Produto(string descricao, double precoUnitario, CategoriaProduto categoria, ImagemProduto imagem)
        {
            this.Descricao = descricao;
            this.PrecoUnitario = precoUnitario;
            this.Categoria = categoria;
            this.ImagemProduto = imagem;
        }

        public Produto() { }

        public void SetId(int id)
        {
            this.Id = id;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Descricao))
                throw new Exception("Descrição é um campo obrigatório");

            if (PrecoUnitario <= 0)
                throw new Exception("Preço Unitário é um campo obrigatório e deve ser maior que zero");

            if (PrecoUnitario <= 0)
                throw new Exception("Preço Unitário é um campo obrigatório e deve ser maior que zero");

            if(CategoriaId  <= 0)
                throw new Exception("Categoria é um campo obrigatório e deve ser maior que zero");

            if (string.IsNullOrEmpty(Url_path) && string.IsNullOrEmpty(Url_Blob_Storage))
                throw new Exception("A url da imagem do produto é um campo obrigatório");
        }

        public string Url_path
        {
            get
            {
                return ImagemProduto.Url;
            }
        }

        public string Url_Blob_Storage
        {
            get
            {
                return ImagemProduto.UrlBlobStorage;
            }
        }


        public ProdutoDto ConvertToDto()
        {
            return new ProdutoDto
            {
                Id = this.Id,
                Descricao = this.Descricao,
                PrecoUnitario = this.PrecoUnitario,
                Categoria = this.Categoria.ToString(),
                CategoriaProduto = this.Categoria,
                ImagemProduto = this.ImagemProduto,
                UrlImagem = this.ImagemProduto.Url,
                UrlBlobStorage = this.UrlBlobStorage
            };
        }
        public static ProdutoDto ConvertToDto(Produto produto)
        {
            if (produto == null)
                return null;

            var result = new ProdutoDto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                PrecoUnitario = produto.PrecoUnitario,
                Categoria = produto.Categoria.ToString(),
                CategoriaProduto = produto.Categoria
            };

            if (produto.ImagemProduto != null)
            {
                result.ImagemProduto = produto.ImagemProduto;
                result.UrlImagem = produto.ImagemProduto.Url;
                result.UrlBlobStorage = produto.UrlBlobStorage;
            }

            return result;    
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
