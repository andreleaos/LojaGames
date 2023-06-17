using AutoMapper;
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
        private IMapper _mapper;

        public ProdutoController(IProdutoClientService client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
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
        public async Task<IActionResult> Create(ProdutoFormDto produtoForm)
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
            produto.ConfigurarPrecoProduto();1
            ProdutoFormDto produto = _mapper.Map<ProdutoFormDto>(produtoDto);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoFormDto produtoForm)
        {
            produtoForm.ConfigurarPrecoProduto();
            await _client.Update(produtoForm);
            return RedirectToAction("Index");
        }
    }
}
