using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using FluentValidation;
using Mapster;
using Marten;

namespace Catalog.API.Products.UpdateProduct;


public record UpdateProductCommand(Guid Id, String Name, List<string> Category, string Description, string ImageUrl, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool isSuccess);

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name should not be empty");
        RuleFor(p => p.Category).NotEmpty().WithMessage("Category is Required");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
    }
}

internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

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