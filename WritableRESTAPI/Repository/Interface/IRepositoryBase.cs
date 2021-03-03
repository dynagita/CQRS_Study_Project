using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WritableRESTAPI.Repository.Interface
{
    public interface IRepositoryBase<T>
    {
        Task<T> Get(int id);

        Task<T> Insert(T entity);

        Task<T> Update(int id, T entity);

        Task<T> Delete(int id);
    }
}
