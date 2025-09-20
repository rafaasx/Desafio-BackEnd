using System.Linq.Expressions;

namespace MotorRent.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> Query();
        Task AddAsync(T entity);
        Task<bool> ExistsAsync(string id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync();
    }
}
