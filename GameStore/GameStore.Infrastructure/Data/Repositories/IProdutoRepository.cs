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
        void CreateTablesAndSeed(int retry = 0);
        void CreateProcedures(int retry = 0);
        void CreateProceduresIniciais(int retry = 0);
        void CreateDataBaseLocal(int retry = 0);
        Task<List<Catetoria>> GetAllCategoria();
    }
}
