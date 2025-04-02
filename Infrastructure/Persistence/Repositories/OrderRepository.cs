using Application.DTOs.Common;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<TOrder>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<TOrder>> GetAllPaged(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            IQueryable<TOrder> query = _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .OrderBy(o => o.id);

            return await GetPagedResultAsync(query, pageNumber, pageSize, cancellationToken);
        }

        public Task<TOrder?> GetByIdWithDetailsAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderDetails)
                    //.ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.id == orderId, cancellationToken);
        }

        public Task<TOrder?> GetOrder(Guid orderId, CancellationToken cancellationToken = default)
        {
            return _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.id == orderId, cancellationToken);
        }
    }
}