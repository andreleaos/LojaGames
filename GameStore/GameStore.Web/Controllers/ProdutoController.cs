using GameStore.Service.Api;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IApiProdutoService _apiProdutoService;
        public ProdutoController(IApiProdutoService apiProdutoService)
        {
            _apiProdutoService = apiProdutoService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
