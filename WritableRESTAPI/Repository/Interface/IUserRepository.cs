using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Infrastructure.Context;
using WritableRESTAPI.Repository.Impl;

namespace WritableRESTAPI.Repository.Interface
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserByMail(string email);
    }
}
