using Domain.Interfaces.Generic;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<TOrder>
    {
        Task<PagedResult<TOrder>> GetAllPaged(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<TOrder?> GetByIdWithDetailsAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<TOrder?> GetOrder(Guid orderId, CancellationToken cancellationToken = default);
    }
}