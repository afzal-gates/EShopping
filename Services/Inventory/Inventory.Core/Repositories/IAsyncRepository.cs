using Inventory.Core.Entities;
using System.Linq.Expressions;

namespace Inventory.Core.Repositories;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
