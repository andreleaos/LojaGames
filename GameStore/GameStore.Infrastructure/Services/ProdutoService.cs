using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public void Create(Produto produto)
        {
            _repository.Create(produto);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public List<Produto> GetAll()
        {
            return _repository.GetAll();
        }

        public Produto? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Produto entity)
        {
            _repository.Update(entity);
        }
    }
}
