using Microsoft.Extensions.Options;
using QuickOrderPedido.Application.Events;
using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace QuickOrderPedido.Infra.MQ
{
    [ExcludeFromCodeCoverage]
    public class RabbitMqPub<T> : IRabbitMqPub<T> where T : class
    {
        private readonly IModel _channel;
        private readonly string _exchange = "QuickOrder";

        public RabbitMqPub(IOptions<RabbitMqSettings> configuration)
        {
            var factory = new ConnectionFactory
            {
                // "guest"/"guest" by default, limited to localhost connections
                UserName = configuration.Value.UserName,
                Password = configuration.Value.Password,
                VirtualHost = "/",
                HostName = configuration.Value.Host,
                Port = Int32.Parse(configuration.Value.Port)
            };

            IConnection connection = factory.CreateConnection();

            _channel = connection.CreateModel();
        }

        public void Publicar(T obj, string routingKey, string queue)
        {
            _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Fanout);

            _channel.QueueDeclare(queue: queue,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            string mensagem = JsonSerializer.Serialize(obj);
            var body = Encoding.UTF8.GetBytes(mensagem);

            _channel.BasicPublish(exchange: _exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: body
                );
        }

    }
}
