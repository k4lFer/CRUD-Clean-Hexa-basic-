using Domain.Interfaces.Generic;
using Domain.Entities;
using System.Collections.ObjectModel;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderDetailRepository : IGenericRepository<TOrderDetail>
    {
        Task CreateRangeAsync(IEnumerable<TOrderDetail> orderDetails, CancellationToken cancellationToken = default);
        Task<ICollection<TOrderDetail>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}