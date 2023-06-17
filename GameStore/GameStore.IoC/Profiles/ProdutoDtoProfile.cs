using AutoMapper;
using GameStore.Domain.Dtos;
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
            CreateMap<ProdutoFormDto, ProdutoDto>().ReverseMap();
        }
    }
}
