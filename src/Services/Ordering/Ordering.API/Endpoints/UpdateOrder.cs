using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDTO Order);
public record UpdateOrderResponse(bool isSuccess);

public class UpdateOrder : ICarterModule
{   
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var result = sender.Send(command);
                var response = result.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("Update order")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK);
    }
}