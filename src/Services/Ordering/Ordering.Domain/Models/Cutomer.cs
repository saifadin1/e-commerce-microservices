namespace Ordering.Domain.Models;

public class Cutomer : Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public static Cutomer Create(CustomerId id, string name, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);        
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        return new Cutomer
        {
            Name = name,
            Id = id,
            Email = email
        };
    }
}