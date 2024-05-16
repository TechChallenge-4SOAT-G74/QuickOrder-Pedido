﻿using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using QuickOrderPedido.Infra.Gateway.Core;

namespace QuickOrderPedido.Infra.Gateway.Gateway
{
    public class PedidoStatusGateway : BaseMongoDBRepository<PedidoStatus>, IPedidoStatusGateway
    {
        public PedidoStatusGateway(IMondoDBContext mondoDBContext) : base(mondoDBContext)
        {
        }
    }
}
