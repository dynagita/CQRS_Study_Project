using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Repository.Interface
{
    public interface IRepositoryBase<T>
    {
        Task Update(T entity);
        Task Insert(T entity);
        Task Delete(T entity);

        Task<T> FindOne(long writableRelation);
    }
}
