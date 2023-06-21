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
        #region Atributos

        private readonly IProdutoRepository _repository;
        private readonly IConfiguration _configuration;

        private static string conectionStorageAccount = string.Empty;
        private static string containerBlobStorage = string.Empty;
        private static bool _local_execution = false;

        #endregion

        #region Construtor

        public ProdutoService(IProdutoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;

            containerBlobStorage = _configuration.GetSection("ContainerBlobStorage").Value;
            conectionStorageAccount = _configuration.GetSection("ConnectionStorageAccount").Value;
            _local_execution = Boolean.Parse(_configuration.GetSection("EnableLocalExecution").Value);
        }

        #endregion

        #region Métodos Públicos

        public void Create(ProdutoDto produtoDto)
        {
            Produto produto = produtoDto.ConvertToEntity();
            var imagemProduto = produto.GetImagemProduto();

            if (!_local_execution)
            {
                produto.SetUrlBlobStorage(UploadBase64ImageBlobStorage(imagemProduto.GetDatabase64Content(), containerBlobStorage, produtoDto.UrlImagem));
                produto.GetImagemProduto().SetUrlBlobStorage(produto.GetUrlBlobStorage());
            }

            produto.Validate();
            _repository.Create(produto);
        }

        public bool Delete(int id)
        {
            var produto = _repository.GetById(id);
            bool deletedImageBlobStorage = false;
            if (produto != null)
            {
                var uri = new Uri(produto.GetImagemProduto().GetUrl());
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

            var produtoAtual = _repository.GetById(produto.GetId());
            if (produtoAtual != null && produtoAtual.GetImagemProduto().GetUrl() != produtoDto.UrlImagem && produto.GetImagemProduto().GetDatabase64Content != null)
            {
                produto.SetUrlBlobStorage(UploadBase64ImageBlobStorage(produto.GetImagemProduto().GetDatabase64Content(), containerBlobStorage, produtoDto.UrlImagem));
                produto.GetImagemProduto().SetUrlBlobStorage(produto.GetUrlBlobStorage());
            }
            else
            {
                produto.SetUrlBlobStorage(produtoDto.UrlImagem);
                produto.GetImagemProduto().SetUrlBlobStorage(produto.GetUrlBlobStorage());
            }

            produto.Validate();
            _repository.Update(produto);
        }

        #endregion

        #region Métodos Auxiliares

        private string UploadBase64ImageBlobStorage(string base64Image, string container, string urlImagem)
        {
            FileInfo fileInfo = new FileInfo(urlImagem);

            // Gera um nome randomico para imagem
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;

            // Limpa o hash enviado
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

            // Gera um array de Bytes
            byte[] imageBytes = Convert.FromBase64String(data);

            // Define o BLOB no qual a imagem será armazenada
            var blobClient = new BlobClient(conectionStorageAccount, container, fileName);

            // Envia a imagem
            using (var stream = new MemoryStream(imageBytes))
            {
                var uploadFile = blobClient.Upload(stream);
            }

            if (File.Exists(urlImagem))
                File.Delete(urlImagem);

            // Retorna a URL da imagem
            return blobClient.Uri.AbsoluteUri;
        }
        private bool DeleteImageBlobStorage(Uri urlBlobStorage, string containerBlobStorage)
        {
            if (_local_execution)
                return true;

            var fileName = urlBlobStorage.LocalPath.Replace("/" + containerBlobStorage + "/", "");

            // Define o BLOB no qual a imagem está armazenada
            var blobClient = new BlobClient(conectionStorageAccount, containerBlobStorage, fileName);

            var deleted = blobClient.DeleteIfExistsAsync().Result;

            return deleted;
        }

        #endregion
    }

}
