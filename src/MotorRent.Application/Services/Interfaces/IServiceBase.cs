
using MotorRent.Domain.Entities;

namespace MotorRent.Application.Services.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        IQueryable<TEntity> Query();
        Task AddAsync(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
