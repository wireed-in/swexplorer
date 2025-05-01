namespace swexplorer.core.Interfaces;

/// <summary>
/// Unit of work coordinates database transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Get a generic repository for a given entity.
    /// </summary>
    /// <typeparam name="TRepository">Generic repository with a type of entity.</typeparam>
    /// <returns>Repository for an entity.</returns>
    TRepository Repository<TRepository>();

    /// <summary>
    /// Save changes to the database.
    /// </summary>
    void Save();

    /// <summary>
    /// Discard all changes and call Dispose().
    /// </summary>
    void Discard();

    /// <summary>
    /// Save all changes to the database asynchronously.
    /// </summary>
    /// <returns><see cref="Task"/>.</returns>
    Task SaveAsync();

    /// <summary>
    /// Discard all changes and dispose asynchronously.
    /// </summary>
    /// <returns><see cref="Task"/>.</returns>
    Task DiscardAsync();
}