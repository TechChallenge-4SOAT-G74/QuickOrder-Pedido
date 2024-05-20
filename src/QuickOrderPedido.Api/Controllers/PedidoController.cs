using Microsoft.AspNetCore.Mvc;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace QuickOrderPedido.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    public static class PedidoController
    {
        public static void RegisterPedidoController(this WebApplication app)
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

            app.MapPut("/confirmarpedido/{codigoPedido}", async ([FromServices] IPedidoAtualizarUseCase pedidoAtualizarUseCase, string codigoPedido) =>
            {
                return Results.Ok(await pedidoAtualizarUseCase.ConfirmarPedido(codigoPedido));
            });

            app.MapDelete("/cancelarpedido/{codigoPedido}", async ([FromServices] IPedidoExcluirUseCase pedidoExcluirUseCase, string codigoPedido) =>
            {
                return Results.Ok(await pedidoExcluirUseCase.CancelarPedido(codigoPedido, EStatusPedido.CanceladoCliente.ToString()));
            });

            app.MapPost("/criarpedido/{idcliente}", async ([FromServices] IPedidoCriarUseCase pedidoCriarUseCase, int idcliente) =>
            {
                return Results.Ok(await pedidoCriarUseCase.CriarPedido(idcliente));
            });
        }
    }
}


