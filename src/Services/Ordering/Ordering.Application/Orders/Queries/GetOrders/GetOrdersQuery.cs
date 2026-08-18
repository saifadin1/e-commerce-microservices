using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Ordering.Application.DTOs;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest paginationRequest) : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDTO> Orders);