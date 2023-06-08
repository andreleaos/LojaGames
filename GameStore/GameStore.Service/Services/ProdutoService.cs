using Azure.Storage.Blobs;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameStore.Service.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        private readonly IConfiguration _configuration;

        private static string urlContainerBlobStorage = string.Empty;
        private static string containerBlobStorage = "images";
        public ProdutoService(IProdutoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            urlContainerBlobStorage = _configuration.GetConnectionString("ContainerBlob");
        }

        public void Create(ProdutoDto produtoDto)
        {
            Produto produto = produtoDto.ConvertToEntity();
            produto.UrlBlobStorage = UploadBase64ImageBlobStorage(produto.ImagemProduto.Database64Content, containerBlobStorage, produtoDto.UrlImagem);
            produto.ImagemProduto.UrlBlobStorage = produto.UrlBlobStorage;
            _repository.Create(produto);
        }

        public bool Delete(int id)
        {
            var produto = _repository.GetById(id);
            bool deletedImageBlobStorage = false;
            if (produto != null)
            {
                var uri = new Uri(produto.ImagemProduto.Url);
                deletedImageBlobStorage = DeleteImageBlobStorage(uri, containerBlobStorage);
            }

            return _repository.Delete(id);
        }

        public List<ProdutoDto> GetAll()
        {
            var produtos = _repository.GetAll();
            var produtosDto = Produto.ConvertToDto(produtos);
            return produtosDto;
        }

        public ProdutoDto? GetById(int id)
        {
            var produto = _repository.GetById(id);
            ProdutoDto produtoDto = Produto.ConvertToDto(produto);
            return produtoDto;
        }

        public void Update(ProdutoDto produtoDto)
        {
            Produto produto = produtoDto.ConvertToEntity();

            var produtoAtual = _repository.GetById(produto.Id);
            if (produtoAtual != null && produtoAtual.ImagemProduto.Url != produtoDto.UrlImagem && produto.ImagemProduto.Database64Content != null)
            {
                produto.UrlBlobStorage = UploadBase64ImageBlobStorage(produto.ImagemProduto.Database64Content, containerBlobStorage, produtoDto.UrlImagem);
                produto.ImagemProduto.UrlBlobStorage = produto.UrlBlobStorage;
            }
            else
            {
                produto.UrlBlobStorage = produtoDto.UrlImagem;
                produto.ImagemProduto.UrlBlobStorage = produto.UrlBlobStorage;
            }

            _repository.Update(produto);
        }

        public string UploadBase64ImageBlobStorage(string base64Image, string container, string urlImagem)
        {
            FileInfo fileInfo = new FileInfo(urlImagem);

            // Gera um nome randomico para imagem
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension; // ".jpg";

            // Limpa o hash enviado
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

            // Gera um array de Bytes
            byte[] imageBytes = Convert.FromBase64String(data);

            // Define o BLOB no qual a imagem será armazenada
            var blobClient = new BlobClient(urlContainerBlobStorage, container, fileName);

            // Envia a imagem
            using (var stream = new MemoryStream(imageBytes))
            {
                var uploadFile = blobClient.Upload(stream);
            }

            if(File.Exists(urlImagem))
                File.Delete(urlImagem);

            // Retorna a URL da imagem
            return blobClient.Uri.AbsoluteUri;
        }

        public bool DeleteImageBlobStorage(Uri urlBlobStorage, string container)
        {
            var fileName = urlBlobStorage.LocalPath.Replace("/images/", "");

            // Define o BLOB no qual a imagem está armazenada
            var blobClient = new BlobClient(urlContainerBlobStorage, container, fileName);

            var deleted = blobClient.DeleteIfExistsAsync().Result;

            return deleted;
        }
    }

}
