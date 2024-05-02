namespace QuickOrderPedido.Infra.Gateway.Core
{
    public class DatabaseMongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string SecretManagerKey { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

    }
}
