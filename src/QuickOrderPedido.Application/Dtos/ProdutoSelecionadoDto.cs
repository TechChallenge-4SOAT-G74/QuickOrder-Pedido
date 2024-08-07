namespace QuickOrderPedido.Application.Dtos
{
    public class ProdutoSelecionadoDto
    {
        public int IdCliente { get; set; }
        public string? CategoriaProduto { get; set; }
        public string? NomeProduto { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public double ValorProduto { get; set; }
        public List<ProdutoItens>? ProdutosItens { get; set; }
    }

    public class ProdutoItens
    {
        public string? NomeProdutoItem { get; set; }
        public int Quantidade { get; set; }
        public double ValorItem { get; set; }
    }
}
