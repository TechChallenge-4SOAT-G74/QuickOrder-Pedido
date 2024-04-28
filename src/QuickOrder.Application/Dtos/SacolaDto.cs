namespace QuickOrderPedido.Application.Dtos
{
    public class SacolaDto
    {
        public string NumeroPedido { get; set; }
        public string? NumeroCliente { get; set; }
        public string CarrinhoId { get; set; }
        public double Valor { get; set; }
    }
}
