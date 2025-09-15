namespace ETickets.Models
{
    public interface IRepository<T> where T : class
    {
         Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options);
         Task<T> GetByIdAsync(int id, QueryOptions<T> options);
         Task DeleteByIdAsync(int id);
         Task UpdateAsync(T entity);
         Task AddAsync(T entity);
    }
}
