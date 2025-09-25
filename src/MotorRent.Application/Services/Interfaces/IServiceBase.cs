
using MotorRent.Domain.Entities;

namespace MotorRent.Application.Services.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        IQueryable<TEntity> Query();
        Task AddAsync(TEntity entity);
        Task<int> SaveChangesAsync();
        Task<TEntity?> GetByIdAsync(string id);
        Task<bool> IdExistsAsync(string id, CancellationToken token = default);
        Task DeleteAsync(string id);
    }
}
