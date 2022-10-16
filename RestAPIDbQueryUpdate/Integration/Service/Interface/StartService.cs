using Microsoft.Extensions.Hosting;
using RestAPIDbQueryUpdate.Integration.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.Service.Interface
{
    public class StartService : IHostedService
    {
        private readonly IQueueReader _reader;
        public StartService(IQueueReader reader)
        {
            _reader = reader;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _reader.Read();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _reader.StopReading();
        }
    }
}
