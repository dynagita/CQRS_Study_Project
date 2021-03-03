using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Integration.Interface;
using WritableRESTAPI.Repository.Interface;
using WritableRESTAPI.Service.Interface;
using WritableRESTAPI.ViewModel;

namespace WritableRESTAPI.Service.Impl
{
    public class UserService : ServiceBase<UserViewModel, User>, IUserService
    {
        IUserRepository _repository;
        public UserService(IUserRepository repository, IMapper mapper, IQueueSender queue, ILogger<UserViewModel> logger) : base(repository, mapper, queue, logger)
        {
            _repository = repository;
        }       
    }
}
