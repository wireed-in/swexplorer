using swexplorer.application.Dtos;
using swexplorer.application.Interfaces;
using swexplorer.application.Models;
using swexplorer.application.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace swexplorer.api.Controllers;

/// <summary>
/// Handles user authentication operations including registration, 
/// confirmation, login, and logout.
/// </summary>
/// <remarks>
/// The controller uses <see cref="AuthService"/> to perform operations such as 
/// creating users, authenticating users, and signing users in or out.
/// </remarks>
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">The authentication service that handles user registration and login.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService), $"{nameof(authService)} cannot be null.");
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="model">The registration details provided by the client.</param>
    /// <returns>An ApiResponse with the registration result.</returns>
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<object>>> Register([FromBody] RegisterModel model)
    {
        // Check model data.
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.ErrorResponse(["Invalid input data"], "User registration failed."));

        var response = await _authService.RegisterAsync(model);

        // If failed
        if (!response.Success)
        {
            return BadRequest(response);
        }

        // Success
        return Ok(response);
    }

    /// <summary>
    /// Gets user information.
    /// </summary>
    /// <param name="id">Unique identifier for the user.</param>
    /// <returns><see cref="ApiResponse{UserDto}"/> for the user.</returns>
    [HttpGet("users/{id}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetUser(string id)
    {
        var result = await _authService.GetUserByIdAsync(id);

        if (result == null)
            return NotFound(ApiResponse<UserDto>.ErrorResponse(["Failed to retreive user data."]));

        return Ok(ApiResponse<UserDto>.SuccessResponse(result));
    }

    /// <summary>
    /// Logs in an existing user and returns an authentication token.
    /// </summary>
    /// <param name="model">The login details provided by the client.</param>
    /// <returns>An ApiResponse with the login result.</returns>
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<object>>> Login([FromBody] LoginModel model)
    {
        // Check model data.
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.ErrorResponse(["Invalid input data"], "Login failed."));

        // Call the AuthService to handle login logic
        var response = await _authService.LoginAsync(model);

        if (response != null)
            return Ok(ApiResponse<AuthTokenDto>.SuccessResponse(response, "Login successful."));

        return Unauthorized(ApiResponse<object>.ErrorResponse(["Failed to authenticate user"], "Login failed."));
    }
}