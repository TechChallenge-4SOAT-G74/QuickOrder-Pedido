using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickOrderPedido.Domain.Entities
{
    public class Carrinho : EntityMongoBase
    {
        public Carrinho(int numeroPedido, int? numeroCliente, double valor, DateTime dataAtualizacao, List<ProdutoCarrinho>? produtosCarrinho)
        {
            NumeroPedido = numeroPedido;
            NumeroCliente = numeroCliente;
            Valor = valor;
            DataAtualizacao = dataAtualizacao;
            ProdutosCarrinho = produtosCarrinho;

            CalculaValorPedido();
        }

        public int NumeroPedido { get; set; }
        public int? NumeroCliente { get; set; }
        public double Valor { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public List<ProdutoCarrinho>? ProdutosCarrinho { get; set; }

        void CalculaValorPedido()
        {
            this.Valor = this.ProdutosCarrinho?.Sum(x => x.ValorProduto) ?? this.Valor;
        }

    }

    public class ProdutoCarrinho
    {
        public ProdutoCarrinho(string categoriaProduto, string nomeProduto, int idProduto, double valorProduto, List<ProdutoItensCarrinho>? produtosItensCarrinho)
        {
            CategoriaProduto = categoriaProduto;
            NomeProduto = nomeProduto;
            IdProduto = idProduto;
            ValorProduto = valorProduto;
            ProdutosItensCarrinho = produtosItensCarrinho;

            RecalculaValorProduto();
        }

        public string CategoriaProduto { get; set; }
        public string NomeProduto { get; set; }
        public int IdProduto { get; set; }
        public double ValorProduto { get; set; }
        public List<ProdutoItensCarrinho>? ProdutosItensCarrinho { get; set; }

        void RecalculaValorProduto()
        {
            this.ValorProduto = this.ProdutosItensCarrinho?.Sum(x => x.ValorItem) ?? this.ValorProduto;
        }

    }

    public class ProdutoItensCarrinho
    {
        public int ProdutoId { get; set; }
        public int ItemId { get; set; }
        public string NomeProdutoItem { get; set; }
        public int Quantidade { get; set; }
        public double ValorItem { get; set; }
    }
}
