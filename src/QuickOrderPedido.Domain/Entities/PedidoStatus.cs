namespace QuickOrderPedido.Domain.Entities
{
    public class PedidoStatus : EntityMongoBase
    {
        public PedidoStatus(string codigoPedido, string statusPedido, DateTime dataAtualizacao)
        {
            CodigoPedido = codigoPedido;
            StatusPedido = statusPedido;
            DataAtualizacao = dataAtualizacao;
        }

        public string CodigoPedido { get; set; }
        public string StatusPedido { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
