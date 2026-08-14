namespace Ordering.Application.DTOs;

public record PaymentDTO(string Name, string Number, string Expiration, string Cvv, int PaymentMethod);
