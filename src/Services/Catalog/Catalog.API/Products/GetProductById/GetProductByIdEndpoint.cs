using Carter;
using Catalog.API.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Products.GetProductById;

// public record GetProductByIdRequest(Guid Id);

public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var Response = result.Adapt<GetProductByIdResponse>();

            if (Response.Product == null)
            {
                return Results.NotFound();
            }
            
            return Results.Ok(Response);
        })
            .WithName("GetProductById"); 
    }
}