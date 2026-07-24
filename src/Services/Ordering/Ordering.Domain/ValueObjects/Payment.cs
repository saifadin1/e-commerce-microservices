namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string Name { get; } = default!;
    public string Number { get; } = default!;
    public string Expiration { get; } = default;
    public string CVV { get; } = default;
    public string PaymentMethod { get; } = default;

    protected Payment()
    {
        
    }

    private Payment(string name, string number, string expiration, string cvv, string paymentMethod)
    {
        Name = name;
        Number = number;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string name, string number, string expiration, string cvv, string paymentMethod)
    {
        return new Payment(name, number, expiration, cvv, paymentMethod);
    }
}