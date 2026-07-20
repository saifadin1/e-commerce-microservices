namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string Name { get; } = default!;
    public string Number { get; } = default!;
    public string Expiration { get; } = default;
    public string CVV { get; } = default;
    public string PaymentMethod { get; } = default;
}