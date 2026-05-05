using System.Linq.Expressions;

namespace NEEFRA.Domain.IReposatory
{
    public interface IReposatory<T> where T : class
    {
        Task CreateAsync(T model);
        Task DeleteAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            int pageSize = 0,
            int pageNumber = 1);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task UpdateAsync(Expression<Func<T, bool>> filter, T entity);


    }
}
