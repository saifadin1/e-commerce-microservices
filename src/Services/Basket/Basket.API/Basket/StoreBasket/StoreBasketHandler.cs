using Basket.API.Data;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(p => p.Cart).NotNull().WithMessage("Cart is Required");
        RuleFor(p => p.Cart.UserName).NotEmpty().WithMessage("UserName is Required");
    }
}

public class StoreBasketCommandHandler(IBasketRepository basketRepository) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Cart;
        await basketRepository.StoreBasket(cart, cancellationToken);
        //TODO : Update cache 
        return new StoreBasketResult(command.Cart.UserName);
    }
}