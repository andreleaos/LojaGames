using AutoMapper;
using GameStore.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Client
{
    public class CategoriaClientService : ICategoriaClientService
    {
        private HttpClient _httpClient = null;
        private string _url_base_address = string.Empty;

        public CategoriaClientService(IConfiguration configuration)
        {
            _url_base_address = configuration.GetSection("API_CATEGORIAS").Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url_base_address);
        }

        public Task Create(CategoriaDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoriaDto>> GetAll()
        {
            string endpoint = $"{_url_base_address}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<CategoriaDto>>(content);

            return result;
        }

        public Task<CategoriaDto?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(CategoriaDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
