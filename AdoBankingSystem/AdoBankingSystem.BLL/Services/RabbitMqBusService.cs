using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using AdoBankingSystem.Shared.DTOs;

namespace AdoBankingSystem.BLL.Services
{
    public class RabbitMqBusService
    {
        private readonly IConnectionFactory _connectionFactory = null;

        public bool IsConnectionAvailable()
        {
            try
            {
                IConnection connection = _connectionFactory.CreateConnection();

                bool result = connection.IsOpen;
                if (result) return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void PublishMessageToQueue<T>(string queueName, T message) where T : class
        {
            using (IConnection connection = _connectionFactory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body : body);
            }
        }
        public RabbitMqBusService()
        {
            _connectionFactory = new ConnectionFactory()
            {
                UserName = "test",
                Password = "test",
                VirtualHost = "/",
                Port = 5672,
                HostName = "52.166.219.193"
            };
        }

        public void PublishMessageToStorage<T>(T data) where T : EntityDto
        {
            PublishMessageToQueue<T>(typeof(T).ToString(), data);
        }


    }
}
