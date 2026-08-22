using Basket.API.DTOs;
using Mapster;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDTO BasketCheckoutDTO);
public record CheckoutBasketResponse(bool IsSuccess);
public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckoutBasketCommand>();
            var result = sender.Send(command);
            var respons = result.Adapt<CheckoutBasketResponse>();
            
            return Results.Ok(respons);
        });
    }
}