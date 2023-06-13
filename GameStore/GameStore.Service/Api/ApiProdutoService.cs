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

namespace GameStore.Service.Api
{
    public class ApiProdutoService : IApiProdutoService
    {
        private HttpClient _httpClient = null;
        private string _url_base_address = string.Empty;
        private IMapper _mapper;
        public ApiProdutoService(IConfiguration configuration, IMapper mapper)
        {
            _url_base_address = configuration.GetSection("API_PRODUTOS").Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url_base_address);
            _mapper = mapper;
        }

        public async Task Create(ProdutoFormDto produtoFormDto)
        {
            ProdutoDto produto = RealizarMapperParaProdutoDto(produtoFormDto);

            produto.AtualizarImagemProdutoBase64();

            var produtoDto = CompletedProductData(produto);

            string endpoint = $"{_url_base_address}";
            HttpContent content = FormatContentData(produtoDto);
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
        public async Task<List<ProdutoDto>> GetAll()
        {
            string endpoint = $"{_url_base_address}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<ProdutoDto>>(content);
            return result;
        }
        public async Task<ProdutoDto?> GetById(int id)
        {
            string endpoint = $"{_url_base_address}/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ProdutoDto>(content);
            return result;
        }
        public async Task Update(ProdutoFormDto produtoFormDto)
        {
            ProdutoDto produto = RealizarMapperParaProdutoDto(produtoFormDto);

            produto.AtualizarImagemProdutoBase64();

            var produtoDto = CompletedProductData(produto);

            string endpoint = $"{_url_base_address}";
            HttpContent content = FormatContentData(produtoDto);
            HttpResponseMessage response = await _httpClient.PutAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("Atualizacao efetuada com sucesso!");
            else
            {
                string errormsg = $"StatusCode: {response.StatusCode.ToString()}, Message: {response.RequestMessage?.Content?.ToString()}";
                throw new Exception($"Falha ao realizar o cadastro. {errormsg}");
            }
        }

        private ProdutoDto CompletedProductData(ProdutoDto produtoDto)
        {
            Produto produto = produtoDto.ConvertToEntity();
            ProdutoDto result = produtoDto;
            result.ImagemProduto = produto.ImagemProduto;
            result.CategoriaProduto = produto.Categoria;
            result.UrlBlobStorage = produto.Url_path;
            result.ImagemProduto.UrlBlobStorage = produto.Url_path;
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

        public ProdutoDto RealizarMapperParaProdutoDto(ProdutoFormDto produtoForm)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Arquivos/Recebidos");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            FileInfo fileInfo = new FileInfo(produtoForm.Arquivo.FileName);
            string fileName = produtoForm.Arquivo.FileName;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                produtoForm.Arquivo.CopyTo(stream);
            }

            produtoForm.UrlImagem = fileNameWithPath;

            ProdutoDto produtoDto = _mapper.Map<ProdutoDto>(produtoForm);
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
    }
}
