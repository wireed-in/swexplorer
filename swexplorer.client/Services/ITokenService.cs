namespace swexplorer.client.Services;

/// <summary>
/// Contract for a service for handling token storage in local/session storage.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Store token in local or session storage based on user choice.
    /// </summary>
    /// <param name="token">JWT token.</param>
    /// <param name="rememberMe">Indicates whether to remember the user.</param>
    Task StoreTokenAsync(string token, bool rememberMe);

    /// <summary>
    /// Retrieve the token from storage.
    /// </summary>
    /// <returns>The JWT token.</returns>
    Task<string> GetTokenAsync();

    /// <summary>
    /// Remove token from storage on logout.
    /// </summary>
    Task RemoveTokenAsync();
}