using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Infrastructure.Persistence.Repositories
{
    public class OrderDetailRepository : GenericRepository<TOrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateRangeAsync(IEnumerable<TOrderDetail> orderDetails, CancellationToken cancellationToken = default)
        {
            var orderDetailList = orderDetails.ToList();
            await _context.OrderDetails.AddRangeAsync(orderDetailList, cancellationToken);
        }

        public async Task<ICollection<TOrderDetail>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _context.OrderDetails.Where(d => d.orderId == orderId).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}