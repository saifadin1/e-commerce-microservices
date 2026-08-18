using Ordering.Application.DTOs;
using Ordering.Domain.Models;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDTO> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => new OrderDTO(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            ShippingAddress: new AddressDTO(
                FirstName: order.ShippingAddress.FirstName,
                LastName: order.ShippingAddress.LastName,
                EmailAddress: order.ShippingAddress.EmailAddress ?? string.Empty,
                AddressLine: order.ShippingAddress.AddressLine,
                Country: order.ShippingAddress.Country,
                State: order.ShippingAddress.State,
                ZipCode: order.ShippingAddress.ZipCode
            ),
            BillingAddress: new AddressDTO(
                FirstName: order.BillingAddress.FirstName,
                LastName: order.BillingAddress.LastName,
                EmailAddress: order.BillingAddress.EmailAddress ?? string.Empty,
                AddressLine: order.BillingAddress.AddressLine,
                Country: order.BillingAddress.Country,
                State: order.BillingAddress.State,
                ZipCode: order.BillingAddress.ZipCode
            ),
            Payment: new PaymentDTO(
                Name: order.Payment.Name,
                Number: order.Payment.Number,
                Expiration: order.Payment.Expiration,
                Cvv: order.Payment.CVV,
                PaymentMethod: order.Payment.PaymentMethod
            ),
            Status: order.Status,
            OrderItems: order.OrderItems.Select(oi => new OrderItemDTO(
                OrderId: oi.OrderId.Value,
                ProductId: oi.ProductId.Value,
                Quantity: oi.Quantity,
                Price: oi.Price
            )).ToList()
        ));
    }
}