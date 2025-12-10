using Application.Abstractions;
using Application.Common;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entites;
using Mapster;
using MediatR;

namespace Application.Orders.Queries.ListOrders
{
    internal class GetOrdersQueryHandler(IOrderRepository _orderRepository,ICacheService _cacheService)
        : IRequestHandler<GetOrdersQuery, Result<IReadOnlyList<OrderDto>>>
    {
        public async Task<Result<IReadOnlyList<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = CachingKeys.OrdersListKey;
            
            var cachedData=  await _cacheService.GetAsync<IReadOnlyList<OrderDto>>(cacheKey);

            if(cachedData is not null)
                return Result<IReadOnlyList<OrderDto>>.Success(cachedData);

            var orders= await  _orderRepository.GetAllOrdersAsync(cancellationToken);

            var  ordersDtos= orders.Adapt<IReadOnlyList<OrderDto>>();

            await _cacheService.SetAsync(cacheKey, ordersDtos,TimeSpan.FromMinutes(5),TimeSpan.FromMinutes(1));

            return Result<IReadOnlyList<OrderDto>>.Success(ordersDtos);
        }
    }
}
