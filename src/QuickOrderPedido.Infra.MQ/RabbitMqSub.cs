using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace QuickOrderPedido.Infra.MQ
{
    [ExcludeFromCodeCoverage]
    public class RabbitMqSub : BackgroundService
    {
        private readonly string _nomeDaFilaProduto;
        private readonly string _nomeDaFilaPagamento;
        private IModel _channelProduto;
        private IModel _channelPagamento;
        private readonly IProcessaEvento _processaEvento;
        private readonly string _exchange = "QuickOrder";
        public RabbitMqSub(IOptions<RabbitMqSettings> configuration, IProcessaEvento processaEvento)
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

            _channelProduto = connection.CreateModel();
            _nomeDaFilaProduto = "Produto_Selecionado";
            _channelProduto.QueueBind(queue: _nomeDaFilaProduto, exchange: _exchange, routingKey: "Produto");

            _channelPagamento = connection.CreateModel();
            _nomeDaFilaPagamento = "Produto_Selecionado";
            _channelPagamento.QueueBind(queue: _nomeDaFilaPagamento, exchange: _exchange, routingKey: "Pagamento");


            _processaEvento = processaEvento;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EventingBasicConsumer? consumidorProduto = new EventingBasicConsumer(_channelProduto);

            consumidorProduto.Received += (ModuleHandle, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                string? mensagem = Encoding.UTF8.GetString(body.ToArray());
                _processaEvento.ProcessaProduto(mensagem);
            };

            _channelProduto.BasicConsume(queue: _nomeDaFilaProduto, autoAck: true, consumer: consumidorProduto);



            EventingBasicConsumer? consumidorPagamento = new EventingBasicConsumer(_channelPagamento);

            consumidorPagamento.Received += (ModuleHandle, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                string? mensagem = Encoding.UTF8.GetString(body.ToArray());
                _processaEvento.ProcessaPagamento(mensagem);
            };

            _channelPagamento.BasicConsume(queue: _nomeDaFilaPagamento, autoAck: true, consumer: consumidorPagamento);

            return Task.CompletedTask;
        }
    }
}
