using swexplorer.application.Dtos;
using swexplorer.application.Models;
using swexplorer.application.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace swexplorer.application.Interfaces;

/// <summary>
/// Interface for handling user authentication-related operations such as registration and login.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user, generates an authentication token and sends email.
    /// </summary>
    /// <param name="model">The registration details provided by the client.</param>
    /// <returns>An <see cref="ApiResponse{string}"/> containing the result of the registration process.</returns>
    Task<ApiResponse<string>> RegisterAsync(RegisterModel model);

    /// <summary>
    /// Gets the user information using userId.
    /// </summary>
    /// <param name="usedId">Unique identifier for the user.</param>
    /// <returns><see cref="UserDto"/> with user information.</returns>
    Task<UserDto?> GetUserByIdAsync(string usedId);

    /// <summary>
    /// Logs in an existing user and generates an authentication token.
    /// </summary>
    /// <param name="model">The login details (e.g., email and password) provided by the client.</param>
    /// <returns>Token containing the result of the login process.</returns>
    Task<AuthTokenDto?> LoginAsync(LoginModel model);
}