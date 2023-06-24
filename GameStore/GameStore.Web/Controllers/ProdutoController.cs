using AutoMapper;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using GameStore.Service.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NuGet.Protocol.Core.Types;

namespace GameStore.Web.Controllers
{
    public class ProdutoController : Controller
    {
        #region Atributos

        private readonly IProdutoClientService _client;
        private readonly ICategoriaClientService _clientCategoria;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private static bool _local_execution = false;

        #endregion

        #region Construtor
        public ProdutoController(IProdutoClientService client, IMapper mapper, ICategoriaClientService clientCategoria, IConfiguration configuration)
        {
            _client = client;
            _mapper = mapper;
            _clientCategoria = clientCategoria;
            _configuration = configuration;
            _local_execution = bool.Parse(_configuration.GetSection("EnableLocalExecution").Value);
        }
        #endregion

        #region Listagem de Produtos

        public async Task<IActionResult> Index()
        {
            ViewBag.LocalExecution = _local_execution;
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
            ProdutoViewDto? produto = await _client.GetById(id);
            ViewBag.LocalExecution = _local_execution;
            return View(produto);
        }

        #endregion

        #region Cadastro de Produtos
        public async Task<IActionResult> Create()
        {
            var categorias =  await _clientCategoria.GetAll();
            ViewBag.Categorias = categorias;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoViewDto produtoViewDto)
        {
            await _client.Create(produtoViewDto);
            return RedirectToAction("IndexLista");
        }

        #endregion

        #region Exclusão de Produto

        public async Task<IActionResult> Delete(int id)
        {
            ProdutoViewDto? produto = await _client.GetById(id);
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
            ProdutoViewDto? produtoViewDto = await _client.GetById(id);
            produtoViewDto.MapperCategoriaToCategoriaEnum();

            produtoViewDto.PrecoUnitarioStr = produtoViewDto.PrecoUnitario.ToString("N2");

            var categorias = await _clientCategoria.GetAll();
            ViewBag.Categorias = categorias;
            return View(produtoViewDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoViewDto produtoViewDto)
        {
            await _client.Update(produtoViewDto);
            return RedirectToAction("IndexLista");
        }

        #endregion

        #region Métodos Auxiliares
        private async Task<IActionResult> ListarProdutos()
        {
            List<ProdutoViewDto> produtos = await _client.GetAll();
            return View(produtos);
        }
        #endregion
    }
}
