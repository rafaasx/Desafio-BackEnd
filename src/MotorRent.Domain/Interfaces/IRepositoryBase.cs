using System.Linq.Expressions;

namespace MotorRent.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();
        Task AddAsync(TEntity entity);
        Task<bool> ExistsAsync(string id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync();
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdAsync(string id);
        Task DeleteAsync(string id);
    }
}
