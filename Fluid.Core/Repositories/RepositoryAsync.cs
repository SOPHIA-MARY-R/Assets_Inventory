using Fluid.Core.Persistence;
using Fluid.Shared.Contracts;

namespace Fluid.Core.Repositories;

public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class, IEntity
{
    private readonly AppDbContext _appDbContext;

    public RepositoryAsync(AppDbContext appDbContext) => _appDbContext = appDbContext;

    public IQueryable<T> Entities => _appDbContext.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        await _appDbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _appDbContext.Set<T>().CountAsync(predicate);
    }

    public async Task<int> CountAsync()
    {
        return await _appDbContext.Set<T>().CountAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _appDbContext.Set<T>().Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _appDbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync<TId>(TId id)
    {
        return await _appDbContext.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
    {
        return await _appDbContext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
    }

    public async Task UpdateAsync<TId>(T updatedEntity, TId id)
    {
        T old = await _appDbContext.Set<T>().FindAsync(id);
        _appDbContext.Entry(old).CurrentValues.SetValues(updatedEntity);
        await Task.CompletedTask;
    }
}
