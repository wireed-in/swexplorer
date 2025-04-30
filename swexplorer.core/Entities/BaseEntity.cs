namespace swexplorer.core.Entities;

/// <summary>
/// Base entity for all other entities to derive from.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Record created date-time.
    /// </summary>
    public DateTime CreatedDateTime { get; set; }
}