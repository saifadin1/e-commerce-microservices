using Basket.API.Data;
using Basket.API.DTOs;
using BuildingBlocks.Messaging.Events;
using FluentValidation;
using Mapster;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record  CheckoutBasketCommand(BasketCheckoutDTO BasketCheckoutDTO) : ICommand<CheckoutBasketResult>;
public record  CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(e => e.BasketCheckoutDTO).NotNull().WithMessage("Basket Checkout DTO is required");
        RuleFor(e => e.BasketCheckoutDTO.UserName).NotEmpty().WithMessage("Basket Checkout Username is required");
    }
}


public class CheckoutBasketCommandHandler (
    IBasketRepository basketRepository,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(command.BasketCheckoutDTO.UserName, cancellationToken);
        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = command.BasketCheckoutDTO.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await basketRepository.DeleteBasket(command.BasketCheckoutDTO.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}