namespace swexplorer.application.Dtos;

/// <summary>
/// User Data Transfer Object (DTO) - Contains essential user information
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}