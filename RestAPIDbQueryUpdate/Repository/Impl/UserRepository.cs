using MongoDB.Driver;
using RestAPIDbQueryUpdate.Context;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Repository.Impl
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbContext context) : base(context)
        {
        }

        protected override FilterDefinition<User> GetEntityFilter(User entity)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(x => x.WritableRelation, entity.WritableRelation);

            return filter;
        }

        protected override UpdateDefinition<User> GetUpdateDefinition(User entity)
        {
            var update = Builders<User>
                        .Update
                        .Set(x => x.Name, entity.Name)
                        .Set(x => x.LastName, entity.LastName)
                        .Set(x => x.Email, entity.Email)
                        .Set(x => x.WritableRelation, entity.WritableRelation);

            return update;
        }
    }
}
