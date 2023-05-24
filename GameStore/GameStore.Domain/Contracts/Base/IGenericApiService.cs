using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Contracts.Base
{
    public interface IGenericApiService<T, Y>
    {
        Task Create(T entity);
        Task<T?> GetById(Y id);
        Task<List<T>> GetAll();
        Task Update(T entity);
        Task<bool> Delete(Y id);
    }
}
