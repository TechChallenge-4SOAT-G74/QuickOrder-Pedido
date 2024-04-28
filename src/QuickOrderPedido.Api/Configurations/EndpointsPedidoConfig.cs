using Microsoft.AspNetCore.Mvc;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Entities;

namespace QuickOrderPedido.Api.Configurations
{
    public static class EndpointsPedidoConfig
    {
        public static void RegisterPedidoEndpoints(this WebApplication app)
        {
            app.MapGet("/consultarpedido/{id}", async ([FromServices] IPedidoObterUseCase pedidoObterUseCase, int id) =>
            {
                return Results.Ok(await pedidoObterUseCase.ConsultarPedido(id));
            });

            app.MapGet("/consultarlistapedidos", async ([FromServices] IPedidoObterUseCase pedidoObterUseCase) =>
            {
                return Results.Ok(await pedidoObterUseCase.ConsultarListaPedidos());
            });


            app.MapPost("/criarpedido/{idcliente}", async ([FromServices] IPedidoCriarUseCase pedidoCriarUseCase, int idcliente) =>
            {
                return Results.Ok(await pedidoCriarUseCase.CriarPedido(idcliente));
            });

            app.MapPut("/adicionaritemaopedido/{id}", async ([FromServices] IPedidoAtualizarUseCase pedidoAtualizarUseCase, string id, [FromBody] List<ProdutoCarrinho> produtoCarrinho) =>
            {
                return Results.Ok(await pedidoAtualizarUseCase.AlterarItemAoPedido(id, produtoCarrinho));
            });

            app.MapPut("/confirmapedido/{id}", async ([FromServices] IPedidoAtualizarUseCase pedidoAtualizarUseCase, int id) =>
            {
                return Results.Ok(await pedidoAtualizarUseCase.ConfirmarPedido(id));
            });

            app.MapDelete("/cancelarpedido/{id}", async ([FromServices] IPedidoExcluirUseCase pedidoExcluirUseCase, string id) =>
            {
                return Results.Ok(await pedidoExcluirUseCase.CancelarPedido(id));
            });
        }
    }
}


