using AutoMapper;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.IoC.Profiles
{
    public class ProdutoDtoProfile : Profile
    {
        public ProdutoDtoProfile()
        {
            CreateMap<ProdutoViewDto, ProdutoDto>().ReverseMap();
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Catetoria, CategoriaDto>().ReverseMap();
        }
    }
}
