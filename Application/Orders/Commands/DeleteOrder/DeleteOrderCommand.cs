using Domain.Abstractions;
using MediatR;

namespace Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid OrderId):IRequest<Result>;


}
