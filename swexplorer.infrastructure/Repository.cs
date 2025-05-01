using Microsoft.EntityFrameworkCore;
using swexplorer.core.Interfaces;
using swexplorer.core.Entities;
using System.Linq.Expressions;
using swexplorer.infrastructure.Data;

namespace swexplorer.infrastructure;

/// <summary>
/// Generic repository for all entities.
/// </summary>
/// <typeparam name="TEntity">Type of entity for the repository.</typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// DbContext associated with the repository.
    /// </summary>
    private readonly SwexplorerDbContext _dbContext;

    /// <summary>
    /// Representation for all entities that can be queried from the database.
    /// </summary>
    public DbSet<TEntity> DbSet { get; }

    /// <summary>
    /// Instantiates a new instance of <see cref="Repository{TEntity}"/>.
    /// </summary>
    /// <param name="dbContext">Database context.</param>
    public Repository(SwexplorerDbContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = _dbContext.Set<TEntity>();
    }

    public async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
            query = query.Where(filter);

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, string[]? includeProperties = null)
    {
        // If there are no included properties then find and return the entity.
        if (includeProperties == null || includeProperties.Length == 0)
            return await DbSet.FindAsync(id);

        // Initialize query.
        IQueryable<TEntity> query = DbSet;

        // Add every include property to the query.
        foreach(var includeProperty in includeProperties)
        {
            if (!string.IsNullOrEmpty(includeProperty))
                query = query.Include(includeProperty);
        }

        // Run the query and return result.
        return await query.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(TEntity entity) => await _dbContext.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await _dbContext.AddRangeAsync(entities);
}