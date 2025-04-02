namespace Application.Interfaces.Services
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync(CancellationToken ct = default);
        Task CommitAsync(CancellationToken ct = default);
        Task RollbackAsync(CancellationToken ct = default);
        Task<bool> SaveChangesAsync(CancellationToken ct = default);
    }

}