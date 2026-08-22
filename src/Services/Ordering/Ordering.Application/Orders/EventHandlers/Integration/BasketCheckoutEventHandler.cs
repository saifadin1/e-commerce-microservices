using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) 
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDTO(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDTO(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);

        var orderId = Guid.NewGuid();
        var orderDto = new OrderDTO(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: OrderStatus.Pending,
            OrderItems:   // hardcoded for now 
            [
                new OrderItemDTO(orderId, new Guid("a3f5c8d2-9b1e-4f7a-8c3d-2e6b9a1f4d5c"), 2, 500),
                new OrderItemDTO(orderId, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479   "), 1, 600)
            ]
        );
        
        return new CreateOrderCommand(orderDto);
    }
}
