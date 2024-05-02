namespace QuickOrderPedido.Infra.MQ
{
    public interface IRabbitMqPub<T> where T : class
    {
        void Publicar(T obj);
    }
}
