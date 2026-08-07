using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions;

public class InitialData
{
    // Fixed GUIDs for Customers
    public static readonly CustomerId Customer1Id = CustomerId.Of(new Guid("d0b1352e-6147-49f3-8bd2-6a682bc85bb1"));
    public static readonly CustomerId Customer2Id = CustomerId.Of(new Guid("e4c2f6d9-8134-4a25-9f1e-0a56291e7c42"));
    public static readonly CustomerId Customer3Id = CustomerId.Of(new Guid("a98d30e1-74f8-4395-b283-92f768e1423c"));

    // Fixed GUIDs for Products
    public static readonly ProductId Product1Id = ProductId.Of(new Guid("5334c996-82ae-43f2-9c16-86d646e6241e"));
    public static readonly ProductId Product2Id = ProductId.Of(new Guid("c670d79f-29c3-4217-bf41-698d3744e8c3"));
    public static readonly ProductId Product3Id = ProductId.Of(new Guid("4a8323e2-300e-4367-ae62-723653139049"));

    public static IEnumerable<Customer> Customers => new List<Customer>()
    {
        Customer.Create(Customer1Id, "Alice Smith", "alice.smith@example.com"),
        Customer.Create(Customer2Id, "Bob Jones", "bob.jones@example.com"),
        Customer.Create(Customer3Id, "Charlie Brown", "charlie.brown@example.com")
    };

    public static IEnumerable<Product> Products => new List<Product>()
    {
        Product.Create(Product1Id, "Wireless Mechanical Keyboard", 129.99m),
        Product.Create(Product2Id, "Ergonomic Ergonomic Mouse", 79.50m),
        Product.Create(Product3Id, "27-Inch 4K Monitor", 349.00m)
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            // Sample Order 1 (For Alice)
            var order1 = Order.Create(
                id: OrderId.Of(new Guid("7b938f21-3e42-411a-b620-1b1e9581c3d1")),
                customerId: Customer1Id,
                orderName: OrderName.Of("Alice's Desk Setup Order"),
                shippingAddress: Address.Of("123 Main St", "New York", "NY", "10001", "USA", "12345", "Apt 101"),
                billingAddress: Address.Of("123 Main St", "New York", "NY", "10001", "USA", "12345", "Apt 101"),
                payment: Payment.Of("Alice Smith", "4111222233334444", "12/28", "123", "1")
            );

            order1.Add(Product1Id, quantity: 1, price: 129.99m);
            order1.Add(Product2Id, quantity: 1, price: 79.50m);

            // Sample Order 2 (For Bob)
            var order2 = Order.Create(
                id: OrderId.Of(new Guid("1a62d04f-9e73-45c1-840a-93bd7820d20e")),
                customerId: Customer2Id,
                orderName: OrderName.Of("Bob's Monitor Order"),
                shippingAddress: Address.Of("123 Main St", "New York", "NY", "10001", "USA", "12345", "Apt 101"),
                billingAddress: Address.Of("123 Main St", "New York", "NY", "10001", "USA", "12345", "Apt 101"),
                payment: Payment.Of("Bob Jones", "5500000000000004", "09/27", "456", "2")
            );

            order2.Add(Product3Id, quantity: 2, price: 349.00m);

            return new List<Order> { order1, order2 };
        }
    }
}