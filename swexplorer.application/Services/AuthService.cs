using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using swexplorer.application.Dtos;
using swexplorer.application.Interfaces;
using swexplorer.application.Models;
using swexplorer.application.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace swexplorer.application.Services;

/// <summary>
/// Service for handling user authentication operations such as registration, login, and token generation.
/// Implements the <see cref="IAuthService"/> interface.
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{IdentityUser}"/> service for managing users.</param>
    /// <param name="signInManager">The <see cref="SignInManager{IdentityUser}"/> service for user sign-in functionality.</param>
    public AuthService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager), $"{nameof(userManager)} cannot be null.");
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager), $"{nameof(signInManager)} cannot be null.");
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration), $"{nameof(configuration)} cannot be null.");
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory), $"{nameof(httpClientFactory)} cannot be null.");
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<string>> RegisterAsync(RegisterModel model)
    {
        // Call the AuthService to handle registration logic.
        var result = await CreateUserAsync(model);

        // Check if successful and respond with errors if not.
        if (!result.Succeeded)
            return ApiResponse<string>.ErrorResponse(result.Errors.Select(x => x.Description), "User registration failed.");

        // Registration complete. Send success data.
        return ApiResponse<string>.SuccessResponse(model.Email, "User is registered successfully.");
    }

    /// <inheritdoc/>
    public async Task<UserDto?> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
            return new UserDto { UserName = user.UserName ?? "", Email = user.Email ?? "" };

        return null;
    }

    /// <inheritdoc/>
    public async Task<AuthTokenDto?> LoginAsync(LoginModel model)
    {
        // Check if the user exists with the provided email
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
            return null;

        // Attempt to sign in the user with the provided password
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (!result.Succeeded)
            return null;

        // Setup claims.
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        var expiresAt = DateTime.UtcNow.AddMinutes(15);

        return new AuthTokenDto
        {
            ExpiresAt = expiresAt,
            Token = CreateToken(claims, expiresAt)
        };
    }

    /// <summary>
    /// Helper method to create JWT Token with list of claims and expiration date..
    /// </summary>
    /// <param name="claims">List of provided claims</param>
    /// <param name="expiresAt">Expiration date-time.</param>
    /// <returns></returns>
    private string CreateToken(List<Claim> claims, DateTime expiresAt)
    {
        // Get jwt settings from configuration.
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? "");

        // Setup JWT.
        var jwt = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="model">The registration details provided by the client.</param>
    /// <returns>An <see cref="IdentityResult"/> containing the result of the registration process.</returns>
    private async Task<IdentityResult> CreateUserAsync(RegisterModel model)
    {
        // Create a new user from the provided registration model
        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        // Attempt to create the user with the provided password
        var result = await _userManager.CreateAsync(user, model.Password);

        return result;
    }
}