using Fluid.Core.Persistence;
using Fluid.Shared.Contracts;

namespace Fluid.Core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    AppDbContext AppDbContext { get; }
    IRepositoryAsync<T> GetRepository<T>() where T : class, IEntity;
    Task<int> Commit();
}
