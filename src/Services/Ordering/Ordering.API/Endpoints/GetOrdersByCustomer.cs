using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Domain.ValueObjects;

namespace Ordering.API.Endpoints;

// public record GetOrdersByCustomerRequest(string CustomerId);
public record GetOrdersByCustomerResponse(IEnumerable<OrderDTO> Orders);


public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid CustomerId, ISender sender) =>
            {
                var result = sender.Send(CustomerId);
                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByCustomer");
    }
}