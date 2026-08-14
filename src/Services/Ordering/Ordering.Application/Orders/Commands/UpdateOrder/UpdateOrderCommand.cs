using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.DTOs;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDTO order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool isSuccess);

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(p => p.order.OrderName).NotEmpty().WithMessage("Order name is Required");
    }
}
