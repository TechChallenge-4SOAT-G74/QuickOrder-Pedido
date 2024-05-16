namespace QuickOrderPedido.Domain.Entities
{
    public class Pedido : EntityMongoBase
    {
        public Pedido(
                     DateTime dataHoraInicio,
                     DateTime? dataHoraFinalizado,
                     int clienteId,
                     string? carrinhoId,
                     double valorPedido,
                     bool pedidoPago,
                     List<ProdutoCarrinho>? produtosCarrinho,
                     string? observacao = null)
        {
            DataHoraInicio = dataHoraInicio;
            DataHoraFinalizado = dataHoraFinalizado;
            ClienteId = clienteId;
            CarrinhoId = carrinhoId;
            Produtos = produtosCarrinho;
            ValorPedido = valorPedido;
            PedidoPago = pedidoPago;
            Observacao = observacao;

            CalculaPrecoPedido();
        }
        public virtual string? CarrinhoId { get; set; }
        public virtual DateTime DataHoraInicio { get; set; }
        public virtual DateTime? DataHoraFinalizado { get; set; }
        public virtual int ClienteId { get; set; }
        public virtual double ValorPedido { get; set; }
        public virtual string? Observacao { get; set; }
        public virtual bool PedidoPago { get; set; }
        public List<ProdutoCarrinho>? Produtos { get; set; }

        void CalculaPrecoPedido()
        {
            this.ValorPedido = this.Produtos?.Sum(x => x.ValorProduto) ?? this.ValorPedido;
        }

    }
}
