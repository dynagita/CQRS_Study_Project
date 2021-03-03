using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using WritableRESTAPI.Infrastructure.Queue;
using WritableRESTAPI.Integration.Interface;
using WritableRESTAPI.Integration.Model;
using WritableRESTAPI.Util.Extensions;

namespace WritableRESTAPI.Integration.Impl
{
    public class QueueSender : IQueueSender
    {
        ConnectionFactory factory;
        ILogger<QueueSender> logger;
        RabbitConfig RabbitConfig = new RabbitConfig();
        IConnection connection = null;
        IModel channel = null;
        public QueueSender(IConfiguration config, ILogger<QueueSender> log)
        {
            logger = log;
            config.GetSection(nameof(RabbitConfig)).Bind(RabbitConfig);
            factory = new ConnectionFactory()
            {
                Uri = new Uri(RabbitConfig.Connection)
            };
        }
        public async Task Send(object entity, QueueMethod method)
        {
            try
            {
                var connect = GetChannel();

                var envelop = new Message();
                envelop.Entity = entity.GetType().Name;
                envelop.Envelop = JsonConvert.SerializeObject(entity);
                envelop.Method = (int)method;

                var jsonSerializedMessage = JsonConvert.SerializeObject(envelop);

                var serializedMessage = Encoding.UTF8.GetBytes(jsonSerializedMessage);

                connect.BasicPublish(exchange: "",
                                        routingKey: RabbitConfig.QueueName,
                                        basicProperties: null,
                                        body: serializedMessage);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(QueueSender)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");
                //Send to errors queue
            }
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
                channel.QueueDeclare(queue: RabbitConfig.QueueName,
                                    durable: RabbitConfig.Durable,
                                    exclusive: RabbitConfig.Exclusive,
                                    autoDelete: RabbitConfig.AutoDelete,
                                    arguments: null);
            }

            return channel;
        }
    }
}
