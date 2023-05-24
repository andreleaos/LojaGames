using GameStore.Domain.Dtos;
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

        public async Task<IActionResult> Index()
        {
            List<ProdutoDto> produtos = await _apiProdutoService.GetAll();
            return View(produtos);
        }

        public async Task<IActionResult> Details(int id)
        {
            ProdutoDto? produto = await _apiProdutoService.GetById(id);
            return View(produto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ProdutoDto? produto = await _apiProdutoService.GetById(id);
            return View(produto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProdutoDto? produto = await _apiProdutoService.GetById(id);
            return View(produto);
        }


    }
}
