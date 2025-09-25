using Microsoft.EntityFrameworkCore;
using MotorRent.Domain.Constants;
using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;
using System.Linq.Expressions;

namespace MotorRent.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>() ?? throw new InvalidOperationException($"DbSet for type {typeof(TEntity).Name} could not be initialized.");
        }

        public IQueryable<TEntity> Query() => _dbSet.AsQueryable();

        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
            _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<bool> ExistsAsync(string id) => await _dbSet.AnyAsync(a => a.Identificador == id);
        public async Task<TEntity?> GetByIdAsync(string id) => await _dbSet.FirstOrDefaultAsync(a => a.Identificador == id);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
            await _dbSet.AnyAsync(predicate, cancellationToken);

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException(string.Format(Messages.EntityNotFound, typeof(TEntity).Name, id));
            _dbSet.Remove(entity);
        }
    }
}
