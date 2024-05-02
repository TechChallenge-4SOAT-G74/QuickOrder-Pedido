using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace QuickOrderPedido.Infra.Gateway.Core
{
    public class MondoDBContext : IMondoDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _client { get; set; }
        public IClientSessionHandle SessionHandle { get; set; }

        public MondoDBContext(IOptions<DatabaseMongoDBSettings> configuration)
        {
            _client = new MongoClient(configuration.Value.ConnectionString);
            _db = _client.GetDatabase(configuration.Value.DatabaseName);
        }


        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
