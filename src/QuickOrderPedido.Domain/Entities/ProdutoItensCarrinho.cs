namespace QuickOrderPedido.Domain.Entities
{
    public class ProdutoItensCarrinho
    {
        public int ProdutoId { get; set; }
        public int ItemId { get; set; }
        public string NomeProdutoItem { get; set; }
        public int Quantidade { get; set; }
        public double ValorItem { get; set; }
    }
}
