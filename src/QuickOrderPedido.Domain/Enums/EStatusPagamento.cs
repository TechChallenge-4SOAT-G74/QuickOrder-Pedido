using System.ComponentModel;

namespace QQuickOrderPedido.Domain.Enums
{
    public enum EStatusPagamento
    {
        [Description("Aguardando Pagamento")]
        Aguardando = 1,

        [Description("processando pagamento")]
        Processando = 2,

        [Description("Pagamento Aprovado")]
        Aprovado = 3,

        [Description("Pagamento Negado")]
        Negado = 4,
    }

    public static class EStatusPagamentoExtensions
    {
        public static string ToDescriptionString(this EStatusPagamento val)
        {
            var type = val.GetType();
            var attributes = type.GetField(val.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            return attributes?.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
