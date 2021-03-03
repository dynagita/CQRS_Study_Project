using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Integration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Interface
{
    public interface IReceiveHandler
    {
        Task Handle(Message message);
    }
}
