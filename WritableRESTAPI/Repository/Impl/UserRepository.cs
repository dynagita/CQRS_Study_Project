using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Infrastructure.Context;
using WritableRESTAPI.Repository.Interface;

namespace WritableRESTAPI.Repository.Impl
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(WritableDbContext context) : base(context)
        {
        }

        public User GetUserByMail(string email)
        {
            return _dbSet.FirstOrDefault(x => x.Email.Equals(email));
        }
    }
}
