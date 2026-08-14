using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext context) 
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.order.Id);
        var order = await context.Orders.FindAsync([command.order.Id], cancellationToken: cancellationToken);
        if (order == null)
        {
            throw new OrderNotFoundException(command.order.Id);
        }
        
        updateOrderWithNewValues(order, command.order);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return new UpdateOrderResult(true);
    }


    void updateOrderWithNewValues(Order order, OrderDTO orderDTO)
    {
        
        
        var shippingAddress = Address.Of(orderDTO.ShippingAddress.FirstName, orderDTO.ShippingAddress.LastName, 
            orderDTO.ShippingAddress.EmailAddress, orderDTO.ShippingAddress.AddressLine, 
            orderDTO.ShippingAddress.Country, orderDTO.ShippingAddress.State, orderDTO.ShippingAddress.ZipCode);

        var billingAddress = Address.Of(orderDTO.BillingAddress.FirstName, orderDTO.BillingAddress.LastName, 
            orderDTO.BillingAddress.EmailAddress, orderDTO.BillingAddress.AddressLine, 
            orderDTO.BillingAddress.Country, orderDTO.BillingAddress.State, orderDTO.BillingAddress.ZipCode);

        var payment = Payment.Of(orderDTO.Payment.Name, orderDTO.Payment.Number, 
            orderDTO.Payment.Expiration, orderDTO.Payment.Cvv, orderDTO.Payment.PaymentMethod);
        
        order.Update(OrderName.Of(orderDTO.OrderName), shippingAddress, billingAddress, payment, orderDTO.Status);
    }
}