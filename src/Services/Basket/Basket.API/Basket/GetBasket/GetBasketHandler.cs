                                    
using Basket.API.Data;
using Basket.API.Exceptions;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetTBasketResult>;
public record GetTBasketResult(ShoppingCart Cart);
    
public class GetBasketQueryHandler(IBasketRepository basketRepository):
    IQueryHandler<GetBasketQuery, GetTBasketResult>
{
    public async Task<GetTBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(query.UserName, cancellationToken);
        return new GetTBasketResult(basket);
    }
}