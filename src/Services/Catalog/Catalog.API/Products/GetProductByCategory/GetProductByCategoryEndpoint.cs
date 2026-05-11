using Carter;
using Catalog.API.Models;
using Mapster;
using Marten;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory;


// public record GetProductByCategoryRequest(string Category);
public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));
                var response = result.Adapt<GetProductByCategoryResponse>();
                if (response.Products is null || response.Products.IsEmpty())
                {
                    return Results.NotFound();
                }
                return Results.Ok(response);
            })
            .WithName("GetProductByCategory");
    }
}