using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repositories
{
    public class OwnerRepository : GenericRepository<TOwner>, IOwnerRepository
    {
        public OwnerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<TOwner>> GetAllPaged(int pageNumber, int pageSize, string search, Guid ownerId, CancellationToken cancellationToken = default)
        {
            IQueryable<TOwner> query = _context.Owners
                .Where(o => o.id != ownerId)
                .OrderBy(o => o.id);
                
            // Filtrar por nombre, apellido o Dni o Ruc, pero ruc es un campo opcional, puede ser null
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o => o.firstName.Contains(search) 
                    || o.lastName.Contains(search) 
                    || o.dni.Contains(search)   
                    || o.ruc.Contains(search));
            }
            return await GetPagedResultAsync(query, pageNumber, pageSize, cancellationToken);
        }

        public async Task<TOwner>? GetByDni(string dni, CancellationToken cancellationToken = default)
        {
            return await _context.Owners.AsNoTracking().FirstOrDefaultAsync(o => o.dni == dni, cancellationToken);
        }

        public async Task<TOwner>? GetByMail(string mail, CancellationToken cancellationToken = default)
        {
          
            return await _context.Owners.AsNoTracking().FirstOrDefaultAsync(o => o.email == mail, cancellationToken);
        }

        public async Task<TOwner>? GetByRuc(string ruc, CancellationToken cancellationToken = default)
        {
            return await _context.Owners.AsNoTracking().FirstOrDefaultAsync(o => o.ruc == ruc, cancellationToken);
        }

        public async Task<TOwner>? GetByUsername(string username, CancellationToken cancellationToken = default)
        {
            return await _context.Owners.AsNoTracking().FirstOrDefaultAsync(o => o.username == username, cancellationToken);
        }
    }
}