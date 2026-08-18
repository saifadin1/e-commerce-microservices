using BuildingBlocks.Pagination;
using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDTO> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParametersAttribute] PaginationRequest request, ISender sender) =>
            {
                var result = sender.Send(new GetOrdersQuery(request));
                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrders");
    }
}