﻿using QuickOrderPedido.Domain.Entities;

namespace QuickOrderPedido.Domain.Adapters
{
    public interface ICarrinhoGateway : IBaseGateway, IBaseMongoDBRepository<Carrinho>
    {
    }
}
