using Domain.Interfaces.Generic;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<TProduct> 
    { 
        Task<ICollection<TProduct>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default);
        Task UpdateRankAsync(ICollection<TProduct> products, CancellationToken cancellationToken = default);
        Task<PagedResult<TProduct>> GetAllPaged(int pageNumber, int pageSize, string search, CancellationToken cancellationToken = default);
    }
}