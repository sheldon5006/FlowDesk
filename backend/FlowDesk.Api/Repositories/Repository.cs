using FlowDesk.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Repositories
{
    public class Repository<T> : IRepository<T>
    where T : class
    {
        private readonly FlowDeskDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(FlowDeskDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> Query()
        {
            return _dbSet;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
