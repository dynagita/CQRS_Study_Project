using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Repository.Interface
{
    public interface IRepositoryBase<T>
    {
        Task UpdateAsync(T entity);
        Task InsertAsync(T entity);
        Task DeleteAsync(T entity);

        Task<T> FindOneAsync(long writableRelation);
    }
}
