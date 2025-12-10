using Application.Abstractions;
using Application.Common;
using Domain.Abstractions;
using Domain.Entites;
using Domain.Errors;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IOrderRepository _orderRepository
        ,ICacheService _cacheService,IValidator<CreateOrderCommand> _validator,ILogger<CreateOrderCommandHandler> _logger)
        : IRequestHandler<CreateOrderCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
           var validationResult=  await  _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                
                var errors = validationResult.Errors.Select(x=>x.ErrorMessage).ToList();

                var message = string.Join(",", errors);

                return Result<Guid>.Failure(Error.Validation(message));
            }

            var order = command.Order.Adapt<Order>();

            var cacheKey = CachingKeys.OrdersListKey;

            order.CreatedAt= DateTime.UtcNow;
            
            order.OrderId=Guid.NewGuid();

            _orderRepository.CreateOrder(order);

           var isPersisted= await _orderRepository.SaveChangesAsync(cancellationToken);

            if (!isPersisted)
            {
                _logger.LogWarning("Failed to persist order for customer {CustomerName}", command.Order.CustomerName);
                return Result<Guid>.Failure(Error.Presistance);
            }

           await _cacheService.DeleteAsync(cacheKey);

            return Result<Guid>.Success(order.OrderId);
        }
    }
}
