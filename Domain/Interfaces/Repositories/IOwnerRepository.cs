using Domain.Interfaces.Generic;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces.Repositories
{
    public interface IOwnerRepository : IGenericRepository<TOwner> 
    { 
        Task<TOwner>? GetByMail(string mail, CancellationToken cancellationToken = default);
        Task<TOwner>? GetByUsername(string username, CancellationToken cancellationToken = default);
        Task<TOwner>? GetByRuc(string ruc, CancellationToken cancellationToken = default);
        Task<TOwner>? GetByDni(string dni, CancellationToken cancellationToken = default);
        Task<PagedResult<TOwner>> GetAllPaged(int pageNumber, int pageSize, string search, Guid ownerId, CancellationToken cancellationToken = default);

    }
}