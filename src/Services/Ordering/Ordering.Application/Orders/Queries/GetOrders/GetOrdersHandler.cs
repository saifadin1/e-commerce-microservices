using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext context) 
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var totalCount = await context.Orders.CountAsync(cancellationToken);
        
        var orders = await context.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Skip(query.paginationRequest.pageIndex - 1 * query.paginationRequest.pageSize)
            .Take(query.paginationRequest.pageSize)
            .ToListAsync(cancellationToken);

        return new GetOrdersResult(
                new PaginatedResult<OrderDTO>(query.paginationRequest.pageIndex,
                    query.paginationRequest.pageSize,
                    totalCount,
                    orders.ToOrderDtoList()
                    )
            );
    }
}