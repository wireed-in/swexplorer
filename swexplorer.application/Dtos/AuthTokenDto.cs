namespace swexplorer.application.Dtos;

/// <summary>
/// AuthTokenDto - Contains the authorization token associated expiration date-time.
/// </summary>
public class AuthTokenDto
{
    /// <summary>
    /// Gets or sets the expireation date-time.
    /// </summary>
    public DateTime ExpiresAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Gets or sets the authentication token (e.g., JWT) for the user.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}