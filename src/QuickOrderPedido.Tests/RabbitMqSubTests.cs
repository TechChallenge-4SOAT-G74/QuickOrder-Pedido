using Microsoft.Extensions.Options;
using Moq;
using QuickOrderPedido.Infra.MQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Xunit;

namespace QuickOrderPedido.Tests
{
    public class RabbitMqSubTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldProcessReceivedMessage()
        {
            // Arrange
            var configuration = Options.Create(new RabbitMqSettings { Host = "localhost", Port = "5672" });
            var processaEventoMock = new Mock<IProcessaEvento>();
            var connectionMock = new Mock<IConnection>();
            var channelMock = new Mock<IModel>();
            var consumerMock = new Mock<EventingBasicConsumer>(channelMock.Object);

            var rabbitMqSub = new RabbitMqSub(configuration, processaEventoMock.Object);

            connectionMock.Setup(c => c.CreateModel()).Returns(channelMock.Object);
            channelMock.Setup(c => c.QueueDeclare("queueName", false, false,true, null).QueueName).Returns("queueName");
            channelMock.Setup(c => c.BasicConsume(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<IBasicConsumer>()))
                .Callback<string, bool, IBasicConsumer>((queue, autoAck, consumer) =>
                {
                    consumer.HandleBasicDeliver("consumerTag", 1, false, "exchange", "routingKey", null, Encoding.UTF8.GetBytes("message"));
                });

            // Act
            await rabbitMqSub.StartAsync(CancellationToken.None);
            await Task.Delay(100); // Wait for the message to be processed
            await rabbitMqSub.StopAsync(CancellationToken.None);

            // Assert
            processaEventoMock.Verify(p => p.Processa("message"), Times.Once);
        }
    }
}
