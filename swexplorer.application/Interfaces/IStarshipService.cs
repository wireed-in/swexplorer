using swexplorer.application.Models;

namespace swexplorer.application.Interfaces;

/// <summary>
/// Contract for starship operations.
/// </summary>
public interface IStarshipService
{
    /// <summary>
    /// Gets all starships from the api and returns results.
    /// </summary>
    /// <returns>An <see cref="ApiResponse{IEnumerable{StarshipDto}}"/> containing list of starships.</returns>
    Task<ApiResponse<IEnumerable<StarshipDto>>> GetStarships();
}