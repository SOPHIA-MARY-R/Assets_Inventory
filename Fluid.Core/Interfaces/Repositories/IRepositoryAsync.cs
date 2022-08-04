using Fluid.Shared.Contracts;

namespace Fluid.Core.Interfaces.Repositories;

public interface IRepositoryAsync<T> where T : class, IEntity
{
    IQueryable<T> Entities { get; }

    Task<T> AddAsync(T entity);

    Task<int> CountAsync();

    Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    Task DeleteAsync(T entity);

    Task<List<T>> GetAllAsync();

    Task<T> GetByIdAsync<TId>(TId id);

    Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

    Task UpdateAsync<TId>(T updatedEntity, TId id);
}
