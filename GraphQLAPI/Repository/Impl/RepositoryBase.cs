using GraphQLAPI.Domain;
using GraphQLAPI.Repository.Context;
using GraphQLAPI.Repository.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Repository.Impl
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly IMongoCollection<T> _collection;
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
        }

        public async Task<T> Get(FilterDefinition<T> expression)
        {
            return await Task.Run(() =>
            {
                return _collection.Find<T>(expression).FirstOrDefault();
            });

        }

        public async Task<T> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return _collection.Find<T>(collection => collection.Id.Equals(id)).FirstOrDefault();
            });

        }

        public async Task<IList<T>> List()
        {
            return await Task.Run(() =>
            {
                return _collection.Find(collection => true).ToList();
            });

        }

        public async Task<IList<T>> List(FilterDefinition<T> expression)
        {
            return await Task.Run(() =>
            {
                return _collection.Find(expression).ToList();
            });

        }

        public async Task<IList<T>> ListFilteringByEntity(T entity)
        {
            var filter = GetDynamicFiler(entity);
            if (filter == null)
            {
                return await List();
            }
            else
            {
                return await List(filter);
            }
        }

        public async Task<T> GetFilteringByEntity(T entity)
        {
            var filter = GetDynamicFiler(entity);
            if (filter == null)
            {
                throw new ArgumentException("You can't get a user with any filters.");
            }
            return await Get(filter);
        }

        protected virtual FilterDefinition<T> GetDynamicFiler(T entity)
        {
            throw new NotImplementedException("Please, rewrite this method into specific repository.");
        }
    }
}
