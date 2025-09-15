namespace Restaurant.Models
{
    public interface IRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllsync();
        Task<T> GetByIDAsync(int id, QueryOptions<T> options);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
