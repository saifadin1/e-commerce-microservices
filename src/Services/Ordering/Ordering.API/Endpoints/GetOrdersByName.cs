using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

// public record GetOrdersByNameRequest(string Name);
public record GetOrdersByNameResponse(IEnumerable<OrderDTO> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string GetOrdersByName, ISender sender) =>
        {
            var result = sender.Send(new GetOrdersByNameQuery(GetOrdersByName));
            var response = result.Adapt<GetOrdersByNameResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrdersByName");
    }
}