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

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public void Create(ProdutoDto produto)
        {
            _produtoService.Create(produto);
        }

        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            return _produtoService.Delete(id);
        }

        [HttpGet]
        public List<ProdutoDto> GetAll()
        {
            return _produtoService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public ProdutoDto? GetById(int id)
        {
            var produto = _produtoService.GetById(id);
            return produto;
        }

        [HttpPut]
        public void Update(ProdutoDto produto)
        {
            _produtoService.Update(produto);
        }
    }
}
