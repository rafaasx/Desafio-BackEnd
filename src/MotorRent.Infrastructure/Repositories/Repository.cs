using Microsoft.EntityFrameworkCore;
using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;
using System.Linq.Expressions;

namespace MotorRent.Infrastructure.Repositories
{
    public class Repository<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> Query() => _dbSet.AsQueryable();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<bool> ExistsAsync(string id) => await _dbSet.AnyAsync(a => a.Identifier == id);
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) => await _dbSet.AnyAsync(predicate, cancellationToken);
    }
}
