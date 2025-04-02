using Domain.Interfaces.Generic;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IGenericRepository<TCustomer>
    {
        Task<PagedResult<TCustomer>> GetAllPaged(int pageNumber, int pageSize, string search, CancellationToken cancellationToken = default);
        Task<bool> IsDocNumberUniqueAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsEmailUniqueAsync(string dnil, CancellationToken cancellationToken = default);
    }
}
