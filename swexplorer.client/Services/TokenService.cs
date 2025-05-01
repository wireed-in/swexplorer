using swexplorer.client.Services;
using Microsoft.JSInterop;

namespace swexplorer.client.Services;

/// <summary>
/// Service for handling token storage in local/session storage.
/// </summary>
public class TokenService : ITokenService
{
    private readonly IJSRuntime _jsRuntime;

    public TokenService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    /// <inheritdoc/>
    public async Task StoreTokenAsync(string token, bool rememberMe)
    {
        if (rememberMe)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwt_token", token);
        }
        else
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "jwt_token", token);
        }
    }

    /// <inheritdoc/>
    public async Task<string> GetTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "jwt_token")
            ?? await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt_token");
    }

    /// <inheritdoc/>
    public async Task RemoveTokenAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "jwt_token");
        await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "jwt_token");
    }
}