using AutoMapper;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using GameStore.Service.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GameStore.Web.Controllers
{
    public class ProdutoController : Controller
    {
        #region Atributos

        private readonly IProdutoClientService _client;
        private readonly IMapper _mapper;

        #endregion

        #region Construtor
        public ProdutoController(IProdutoClientService client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }
        #endregion

        #region Listagem de Produtos

        public async Task<IActionResult> Index()
        {
            return await ListarProdutos();
        }
        public async Task<IActionResult> IndexLista()
        {
            return await ListarProdutos();
        }

        #endregion

        #region Consulta Por Id - Detalhe do Produto

        public async Task<IActionResult> Details(int id)
        {
            ProdutoDto? produto = await _client.GetById(id);
            return View(produto);
        }

        #endregion

        #region Cadastro de Produtos
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoDto produtoDto)
        {
            produtoDto.ConfigurarPrecoProduto();
            await _client.Create(produtoDto);
            return RedirectToAction("IndexLista");
        }

        #endregion

        #region Exclusão de Produto

        public async Task<IActionResult> Delete(int id)
        {
            ProdutoDto? produto = await _client.GetById(id);
            return View(produto);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([Bind("Id, Descricao, PrecoUnitario, Categoria, UrlImagem")] ProdutoDto produto)
        {
            await _client.Delete(produto.Id);
            return RedirectToAction("IndexLista");
        }

        #endregion

        #region Atualização de Produto

        public async Task<IActionResult> Edit(int id)
        {
            ProdutoDto? produtoDto = await _client.GetById(id);
            //produto.ConfigurarPrecoProduto();
            produtoDto.PrecoUnitarioStr = produtoDto.PrecoUnitario.ToString("N2");
            return View(produtoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoDto produtoDto)
        {
            produtoDto.ConfigurarPrecoProduto();
            await _client.Update(produtoDto);
            return RedirectToAction("IndexLista");
        }

        #endregion

        #region Métodos Auxiliares
        private async Task<IActionResult> ListarProdutos()
        {
            List<ProdutoDto> produtos = await _client.GetAll();
            return View(produtos);
        }
        #endregion
    }
}
