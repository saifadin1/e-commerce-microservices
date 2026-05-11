using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.DeleteProduct;

// public record DeleteProductRequest(Guid Id);
public record DeleteProductResponse(bool isSuccess);


public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            var response = result.Adapt<DeleteProductResponse>();
            
            return response.isSuccess ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteProduct")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
    }
}