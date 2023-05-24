﻿using GameStore.Api.Controllers.Base;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Base;
using GameStore.Infrastructure.Services;
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
        public void Create([Bind("Descricao, PrecoUnitario, Categoria, UrlImagem")] ProdutoDto produto)
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
            return _produtoService.GetById(id);
        }

        [HttpPut]
        public void Update([Bind("Descricao, PrecoUnitario, Categoria, UrlImagem")] ProdutoDto produto)
        {
            _produtoService.Update(produto);
        }
    }
}
