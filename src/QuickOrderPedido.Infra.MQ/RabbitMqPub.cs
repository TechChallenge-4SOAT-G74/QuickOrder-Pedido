using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace QuickOrderPedido.Infra.MQ
{
    public abstract class RabbitMqPub<T> : IRabbitMqPub<T>  where T : class
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqPub(IOptions<RabbitMqSettings> configuration)
        {
            _connection = new ConnectionFactory() { HostName = configuration.Value.Host, Port = Int32.Parse(configuration.Value.Port) }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        }

        public void Publicar(T obj)
        {
            string mensagem = JsonSerializer.Serialize(obj);
            var body = Encoding.UTF8.GetBytes(mensagem);

            _channel.BasicPublish(exchange: "trigger",
                routingKey: "Pedido",
                basicProperties: null,
                body: body
                );

        }
    }
}
