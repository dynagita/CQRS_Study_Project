using GraphQLAPI.Domain;
using GraphQLAPI.Repository.Context;
using GraphQLAPI.Repository.Interface;
using MongoDB.Driver;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Repository.Impl
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly IMongoCollection<T> _collection;
        private readonly AsyncRetryPolicy _retry;

        public RepositoryBase(IGraphQLAPIContext context)
        {
            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _collection = database.GetCollection<T>(typeof(T).Name.ToLower());

            if (_collection == null)
            {
                database.CreateCollection(typeof(T).Name);
                _collection = database.GetCollection<T>(typeof(T).Name.ToLower());
            }
            _retry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, attemptRetry => TimeSpan.FromSeconds(Math.Pow(3, attemptRetry)));
        }

        public async Task<T> GetAsync(FilterDefinition<T> expression)
        {
            var data = await _retry.ExecuteAsync(async () => await _collection.FindAsync<T>(expression));
            return data.FirstOrDefault();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var data = await _retry.ExecuteAsync(async () => await _collection.FindAsync<T>(collection => collection.Id.Equals(id)));
            return data.FirstOrDefault();
        }

        public async Task<IList<T>> ListAsync()
        {
            var data = await _retry.ExecuteAsync(async () => await _collection.FindAsync(collection => true));
            return data.ToList();
        }

        public async Task<IList<T>> ListAsync(FilterDefinition<T> expression)
        {
            var data = await _retry.ExecuteAsync(async () => await _collection.FindAsync(expression));
            return data.ToList();
        }

        public async Task<IList<T>> ListFilteringByEntityAsync(T entity)
        {
            var filter = GetDynamicFiler(entity);
            if (filter == null)
            {
                return await ListAsync();
            }
            else
            {
                return await ListAsync(filter);
            }
        }

        public async Task<T> GetFilteringByEntityAsync(T entity)
        {
            var filter = GetDynamicFiler(entity);
            if (filter == null)
            {
                throw new ArgumentException("You can't get a user with any filters.");
            }
            return await GetAsync(filter);
        }

        protected virtual FilterDefinition<T> GetDynamicFiler(T entity)
        {
            throw new NotImplementedException("Please, rewrite this method into specific repository.");
        }
    }
}
