using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Orders.Queries.GetOrder
{
    public record GetOrderByIdQuery(Guid orderId)   
        : IRequest<Result<OrderDto>>;

}
