using Application.Abstractions;
using Application.Common;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Errors;
using Mapster;
using MediatR;

namespace Application.Orders.Queries.GetOrder
{
    public class GetOrderByIdQueryHandler(IOrderRepository _orderRepository
        ,ICacheService _cacheService)
        : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
    {
        public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery command, CancellationToken cancellationToken)
        {

            var cacheKey= CachingKeys.GetOrderKey(command.orderId);

            var cachedData = await _cacheService.GetAsync<OrderDto>(cacheKey);

            if (cachedData is not null)
                return Result<OrderDto>.Success(cachedData);

            var order= await _orderRepository.GetOrderByIdAsync(command.orderId,cancellationToken);

            if (order == null)
                return Result<OrderDto>.Failure(OrderErrors.NotFound);
           
           var orderDto= order.Adapt<OrderDto>();

           await _cacheService.SetAsync(cacheKey, orderDto,TimeSpan.FromMinutes(5),TimeSpan.FromMinutes(1));
            
            return Result<OrderDto>.Success(orderDto);
        }
    }
}
