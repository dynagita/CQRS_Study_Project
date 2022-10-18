using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Integration.Impl;
using RestAPIDbQueryUpdate.Integration.Interface;
using RestAPIDbQueryUpdate.Integration.Model;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.Service.Impl
{
    public class RabbitEventHostedService : IHostedService
    {
        private readonly AsyncRetryPolicy _retry;
        private readonly IQueueReader _queueReader;
        public RabbitEventHostedService(IQueueReader queueReader)
        {
            _queueReader = queueReader;
            _retry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, attemptRetry => TimeSpan.FromSeconds(Math.Pow(30, attemptRetry)));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _retry.ExecuteAsync(async () => await _queueReader.ReadAsync());
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _retry.ExecuteAsync(async () => await _queueReader.StopReadingAsync());
        }
    }
}
