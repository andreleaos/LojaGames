using AutoMapper;
using GameStore.Domain.Dtos;
using GameStore.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _repository;


        public CategoriaService(IMapper mapper, IProdutoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(CategoriaDto entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoriaDto> GetAll()
        {
            var categorias = _mapper.Map<List<CategoriaDto>>(_repository.GetAllCategoria().Result);
            return categorias;
        }

        public CategoriaDto? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CategoriaDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
