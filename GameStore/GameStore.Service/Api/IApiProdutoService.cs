using GameStore.Domain.Contracts.Base;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Api
{
    public interface IApiProdutoService : IGenericApiService<Produto, int>
    {

    }
}
