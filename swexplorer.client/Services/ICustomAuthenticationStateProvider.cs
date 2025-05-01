using Microsoft.AspNetCore.Components.Authorization;

namespace swexplorer.client.Services;

/// <summary>
/// Contract for Custom Authentication State Provider for managing user authentication state.
/// </summary>
public interface ICustomAuthenticationStateProvider
{
    /// <summary>
    /// Gets the current authentication state of the user.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<AuthenticationState> GetAuthenticationStateAsync();

    /// <summary>
    /// Notify the authentication state has changed for user login. Update stored jwt.
    /// </summary>
    /// <param name="jwt">The JWT jwt.</param>
    /// <param name="rememberMe">Flag to indeicate for remembering the user.</param>
    Task MarkAuthenticatedAsync(string jwt, bool rememberMe);

    /// <summary>
    /// Notify the authentication state has changed for user logout. Remove stored jwt.
    /// </summary>
    Task MarkNotAuthenticatedAsync();
}