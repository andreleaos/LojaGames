using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Contracts.Base
{
    public interface IGenericRepoService<T, Y>
    {
        void Create(T entity);
        T? GetById(Y id);
        List<T> GetAll();
        void Update(T entity);
        bool Delete(Y id);
    }
}
