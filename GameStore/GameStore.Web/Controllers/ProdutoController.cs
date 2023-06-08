using AutoMapper;
using GameStore.Domain.Dtos;
using GameStore.Service.Api;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IApiProdutoService _apiProdutoService;
        private IMapper _mapper;
        public ProdutoController(IApiProdutoService apiProdutoService, IMapper mapper)
        {
            _apiProdutoService = apiProdutoService;
            _mapper = mapper;
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
        public async Task<IActionResult> Create(ProdutoFormDto produtoForm)
        {
            await _apiProdutoService.Create(produtoForm);
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
            ProdutoDto? produtoDto = await _apiProdutoService.GetById(id);
            ProdutoFormDto produto = _mapper.Map<ProdutoFormDto>(produtoDto);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoFormDto produtoForm)
        {
            await _apiProdutoService.Update(produtoForm);
            return RedirectToAction("Index");
        }
    }
}
