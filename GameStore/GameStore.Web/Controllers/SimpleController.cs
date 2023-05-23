using GameStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GameStore.Web.Controllers
{
    public class SimpleController : Controller
    {
        private HttpClient _httpClient = null;
        private string _url_base_address = string.Empty;

        public SimpleController(IConfiguration configuration)
        {
            _url_base_address = configuration.GetSection("Endpoints").GetValue<string>("Api");
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                string endpointApi = $"{_url_base_address}api/simple";

                _httpClient.BaseAddress = new Uri(_url_base_address);
                HttpResponseMessage response = await _httpClient.GetAsync(endpointApi);

                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Produto>(content);
                return View(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
