using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.DTOs;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDTO Order) : ICommand<CreateOrderResult>;


public record CreateOrderResult(Guid Id);


public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(p => p.Order.OrderName).NotEmpty().WithMessage("Order name is Required");
    }
    
}