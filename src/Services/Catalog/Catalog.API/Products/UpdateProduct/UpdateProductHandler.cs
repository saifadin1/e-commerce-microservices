using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Mapster;
using Marten;

namespace Catalog.API.Products.UpdateProduct;


public record UpdateProductCommand(Guid Id, String Name, List<string> Category, string Description, string ImageUrl, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool isSuccess);


internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null) return new UpdateProductResult(false);

        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageUrl = command.ImageUrl;
        product.Category = command.Category;
        
        session.Update(product);
        
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}