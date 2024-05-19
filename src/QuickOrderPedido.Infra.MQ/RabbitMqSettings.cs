namespace QuickOrderPedido.Infra.MQ
{
    public class RabbitMqSettings
    {
        public string Host { get; set; } = null!;
        public string Port { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
