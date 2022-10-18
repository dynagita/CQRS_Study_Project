using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Integration.Model;

namespace WritableRESTAPI.Integration.Interface
{
    public interface IQueueSender
    {
        Task SendAsync(object message, QueueMethod method);
    }
}
