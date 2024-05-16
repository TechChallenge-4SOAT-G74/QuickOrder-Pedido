namespace QuickOrderPedido.Domain.Entities
{
    public class ProdutoCarrinho
    {
        public ProdutoCarrinho(string categoriaProduto, string nomeProduto, int idProduto, int quantidade, double valorProduto, List<ProdutoItensCarrinho>? produtosItensCarrinho)
        {
            CategoriaProduto = categoriaProduto;
            NomeProduto = nomeProduto;
            IdProduto = idProduto;
            Quantidade = quantidade;
            ValorProduto = valorProduto;
            ProdutosItensCarrinho = produtosItensCarrinho;

            RecalculaValorProduto();
        }

        public string CategoriaProduto { get; set; }
        public string NomeProduto { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public double ValorProduto { get; set; }
        public List<ProdutoItensCarrinho>? ProdutosItensCarrinho { get; set; }

        void RecalculaValorProduto()
        {
            this.ValorProduto = this.ProdutosItensCarrinho?.Sum(x => x.ValorItem * x.Quantidade) ?? this.ValorProduto;
        }

    }
}
