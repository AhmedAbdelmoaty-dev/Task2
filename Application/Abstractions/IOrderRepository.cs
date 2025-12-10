using Domain.Entites;

namespace Application.Abstractions
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);

        void DeleteOrder(Order order);

        Task<Order?> GetOrderByIdAsync(Guid orderId,CancellationToken cancellationToken);   

        Task<IReadOnlyList<Order>> GetAllOrdersAsync(CancellationToken cancellationToken);

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
