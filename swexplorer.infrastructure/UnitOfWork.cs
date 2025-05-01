using swexplorer.core.Interfaces;
using swexplorer.infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace swexplorer.infrastructure;

/// <summary>
/// Unit of work coordinates database transactions.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// DbContext for the unit of work.
    /// </summary>
    private readonly SwexplorerDbContext _dbContext;

    /// <summary>
    /// Service provider for accessing requested service.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Instantiates a new instance of <see cref="UnitOfWork"/>.
    /// </summary>
    /// <param name="dbContext">DbContext for the unit of work.</param>
    /// <param name="serviceProvider">Service provider.</param>
    public UnitOfWork(SwexplorerDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
    }

    /// <summary>Get a specific repository.</summary>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    /// <returns>Repository.</returns>
    public TRepository Repository<TRepository>()
    {
        // Return the most correct repository.
        return _serviceProvider.GetServices<TRepository>().FirstOrDefault() ??
            throw new InvalidOperationException($"No repository registered for this type '{typeof(TRepository).FullName}'.");
    }

    public void Save() => _dbContext.SaveChanges();

    public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

    public void Discard() => _dbContext.Dispose();

    public async Task DiscardAsync() => await _dbContext.DisposeAsync();

    public void Dispose() => _dbContext.Dispose();

    public void DisposeAsync() => _dbContext.DisposeAsync();
}