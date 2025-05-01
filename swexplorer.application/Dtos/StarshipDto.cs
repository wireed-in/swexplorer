using System.Text.Json.Serialization;

/// <summary>
/// Contains starship related data
/// </summary>
public class StarshipDto
{
    /// <summary>
    /// Starship name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Starship mdoel.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Starship manufacturer.
    /// </summary>
    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// Max atmospheric speed important for travel in earth like atmosphere. Higher the better.
    /// </summary>
    [JsonPropertyName("max_atmosphering_speed")]
    public string MaxAtmospheringSpeed { get; set; }

    /// <summary>
    /// Megalight per hour. Good for space maneuverability. Higher the better.
    /// </summary>
    public string MGLT { get; set; }

    /// <summary>
    /// Important for hyperspace and long distance travel. Lower the better.
    /// </summary>
    [JsonPropertyName("hyperdrive_rating")]
    public string HyperdriveRating { get; set; }

    /// <summary>
    /// Indicates if starship is selected for comparison.
    /// </summary>
    public bool IsSelected { get; set; }
}