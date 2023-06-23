using AutoMapper;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Client
{
    public class ProdutoClientService : IProdutoClientService
    {
        #region Atributos

        private HttpClient _httpClient = null;
        private string _url_base_address = string.Empty;
        private readonly IMapper _mapper;
        private static bool _local_execution = false;
        private static string _local_path_file_images = "";

        #endregion

        #region Construtor

        public ProdutoClientService(IConfiguration configuration, IMapper mapper)
        {
            _url_base_address = configuration.GetSection("API_PRODUTOS").Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url_base_address);
            _mapper = mapper;
            _local_execution = Boolean.Parse(configuration.GetSection("EnableLocalExecution").Value);
            _local_path_file_images = configuration.GetSection("LocalPathImages").Value;
        }

        #endregion

        #region Métodos Públicos

        public async Task Create(ProdutoViewDto produtoViewDto)
        {
            produtoViewDto.MapperCategoriaEnumToCategoria();

            ProdutoDto produto = _mapper.Map<ProdutoDto>(RealizarMapperParaProdutoDto(produtoViewDto));
            produto.ConfigurarPrecoProduto();
            
            produto.AtualizarImagemProdutoBase64();
            var result = CompletedProductData(produto);

            string endpoint = $"{_url_base_address}";
            HttpContent content = FormatContentData(result);
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("Cadastro efetuado com sucesso!");
            else
            {
                string errormsg = $"StatusCode: {response.StatusCode.ToString()}, Message: {response.RequestMessage?.Content?.ToString()}";
                throw new Exception($"Falha ao realizar o cadastro. {errormsg}");
            }
        }
        public async Task<bool> Delete(int id)
        {
            string endpoint = $"{_url_base_address}/{id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(endpoint);

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<bool>(content);
            return result;
        }
        public async Task<List<ProdutoViewDto>> GetAll()
        {
            string endpoint = $"{_url_base_address}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<ProdutoViewDto>>(content);

            return result;
        }
        public async Task<ProdutoViewDto?> GetById(int id)
        {
            string endpoint = $"{_url_base_address}/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ProdutoViewDto>(content);
            return result;
        }
        public async Task Update(ProdutoViewDto produtoViewDto)
        {
            produtoViewDto.MapperCategoriaEnumToCategoria();

            ProdutoDto produto = _mapper.Map<ProdutoDto>(RealizarMapperParaProdutoDto(produtoViewDto));

            produto.ConfigurarPrecoProduto();

            produto.AtualizarImagemProdutoBase64();

            var result = CompletedProductData(produto);

            string endpoint = $"{_url_base_address}";
            HttpContent content = FormatContentData(result);
            HttpResponseMessage response = await _httpClient.PutAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("Atualizacao efetuada com sucesso!");
            else
            {
                string errormsg = $"StatusCode: {response.StatusCode.ToString()}, Message: {response.RequestMessage?.Content?.ToString()}";
                throw new Exception($"Falha ao realizar o cadastro. {errormsg}");
            }
        }

        #endregion

        #region Métodos Auxiliares

        private ProdutoDto CompletedProductData(ProdutoDto produtoDto)
        {
            Produto produto = produtoDto.ConvertToEntity();
            ProdutoDto result = produtoDto;
            result.ImagemProduto = produto.GetImagemProduto();
            result.CategoriaProduto = produto.GetCategoria();
            result.UrlBlobStorage = produto.Url_path;
            result.ImagemProduto.SetUrlBlobStorage(produto.Url_path);
            result.Database64Content = produto.GetImagemProduto().GetDatabase64Content();
            //result.Arquivo = null;
            return result;
        }
        private HttpContent FormatContentData(ProdutoDto produto)
        {
            if (produto == null)
                throw new Exception("Produto Informado Nulo, não é possível realizar a conversão de tipo de dados");

            var json = JsonConvert.SerializeObject(produto);
            byte[] data = Encoding.UTF8.GetBytes(json);
            ByteArrayContent byteContent = new ByteArrayContent(data);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpContent content = byteContent;
            return content;
        }
        private ProdutoViewDto RealizarMapperParaProdutoDto(ProdutoViewDto produtoDto)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "ArquivosRecebidos");

            if(produtoDto.Arquivo != null)
            {
                FileInfo fileInfo = new FileInfo(produtoDto.Arquivo.FileName);
                string fileName = produtoDto.Arquivo.FileName;

                string fileNameWithPath = string.Empty;

                if (_local_execution)
                    path = _local_path_file_images;

                fileNameWithPath = Path.Combine(path, fileName);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!File.Exists(fileNameWithPath))
                {
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        produtoDto.Arquivo.CopyTo(stream);
                    }
                }

                produtoDto.UrlImagem = fileNameWithPath;
            }

            return produtoDto;
        }

        public Task Create(ProdutoDto entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProdutoDto entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
