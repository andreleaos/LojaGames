using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Descricao, PrecoUnitario, Categoria, UrlImagem")] ProdutoDto produto)
        {
            await _apiProdutoService.Create(produto);
            return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<IActionResult> Delete([Bind("Id, Descricao, PrecoUnitario, Categoria, UrlImagem")] ProdutoDto produto)
        {
            await _apiProdutoService.Delete(produto.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProdutoDto? produto = await _apiProdutoService.GetById(id);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id, Descricao, PrecoUnitario, Categoria, UrlImagem")] ProdutoDto produto)
        {
            await _apiProdutoService.Update(produto);
            return RedirectToAction("Index");
        }
    }
}
