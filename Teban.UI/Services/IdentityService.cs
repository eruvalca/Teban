using Microsoft.AspNetCore.Components.Authorization;
using Refit;
using System.Security.Claims;
using System.Text.Json;
using Teban.Api.Sdk;
using Teban.Contracts.Requests.V1.Identity;
using Teban.Contracts.Responses.V1.Identity;
using Teban.UI.Common;
using Teban.UI.Common.Providers;

namespace Teban.UI.Services;
public class IdentityService : IIdentityService
{
    private IIdentityApi _identityApi;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalSecureStorage _localStorage;

    public IdentityService(AuthenticationStateProvider authStateProvider,
        ILocalSecureStorage localStorage, IIdentityApi identityApi)
    {
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
        _identityApi = identityApi;
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        var response = await _identityApi.RegisterAsync(request);

        return response is not null
            ? response
            : new RegisterResponse
            {
                Success = false,
                ErrorMessage = "Something went wrong. Please try again later."
            };
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        LoginResponse response;

        try
        {
            response = await _identityApi.LoginAsync(request);
        }
        catch (ApiException apiException)
        {
            response = JsonSerializer.Deserialize<LoginResponse>(apiException.Content!, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            })!;
        }
        catch (Exception ex)
        {
            response = new LoginResponse
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }

        if (response is not null)
        {
            if (response.Success)
            {
                await _localStorage.SetAsync("authToken", response.Token);
                ((TokenAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(response.Token);
            }
        }

        return response ?? new LoginResponse
        {
            Success = false,
            ErrorMessage = "Something went wrong. Please try again later."
        };
    }

    public async Task Logout()
    {
        await _localStorage.RemoveAsync("authToken");
        ((TokenAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
    }

    public async Task<string> GetUserId()
    {
        var token = await _localStorage.GetAsync("authToken");
        var claims = JwtParser.ParseClaimsFromJwt(token);
        return claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
    }
}
