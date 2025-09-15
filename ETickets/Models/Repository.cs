using ETickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private AppDbContext _context;
        private DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
             await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            T entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;
            if (options.HasOrderBy)
            {
                query = query.OrderBy(options.OrderBy);
            }
            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }
            foreach (var include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;
            if (options.HasOrderBy)
            {
                query = query.OrderBy(options.OrderBy);
            }
            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }
            foreach (var include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            var key = _context.Model
                .FindEntityType(typeof(T))?
                .FindPrimaryKey()?
                .Properties
                .FirstOrDefault();
            string primaryKeyName = key.Name;
            return await query.FirstOrDefaultAsync(entity => EF.Property<int>(entity, primaryKeyName) == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
