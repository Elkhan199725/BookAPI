using System.Linq.Expressions;

namespace BookAPI.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "",
        int? page = null,
        int pageSize = 10);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
}