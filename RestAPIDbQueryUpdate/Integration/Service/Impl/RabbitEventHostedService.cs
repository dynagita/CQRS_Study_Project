using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Integration.Impl;
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
        ConnectionFactory factory;
        RabbitConfig config = new RabbitConfig();
        ILogger<QueueReader> logger;
        ReceiveHandlerFactory _handlerFactory;
        IConnection connection = null;
        IModel channel = null;

        public RabbitEventHostedService(IConfiguration configuration, ILogger<QueueReader> log, ReceiveHandlerFactory handlerFactory)
        {
            logger = log;

            configuration.GetSection("ComponentQueue").Bind(config);

            factory = new ConnectionFactory()
            {
                Uri = new Uri(config.Connection)
            };

            _handlerFactory = handlerFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var connect = GetChannel();

                connect.QueueDeclare(queue: config.QueueName,
                                     durable: config.Durable,
                                     exclusive: config.Exclusive,
                                     autoDelete: config.AutoDelete,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(connect);

                consumer.Received += HandleQueue;

                connect.BasicConsume(queue: config.QueueName,
                                     autoAck: true,
                                     consumer: consumer);

            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(QueueReader)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");

                //implement resilience for getting sure data will get into queue
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        protected void HandleQueue(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var envelope = Encoding.UTF8.GetString(body);
            var message = JsonConvert.DeserializeObject<Message>(envelope);

            _handlerFactory.Entity = message.Entity;

            var handler = _handlerFactory.CreateHandler();

            handler.Handle(message);
        }

        private IConnection GetConnection()
        {
            if (connection == null)
            {
                connection = factory.CreateConnection();
            }

            return connection;
        }

        private IModel GetChannel()
        {
            if (channel == null)
            {
                connection = GetConnection();
                channel = connection.CreateModel();
                channel.QueueDeclare(queue: config.QueueName,
                                    durable: config.Durable,
                                    exclusive: config.Exclusive,
                                    autoDelete: config.AutoDelete,
                                    arguments: null);
            }

            return channel;
        }
    }
}
