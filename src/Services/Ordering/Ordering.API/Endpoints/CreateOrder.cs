using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDTO Order);
public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/order", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOrderCommand>();
            var result = await sender.Send(command);
            var repsponse = result.Adapt<CreateOrderResponse>();
            
            return Results.Created($"/orders/{result.Id}", repsponse);
        });
    }
}