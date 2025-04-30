using System.ComponentModel.DataAnnotations;

namespace swexplorer.core.Entities;

/// <summary>
/// Entity for a starship.
/// </summary>
public class StarshipEntity : BaseEntity
{
    /// <summary>
    /// Identifying name for the <see cref="StarshipEntity"/>.
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Name { get; set; } = string.Empty;
}