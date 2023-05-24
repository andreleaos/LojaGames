using GameStore.Domain.Entities;
using GameStore.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Services
{
    public interface IApiProdutoService : IGenericApiService<Produto, int>
    {

    }
}
