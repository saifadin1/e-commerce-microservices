using Basket.API.Data;
using FluentValidation;
using Marten;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string username) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool isSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(p => p.username).NotEmpty().WithMessage("username is Required");
    }
}
public class DeleteBasketCommandHandler(IBasketRepository basketRepository) 
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        // TODO : delete basket from db and cache
        await basketRepository.DeleteBasket(command.username, cancellationToken);
        return new DeleteBasketResult(true);
    }
}