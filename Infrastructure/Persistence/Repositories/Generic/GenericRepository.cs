using Domain.Interfaces.Generic;
using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class,  IEntity
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context ;
        }


        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
           // return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "id") == id, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        { 
            _context.Set<T>().Update(entity);
            //return Task.CompletedTask;
        }
        
        protected async Task<PagedResult<T>> GetPagedResultAsync<T>(
            IQueryable<T> query, 
            int pageNumber, 
            int pageSize, 
            CancellationToken cancellationToken = default) 
            where T : class
        {
            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
            // return new PagedResult<TEntity>(pagedResult, count, pageNumber, pageSize);
        }
    }
}