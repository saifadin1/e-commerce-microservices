namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    const int DEFAULT_LENGTH = 5;
    public string Value { get; }

    OrderName(string value)
    {
        Value = value;
    }

    public static OrderName Of(string value)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DEFAULT_LENGTH);

        return new OrderName(value);
    }
}