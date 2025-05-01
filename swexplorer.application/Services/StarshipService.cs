using swexplorer.application.Interfaces;
using swexplorer.application.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace swexplorer.application.Services;

/// <summary>
/// Service responsible for starship operations.
/// </summary>
public class StarshipService : IStarshipService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _configuration;

    public StarshipService(HttpClient http, IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration), $"{nameof(configuration)} cannot be null.");
        _http = http ?? throw new ArgumentNullException(nameof(http), $"{nameof(http)} cannot be null.");
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<IEnumerable<StarshipDto>>> GetStarships()
    {
        // Get api settings from configuration.
        var swapiSettings = _configuration.GetSection("Swapi");
        var swapiEndpoint = swapiSettings["BaseUrl"];
        
        var response = await _http.GetAsync($"{swapiEndpoint}/starships");

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return ApiResponse<IEnumerable<StarshipDto>>.ErrorResponse(null, "Error fetching starships");
        }

        var data = await response.Content.ReadFromJsonAsync<IEnumerable<StarshipDto>>();
        return ApiResponse<IEnumerable<StarshipDto>>.SuccessResponse(data);
    }
}