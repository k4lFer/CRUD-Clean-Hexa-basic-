using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<TCustomer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<TCustomer>> GetAllPaged(int pageNumber, int pageSize, string? search, 
        CancellationToken cancellationToken = default)
        {
            IQueryable<TCustomer> query = _context.Customers
                    .OrderBy(c => c.id);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                    c => EF.Functions.Like(c.firstName, $"%{search}%") 
                        || EF.Functions.Like(c.lastName, $"%{search}%"));
            }

            return await GetPagedResultAsync(query, pageNumber, pageSize, cancellationToken);
        }

        public async Task<bool> IsDocNumberUniqueAsync(string dni, CancellationToken cancellationToken = default)
        {
            return !(await _context.Customers
                .AsNoTracking()
                .AnyAsync(c => c.documentNumber == dni, cancellationToken));
        }

        public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
        {
            return !(await _context.Customers
                .AsNoTracking()
                .AnyAsync(c => c.email == email, cancellationToken));
        }
    }
}
