namespace QuickOrderPedido.Domain.Entities
{
    public class Carrinho : EntityMongoBase
    {
        public Carrinho() { }

        public Carrinho(int numeroCliente, double valor, DateTime dataAtualizacao, List<ProdutoCarrinho>? produtosCarrinho)
        {
            NumeroCliente = numeroCliente;
            Valor = valor;
            DataAtualizacao = dataAtualizacao;
            ProdutosCarrinho = produtosCarrinho;

            CalculaValorPedido();
        }

        public int NumeroCliente { get; set; }
        public double Valor { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public List<ProdutoCarrinho>? ProdutosCarrinho { get; set; }

        void CalculaValorPedido()
        {
            this.Valor = this.ProdutosCarrinho?.Sum(x => x.ValorProduto) ?? this.Valor;
        }

    }
}
