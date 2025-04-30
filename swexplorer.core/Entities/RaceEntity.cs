namespace swexplorer.core.Entities;

/// <summary>
/// Entity for races between starships.
/// </summary>
public class RaceEntity : BaseEntity
{
    /// <summary>
    /// Foreign key for the <see cref="StarshipEntity"/>.
    /// </summary>
    public Guid WinnerId { get; set; }

    /// <summary>
    /// Navigation property for the <see cref="StarshipEntity"/>.
    /// </summary>
    public virtual StarshipEntity? Winner { get; set; }

    /// <summary>
    /// Navigation property for the list of participating <see cref="StarshipEntity"/>.
    /// </summary>
    public virtual ICollection<StarshipEntity> Starships { get; set; } = [];
}