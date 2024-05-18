namespace QuickOrderPedido.Domain.Adapters
{
    public interface IBaseMongoDBRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(string id);
        Task<TEntity> Get(string id);
        Task<TEntity> GetValue(string column, string value);
        Task<TEntity> GetValue(string column, int value);
        Task<IEnumerable<TEntity>> GetAll();

    }
}
