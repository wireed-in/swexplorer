using swexplorer.client.Components;
using swexplorer.client.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register custom authentication state provider and override the AuthenticationStateProvider.
builder.Services.AddScoped<ICustomAuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Register token service for storing and retreving jwt tokens.
builder.Services.AddScoped<ITokenService, TokenService>();

// Setup HttpClient with our api.
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7257")
});

builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
