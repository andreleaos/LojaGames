using GameStore.Domain.Contracts.Base;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Client
{
    public interface IProdutoClientService : IGenericApiService<ProdutoViewDto, int>
    {
    }
}
