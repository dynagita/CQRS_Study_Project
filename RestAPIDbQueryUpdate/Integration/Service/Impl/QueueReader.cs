using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestAPIDbQueryUpdate.Integration.Interface;
using RestAPIDbQueryUpdate.Integration.Model;
using RestAPIDbQueryUpdate.Extensions;
using RabbitMQ.Client.Events;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler;

namespace RestAPIDbQueryUpdate.Integration.Impl
{
    public class QueueReader : IQueueReader
    {
        ConnectionFactory _factory;
        RabbitConfig _config = new RabbitConfig();
        ILogger<QueueReader> _logger;
        ReceiveHandlerFactory _handlerFactory;
        IConnection _connection = null;
        IModel _channel = null;

        public QueueReader(IConfiguration configuration, ILogger<QueueReader> log, ReceiveHandlerFactory handlerFactory)
        {
            _logger = log;

            configuration.GetSection("ComponentQueue").Bind(_config);

            _factory = new ConnectionFactory()
            {
                Uri = new Uri(_config.Connection)                
            };

            _handlerFactory = handlerFactory;
        }

        public async Task ReadAsync()
        {
            try
            {
                var connect = GetChannel();

                connect.QueueDeclare(queue: _config.QueueName,
                                     durable: _config.Durable,
                                     exclusive: _config.Exclusive,
                                     autoDelete: _config.AutoDelete,
                                     arguments: null);

                        var consumer = new EventingBasicConsumer(connect);
                        
                        consumer.Received += HandleQueue;

                        connect.BasicConsume(queue: _config.QueueName,
                                             autoAck: true,
                                             consumer: consumer);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(QueueReader)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");
                throw;
                //implement resilience for getting sure data will get into queue
            }
        }

        protected void HandleQueue(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var envelope = Encoding.UTF8.GetString(body);
            var message = JsonConvert.DeserializeObject<Message>(envelope);

            _handlerFactory.Entity = message.Entity;

            var handler = _handlerFactory.CreateHandler();

            handler.HandleAsync(message);
        }

        private IConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = _factory.CreateConnection();
            }

            return _connection;
        }

        private IModel GetChannel()
        {
            if (_channel == null)
            {
                _connection = GetConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: _config.QueueName,
                                    durable: _config.Durable,
                                    exclusive: _config.Exclusive,
                                    autoDelete: _config.AutoDelete,
                                    arguments: null);
            }

            return _channel;
        }

        public async Task StopReadingAsync()
        {
            _channel.Close();
            _channel.Dispose();
            _connection.Close();
            _connection.Dispose();
        }
    }
}
