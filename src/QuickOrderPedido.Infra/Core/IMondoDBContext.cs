using MongoDB.Driver;

namespace QuickOrderPedido.Infra.Core
{
    public interface IMondoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
