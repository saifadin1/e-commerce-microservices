using Basket.API.Data;
using Discount.Grpc;
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

public class StoreBasketCommandHandler
    (IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountClient) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);
        
        await basketRepository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }


    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
                var tasks = cart.Items.Select(item =>
                    discountClient.GetDiscountAsync(new GetDiscountRequest()).ResponseAsync);

                var coupons = await Task.WhenAll(tasks);

                for (int i = 0; i < coupons.Length; i++)
                {
                    cart.Items[i].Price -= coupons[i].Amount;
                }
    }
}