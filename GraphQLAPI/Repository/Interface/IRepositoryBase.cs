using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLAPI.Repository.Interface
{
    public interface IRepositoryBase<T>
    {
        Task<IList<T>> ListAsync();

        Task<IList<T>> ListAsync(FilterDefinition<T> expression);

        Task<T> GetByIdAsync(string id);

        Task<T> GetAsync(FilterDefinition<T> expression);

        Task<IList<T>> ListFilteringByEntityAsync(T entity);

        Task<T> GetFilteringByEntityAsync(T entity);
    }
}
