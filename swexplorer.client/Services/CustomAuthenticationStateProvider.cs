using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace swexplorer.client.Services
{
    /// <summary>
    /// Custom Authentication State Provider for managing user authentication state.
    /// </summary>
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ICustomAuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public CustomAuthenticationStateProvider(
            HttpClient httpClient,
            ITokenService tokenService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), $"{nameof(httpClient)} cannot be null."); ;
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService), $"{nameof(tokenService)} cannot be null."); ;
        }

        /// <inheritdoc/>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Try to get jwt from session or local storage.
            var jwt = await _tokenService.GetTokenAsync();

            // Create identity with jwt or anonymous identity.
            var identity = string.IsNullOrEmpty(jwt)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(GetClaimsFromJwt(jwt), "jwt");

            // Create a user with identity.
            var user = new ClaimsPrincipal(identity);

            if (!string.IsNullOrEmpty(jwt) && _httpClient.DefaultRequestHeaders.Authorization == null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt);
            }

            return new AuthenticationState(user);
        }

        /// <inheritdoc/>
        public async Task MarkAuthenticatedAsync(string jwt, bool rememberMe)
        {
            await _tokenService.StoreTokenAsync(jwt, rememberMe);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(GetClaimsFromJwt(jwt), "jwt"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt);
        }

        /// <inheritdoc/>
        public async Task MarkNotAuthenticatedAsync()
        {
            await _tokenService.RemoveTokenAsync();
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        /// <summary>
        /// Gets claims from JWT token.
        /// TODO: Look into manually parsing JWT if this is a memory issue.
        /// </summary>
        /// <param name="jwt">The JWT token.</param>
        /// <returns>An array of claims.</returns>
        private static IEnumerable<Claim> GetClaimsFromJwt(string jwt)
        {
            var jwtSecurityToken = new JwtSecurityToken(jwt);
            return jwtSecurityToken.Claims;
        }
    }
}