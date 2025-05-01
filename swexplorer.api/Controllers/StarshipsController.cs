using Microsoft.AspNetCore.Mvc;

namespace swexplorer.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StarshipsController : ControllerBase
{
    private readonly HttpClient _http;
    private readonly IConfiguration _configuration;

    public StarshipsController(HttpClient http, IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration), $"{nameof(configuration)} cannot be null.");
        _http = http ?? throw new ArgumentNullException(nameof(http), $"{nameof(http)} cannot be null.");
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // Get jwt settings from configuration.
        var swapiSettings = _configuration.GetSection("Swapi");
        var swapiEndpoint = swapiSettings["BaseUrl"];

        var response = await _http.GetAsync($"{swapiEndpoint}/starships");
        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, "Error fetching starships");

        var json = await response.Content.ReadAsStringAsync();
        return Content(json, "application/json");
    }
}