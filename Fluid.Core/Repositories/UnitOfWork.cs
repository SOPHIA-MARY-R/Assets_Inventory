using Fluid.Core.Persistence;
using Fluid.Shared.Contracts;
using System.Collections;

namespace Fluid.Core.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;
    private bool _disposed;
    private Hashtable _repositories;

    public UnitOfWork(AppDbContext appDbContext) => _appDbContext = appDbContext;

    public IRepositoryAsync<T> GetRepository<T>() where T : class, IEntity
    {
        if (_repositories == null)
            _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryAsync<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _appDbContext);
            _repositories.Add(type, repositoryInstance);
        }

        return (IRepositoryAsync<T>)_repositories[type];
    }

    public async Task<int> Commit()
    {
        return await _appDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                //dispose managed resources
                _appDbContext.Dispose();
            }
        }
        //dispose unmanaged resources
        _disposed = true;
    }
}
