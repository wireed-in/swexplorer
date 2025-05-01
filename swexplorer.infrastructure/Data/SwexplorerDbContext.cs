using swexplorer.core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace swexplorer.infrastructure.Data;

/// <summary>
/// Application database context using SQLite.
/// </summary>
public class SwexplorerDbContext : IdentityDbContext
{
    public SwexplorerDbContext(DbContextOptions<SwexplorerDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// <see cref="DbSet{StarshipEntity}"/>.
    /// </summary>
    public DbSet<StarshipEntity> Starships { get; set; }

    /// <summary>
    /// <see cref="DbSet{RaceEntity}"/>.
    /// </summary>
    public DbSet<RaceEntity> Races { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call base OnModelCreating for identity.
        base.OnModelCreating(modelBuilder);

        // This is a work around for how SQLite handles Guids. 
        // TODO: Remove if using SQL Server.
        modelBuilder.Entity<PlatformEntity>().Property(k => k.Id).HasConversion<string>();
    }
}