using Microsoft.Extensions.Logging;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Interface;
using RestAPIDbQueryUpdate.Repository.Interface;

namespace RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Impl
{
    public class UserReceiveHandler : ReceiveHandler<User>, IReceiveHandler
    {
        public UserReceiveHandler(IUserRepository repository, ILogger<User> logger) : base(repository, logger)
        {
        
        }        
    }
}
