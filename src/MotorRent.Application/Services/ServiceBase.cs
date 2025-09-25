using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Application.Services
{
    public abstract class ServiceBase<TRepository, TEntity> : IServiceBase<TEntity> where TRepository :
        IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly TRepository Repository;
        protected ServiceBase(TRepository repository)
        {
            Repository = repository;
        }

        public Task AddAsync(TEntity entity) => Repository.AddAsync(entity);
        public Task<TEntity?> GetByIdAsync(string id) => Repository.GetByIdAsync(id);
        public virtual Task DeleteAsync(string id) => Repository.DeleteAsync(id);
        public IQueryable<TEntity> Query() => Repository.Query();
        public Task<int> SaveChangesAsync() => Repository.SaveChangesAsync();
        public Task<bool> IdExistsAsync(string id, CancellationToken token) => Repository.AnyAsync(a => a.Identificador == id, token);
    }
}
