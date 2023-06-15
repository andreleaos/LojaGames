using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Api
{
    public class ApiProdutoService : IApiProdutoService
    {
        private HttpClient _httpClient = null;

        private string _url_base_address = string.Empty;
        public ApiProdutoService(IConfiguration configuration)
        {
            _url_base_address = configuration["Endpoints:API_PRODUTOS"];
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url_base_address);
        }

        public async Task Create(ProdutoDto produto)
        {
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
        public async Task Update(ProdutoDto produto)
        {
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
            result.ImagemProduto = produto.GetImagemProduto();
            result.CategoriaProduto = produto.GetCategoria();
            result.UrlBlobStorage = produto.Url_path;
            result.ImagemProduto.SetUrlBlobStorage(produto.Url_path);
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
    }
}
