using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.Service.Interface
{
    public interface IEventHostedService : IHostedService
    {
        Task StartReadingQueue();
    }
}
