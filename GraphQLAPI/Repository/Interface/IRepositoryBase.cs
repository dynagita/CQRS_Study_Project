using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLAPI.Repository.Interface
{
    public interface IRepositoryBase<T>
    {
        Task<IList<T>> List();

        Task<IList<T>> List(FilterDefinition<T> expression);

        Task<T> GetById(string id);

        Task<T> Get(FilterDefinition<T> expression);

        Task<IList<T>> ListFilteringByEntity(T entity);

        Task<T> GetFilteringByEntity(T entity);
    }
}
