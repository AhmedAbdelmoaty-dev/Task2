using Application.Abstractions;
using Domain.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class OrderRepository (AppDbContext _context) : IOrderRepository
    {
        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<IReadOnlyList<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
           return await _context.Orders.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId,CancellationToken cancellationToken)
        {
           return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId,cancellationToken);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken) => 
            await _context.SaveChangesAsync(cancellationToken) > 0;

    }
}
