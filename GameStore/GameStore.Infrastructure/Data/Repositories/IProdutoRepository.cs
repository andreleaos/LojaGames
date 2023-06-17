using GameStore.Domain.Contracts.Base;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Data.Repositories
{
    public interface IProdutoRepository : IGenericRepoService<Produto, int>
    {
        Task<bool> SeedAsync(string nomeTabela, int retry = 0);
    }
}
