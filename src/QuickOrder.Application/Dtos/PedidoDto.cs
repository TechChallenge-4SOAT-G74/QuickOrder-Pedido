namespace QuickOrderPedido.Application.Dtos
{
    public class PedidoDto
    {
        public int NumeroPedido { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public DateTime? DataHoraFinalizado { get; set; }
        public int? NumeroCliente { get; set; }
        public double ValorPedido { get; set; }
        public string? Observacao { get; set; }
        public bool PedidoPago { get; set; }
        public string? StatusPedido { get; set; }
        public string? CarrinhoId { get; set; }

    }
}
