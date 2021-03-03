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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IGraphQLAPIContext context) : base(context)
        {
        }

        protected override FilterDefinition<User> GetDynamicFiler(User entity)
        {
            FilterDefinition<User> filter = null;

            if (!string.IsNullOrEmpty(entity.Id))
            {
                filter = Builders<User>.Filter.Eq(x => x.Id, entity.Id);
            }

            if (!string.IsNullOrEmpty(entity.Name))
            {
                if (filter != null)
                {
                    filter = filter & Builders<User>.Filter.Eq(x => x.Name, entity.Name);
                }
                else
                {
                    filter = Builders<User>.Filter.Eq(x => x.Name, entity.Name);
                }
            }

            if (!string.IsNullOrEmpty(entity.LastName))
            {
                if (filter != null)
                {
                    filter = filter & Builders<User>.Filter.Eq(x => x.LastName, entity.LastName);
                }
                else
                {
                    filter = Builders<User>.Filter.Eq(x => x.LastName, entity.LastName);
                }
            }

            if (!string.IsNullOrEmpty(entity.Email))
            {
                if (filter != null)
                {
                    filter = filter & Builders<User>.Filter.Eq(x => x.Email, entity.Email);
                }
                else
                {
                    filter = Builders<User>.Filter.Eq(x => x.Email, entity.Email);
                }
            }

            if (!string.IsNullOrEmpty(entity.Email))
            {
                if (filter != null)
                {
                    filter = filter & Builders<User>.Filter.Eq(x => x.WritableRelation, entity.WritableRelation);
                }
                else
                {
                    filter = Builders<User>.Filter.Eq(x => x.WritableRelation, entity.WritableRelation);
                }
            }

            return filter;
        }
    }
}
