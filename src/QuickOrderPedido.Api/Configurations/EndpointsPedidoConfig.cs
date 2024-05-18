using Microsoft.AspNetCore.Mvc;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Enums;

namespace QuickOrderPedido.Api.Configurations
{
    public static class EndpointsPedidoConfig
    {
        public static void RegisterPedidoEndpoints(this WebApplication app)
        {
            app.MapGet("/consultarpedido/{id}", async ([FromServices] IPedidoObterUseCase pedidoObterUseCase, string id) =>
            {
                return Results.Ok(await pedidoObterUseCase.ConsultarPedido(id));
            });

            app.MapGet("/consultarlistapedidos", async ([FromServices] IPedidoObterUseCase pedidoObterUseCase) =>
            {
                return Results.Ok(await pedidoObterUseCase.ConsultarListaPedidos());
            });

            app.MapPut("/finalizarcarrinho/{numeroCliente}", async ([FromServices] IPedidoAtualizarUseCase pedidoAtualizarUseCase, int numeroCliente) =>
            {
                return Results.Ok(await pedidoAtualizarUseCase.FinalizarCarrinho(numeroCliente));
            });

            app.MapDelete("/cancelarpedido/{codigoPedido}", async ([FromServices] IPedidoExcluirUseCase pedidoExcluirUseCase, string codigoPedido) =>
            {
                return Results.Ok(await pedidoExcluirUseCase.CancelarPedido(codigoPedido, EStatusPedido.CanceladoCliente.ToString()));
            });
        }
    }
}


