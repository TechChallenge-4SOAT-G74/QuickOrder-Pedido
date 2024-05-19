using System.Diagnostics.CodeAnalysis;

namespace QuickOrderPedido.Infra.MQ
{
    [ExcludeFromCodeCoverage]
    public class RabbitMqSettings
    {
        public string Host { get; set; } = null!;
        public string Port { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
