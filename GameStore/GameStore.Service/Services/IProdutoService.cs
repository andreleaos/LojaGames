using GameStore.Domain.Contracts.Base;
using GameStore.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Services
{
    public interface IProdutoService : IGenericRepoService<ProdutoDto, int>
    {

    }
}
