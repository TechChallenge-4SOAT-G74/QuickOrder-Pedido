using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickOrderPedido.Infra.MQ
{
    public class RabbitMqSettings
    {
        public string Host { get; set; } = null!;
        public string Port { get; set; } = null!;
    }
}
