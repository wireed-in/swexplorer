using Microsoft.AspNetCore.Mvc;
using swexplorer.application.Interfaces;

namespace swexplorer.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StarshipsController : ControllerBase
{
    private readonly IStarshipService _starshipService;

    public StarshipsController(IStarshipService starshipService)
    {
        _starshipService = starshipService ?? throw new ArgumentNullException(nameof(starshipService), $"{nameof(starshipService)} cannot be null.");
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _starshipService.GetStarships();

        if (!result.Success)
            return NotFound(result);
        
        return Ok(result);
    }
}