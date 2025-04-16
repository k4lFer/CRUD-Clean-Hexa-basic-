using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<TProduct>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<TProduct>> GetAllPaged(int pageNumber, int pageSize, string search, CancellationToken cancellationToken = default)
        {
            IQueryable<TProduct> query = _context.Products
                    .OrderBy(p => p.id);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                    p => EF.Functions.Like(p.name, $"%{search}%"));
            }
            return await GetPagedResultAsync(query, pageNumber, pageSize, cancellationToken);
        }

        public async Task<ICollection<TProduct>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Where(p => 
                ids.Contains(p.id))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task UpdateRankAsync(ICollection<TProduct> products, CancellationToken cancellationToken = default)
        {
            _context.Products.UpdateRange(products);
            return Task.CompletedTask;
        }
    }

}