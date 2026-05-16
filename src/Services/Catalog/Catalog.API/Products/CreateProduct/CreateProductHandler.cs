using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using Mapster;
using Marten;
using MediatR;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    List<string> Category) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);


public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name should not be empty");
        RuleFor(p => p.Category).NotEmpty().WithMessage("Category is Required");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
    }
}


public class CreateProductHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        
        var product = new Product()
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            ImageUrl = command.ImageUrl,
            Category = command.Category
        };
        


        // var product = command.Adapt<Product>();
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(Guid.NewGuid());
    }
}