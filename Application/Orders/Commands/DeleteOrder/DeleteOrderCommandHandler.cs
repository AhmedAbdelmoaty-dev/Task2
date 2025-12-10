using Application.Abstractions;
using Application.Common;
using Domain.Abstractions;
using Domain.Errors;
using MediatR;

namespace Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IOrderRepository _orderRepository,ICacheService _cacheService)
        : IRequestHandler<DeleteOrderCommand, Result>
    {
        public async Task<Result> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {

          var order=  await _orderRepository.GetOrderByIdAsync(command.OrderId,cancellationToken);

            if (order is null)
                return Result.Failure(OrderErrors.NotFound);
            
            _orderRepository.DeleteOrder(order);

            var isDeleted = await _orderRepository.SaveChangesAsync(cancellationToken);
            
            if (!isDeleted)
                return Result<Guid>.Failure(Error.Presistance);

           await  _cacheService.DeleteAsync(CachingKeys.OrdersListKey);
           await _cacheService.DeleteAsync(CachingKeys.GetOrderKey(command.OrderId));
           
            return Result.Success();

        }
    }
}
