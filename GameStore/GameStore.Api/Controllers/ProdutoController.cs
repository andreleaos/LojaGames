using GameStore.Api.Controllers.Base;
using GameStore.Domain.Contracts.Base;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase, IGenericRepoService<ProdutoDto, int>
    {
        private readonly IProdutoService _produtoService;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(IProdutoService produtoService, ILogger<ProdutoController> logger)
        {
            _produtoService = produtoService;
            _logger = logger;
        }

        [HttpPost]
        public void Create(ProdutoDto produto)
        {
            try
            {
                _logger.LogInformation("[API - Produto] Cadastro de Produto Iniciado");
                _produtoService.Create(produto);
                _logger.LogInformation("[API - Produto] Cadastro de Produto Concluido");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha na app: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            try
            {
                _logger.LogInformation("[API - Produto] Exclusao de Produto Iniciado");
                var result = _produtoService.Delete(id);
                _logger.LogInformation("[API - Produto] Exclusao de Produto Concluido");
                return result;       
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha na app: {ex.Message}");
                throw ex;
            }
        }

        [HttpGet]
        public List<ProdutoDto> GetAll()
        {
            try
            {
                _logger.LogInformation("[API - Produto] Listagem de Produto Iniciado");
                var result = _produtoService.GetAll();
                _logger.LogInformation("[API - Produto] Listagem de Produto Concluido");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha na app: {ex.Message}");
                throw ex;
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ProdutoDto? GetById(int id)
        {
            try
            {
                _logger.LogInformation("[API - Produto] Pesquisa de Produto Iniciado");
                var produto = _produtoService.GetById(id);
                _logger.LogInformation("[API - Produto] Pesquisa de Produto Concluido");
                return produto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha na app: {ex.Message}");
                throw ex;
            }
        }

        [HttpPut]
        public void Update(ProdutoDto produto)
        {
            try
            {
                _logger.LogInformation("[API - Produto] Atualizacao de Produto Iniciado");
                _produtoService.Update(produto);
                _logger.LogInformation("[API - Produto] Atualizacao de Produto Concluido");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha na app: {ex.Message}");
                throw ex;
            }
        }
    }
}
