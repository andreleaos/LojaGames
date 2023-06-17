using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using GameStore.Service.Client;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoClientService _client;
        public ProdutoController(IProdutoClientService client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            List<ProdutoDto> produtos = await _client.GetAll();
            return View(produtos);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Descricao, Preco, Categoria, UrlImagem")] ProdutoDto produto)
        {
            produto.ConfigurarPrecoProduto();
            await _client.Create(produto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            ProdutoDto? produto = await _client.GetById(id);
            return View(produto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ProdutoDto? produto = await _client.GetById(id);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([Bind("Id, Descricao, PrecoUnitario, Categoria, UrlImagem")] ProdutoDto produto)
        {
            await _client.Delete(produto.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProdutoDto? produto = await _client.GetById(id);
            produto.ConfigurarPrecoProduto();
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id, Descricao, Preco, Categoria, UrlImagem")] ProdutoDto produto)
        {
            produto.ConfigurarPrecoProduto();
            await _client.Update(produto);
            return RedirectToAction("Index");
        }
    }
}
