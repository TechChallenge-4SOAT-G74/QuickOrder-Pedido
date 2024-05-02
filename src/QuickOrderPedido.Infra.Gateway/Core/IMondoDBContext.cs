﻿using MongoDB.Driver;

namespace QuickOrderPedido.Infra.Gateway.Core
{
    public interface IMondoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
