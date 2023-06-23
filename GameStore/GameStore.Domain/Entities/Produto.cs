using GameStore.Domain.Dtos;
using GameStore.Domain.Enums;
using GameStore.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Produto
    {
        #region Atributos

        private int Id;
        private string Descricao;
        private double PrecoUnitario;
        private CategoriaProduto Categoria;
        private ImagemProduto ImagemProduto;
        private int ImagemId;
        private string Url;
        private string UrlBlobStorage;

        #endregion

        #region Propriedades
        public int CategoriaId { get { return Categoria.GetHashCode(); } }

        #endregion

        #region Construtores

        public Produto(int id, string descricao, double precoUnitario, CategoriaProduto categoria, ImagemProduto imagem, string urlBlobStorage)
            : this(descricao, precoUnitario, categoria, imagem, urlBlobStorage)
        {
            Id = id;
        }

        public Produto(int id, string descricao, double precoUnitario, CategoriaProduto categoria, ImagemProduto imagem)
            : this(descricao, precoUnitario, categoria, imagem)
        {
            Id = id;
        }

        public Produto(int id, string descricao, double precoUnitario, string categoria, ImagemProduto imagem)
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
        public Produto(string descricao, double precoUnitario, CategoriaProduto categoria, ImagemProduto imagem, string urlBlobStorage)
        {
            this.Descricao = descricao;
            this.PrecoUnitario = precoUnitario;
            this.Categoria = categoria;
            this.ImagemProduto = imagem;
            this.UrlBlobStorage = urlBlobStorage;
        }

        public Produto(string descricao, double precoUnitario, string categoria, ImagemProduto imagem)
        {
            this.Descricao = descricao;
            this.PrecoUnitario = precoUnitario;
            this.Categoria = ConversorEnums.ConvertToEnum(categoria);
            this.ImagemProduto = imagem;
        }
        public Produto() { }

        #endregion

        #region Getters e Setters
        public int GetId() { return this.Id; }
        public void SetId(int id) { this.Id = id; }
        public string GetDescricao() { return this.Descricao; } 
        public void SetDescricao(string descricao) { this.Descricao = descricao; }
        public double GetPrecoUnitario() { return this.PrecoUnitario; }
        public void SetPrecoUnitario(double precoUnitario) { this.PrecoUnitario = precoUnitario; }
        public CategoriaProduto GetCategoria() { return Categoria; }
        public void SetCategoria(CategoriaProduto categoria) { this.Categoria = categoria; }
        public ImagemProduto GetImagemProduto() { return ImagemProduto; }
        public void SetImagemProduto(ImagemProduto imagemProduto) { this.ImagemProduto = imagemProduto; }
        public void SetImagemProdutoId(int imagemId) { this.ImagemProduto.SetId(imagemId); }
        public int GetImagemId() { return this.ImagemId; }
        public void SetImagemId(int imagemId) { this.ImagemId = imagemId; }
        public string GetUrl() { return this.Url; }
        public void SetUrl(string url) { this.Url = url; }
        public string GetUrlBlobStorage() { return this.UrlBlobStorage; }
        public void SetUrlBlobStorage(string urlBlobStorage) { this.UrlBlobStorage = urlBlobStorage; }

        #endregion

        #region Métodos da classe
        public void Validate()
        {
            if (string.IsNullOrEmpty(Descricao))
                throw new Exception("Descrição é um campo obrigatório");

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
                return ImagemProduto.GetUrl();
            }
        }
        public string Url_Blob_Storage
        {
            get
            {
                return ImagemProduto.GetUrlBlobStorage();
            }
        }

        public Produto CreateProductByObjectCopy()
        {
            var result = new Produto(this.Id, this.Descricao, this.PrecoUnitario, this.Categoria, this.ImagemProduto, this.UrlBlobStorage);
            return result;
        }

        #endregion

        #region Conversores de Entidade - DTO
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
                UrlImagem = this.ImagemProduto.GetUrl(),
                UrlBlobStorage = this.GetUrlBlobStorage(),
                Database64Content = this.ImagemProduto.GetDatabase64Content()
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
                CategoriaProduto = produto.Categoria,
                UrlBlobStorage = produto.UrlBlobStorage
            };

            if (produto.ImagemProduto != null)
            {
                result.ImagemProduto = produto.ImagemProduto;
                result.UrlImagem = produto.ImagemProduto.GetUrl();
                result.UrlBlobStorage = produto.GetUrlBlobStorage();
                result.Database64Content = produto.ImagemProduto.GetDatabase64Content();

                if (string.IsNullOrEmpty(result.UrlImagem) && !string.IsNullOrEmpty(result.UrlBlobStorage))
                    result.UrlImagem = result.UrlBlobStorage;

                if (string.IsNullOrEmpty(result.UrlBlobStorage) && !string.IsNullOrEmpty(result.UrlImagem))
                    result.UrlBlobStorage = result.UrlImagem;
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

        #endregion
    }
}
