using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.Interface
{
    public interface IQueueReader
    {
        Task ReadAsync();
        Task StopReadingAsync();
    }
}
