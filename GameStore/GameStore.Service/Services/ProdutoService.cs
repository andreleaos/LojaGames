using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public void Create(ProdutoDto produtoDto)
        {
            Produto produto = produtoDto.ConvertToEntity();
            _repository.Create(produto);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public List<ProdutoDto> GetAll()
        {
            var produtos = _repository.GetAll();
            var produtosDto = Produto.ConvertToDto(produtos);
            return produtosDto;
        }

        public ProdutoDto? GetById(int id)
        {
            var produto = _repository.GetById(id);
            ProdutoDto produtoDto = Produto.ConvertToDto(produto);
            return produtoDto;
        }

        public void Update(ProdutoDto produtoDto)
        {
            Produto produto = produtoDto.ConvertToEntity();
            _repository.Update(produto);
        }
    }

}
