using MongoDB.Driver;
using Polly;
using Polly.Retry;
using RestAPIDbQueryUpdate.Context;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Repository.Impl
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly IMongoCollection<T> _collection;
        private readonly AsyncRetryPolicy _retry;
        public RepositoryBase(IDbContext context)
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
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)));
        }

        public async Task DeleteAsync(T entity)
        {
            await _retry.ExecuteAsync(async () => await _collection.DeleteOneAsync(GetEntityFilter(entity)));
        }

        public async Task<T> FindOneAsync(long writableRelation)
        {
            var filter = Builders<T>.Filter.Eq(x => x.WritableRelation, writableRelation);
            var data = await _retry.ExecuteAsync(async () => await _collection.FindAsync<T>(filter));
            return data.FirstOrDefault();
        }

        public async Task InsertAsync(T entity)
        {
            await _retry.ExecuteAsync(async () => await _collection.InsertOneAsync(entity));
        }

        public async Task UpdateAsync(T entity)
        {
            await _retry.ExecuteAsync(async () => await _collection.UpdateOneAsync(GetEntityFilter(entity), GetUpdateDefinition(entity)));
        }

        protected virtual FilterDefinition<T> GetEntityFilter(T entity)
        {
            throw new NotImplementedException("Please, rewrite this method into specific repository.");
        }

        protected virtual UpdateDefinition<T> GetUpdateDefinition(T entity)
        {
            throw new NotImplementedException("Please, rewrite this method into specific repository.");
        }
    }
}
