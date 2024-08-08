namespace QuickOrderPedido.Infra.MQ
{
    public interface IProcessaEvento
    {
        void ProcessaProduto(string mensagem);
        void ProcessaPagamento(string mensagem);
    }
}
