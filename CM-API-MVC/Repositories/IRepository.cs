using System.Linq.Expressions;

namespace CM_API_MVC.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdWithIncludesAsync(object id, params Expression<Func<T, object>>[] includes);

    }
}
