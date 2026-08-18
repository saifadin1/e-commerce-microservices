using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.DTOs;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record  GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;

public record GetOrdersByCustomerResult(IEnumerable<OrderDTO> Orders);

public class GetOrdersByCustomerValidator : AbstractValidator<GetOrdersByCustomerQuery>
{
    public GetOrdersByCustomerValidator()
    {
        RuleFor(p => p.CustomerId).NotEmpty();
    }
}

