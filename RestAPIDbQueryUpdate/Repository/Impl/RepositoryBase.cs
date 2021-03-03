using MongoDB.Driver;
using RestAPIDbQueryUpdate.Context;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Repository.Impl
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly IMongoCollection<T> _collection;
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
        }

        public async Task Delete(T entity)
        {
            await _collection.DeleteOneAsync(GetEntityFilter(entity));
        }

        public async Task<T> FindOne(long writableRelation)
        {
                var filter = Builders<T>.Filter.Eq(x => x.WritableRelation, writableRelation);

                return await _collection.Find<T>(filter).FirstOrDefaultAsync();
        }

        public async Task Insert(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(T entity)
        {
             await _collection.UpdateOneAsync(GetEntityFilter(entity), GetUpdateDefinition(entity));
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
