namespace QuickOrderPedido.Application.Dtos
{
    public class SacolaDto
    {
        public string CodigoPedido { get; set; }
        public string? NumeroCliente { get; set; }
        public string CarrinhoId { get; set; }
        public double Valor { get; set; }
    }
}
