using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using Marten.Linq.QueryHandlers;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;


public record GetProductsQuery(int pageNumber = 1, int pageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);


internal class GetProductsQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(query.pageNumber, query.pageSize, cancellationToken);
        
        return new GetProductsResult(products);
    }
}