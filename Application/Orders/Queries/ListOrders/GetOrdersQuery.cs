using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Orders.Queries.ListOrders
{
    public record GetOrdersQuery:IRequest<Result<IReadOnlyList<OrderDto>>>;


}
