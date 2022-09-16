using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Teban.Application.Dtos.Identity;
using Teban.Application.Dtos.Request;
using Teban.UI.Common;
using Teban.UI.Common.Providers;
using Teban.UI.Interfaces;

namespace Teban.UI.Services
{
    public class IdentityClientService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalSecureStorage _localStorage;

        public IdentityClientService(HttpClient client, AuthenticationStateProvider authStateProvider,
            ILocalSecureStorage localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<IdentityRequestDto> Register(RegisterDto registerDto)
        {
            var response = await _client.PostAsJsonAsync("identity/register", registerDto);
            var authResult = await response.Content.ReadFromJsonAsync<IdentityRequestDto>();

            if (authResult is not null)
            {
                if (authResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(authResult.Token))
                    {
                        await _localStorage.SetAsync("authToken", authResult.Token);
                        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResult.Token);
                        ((TokenAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(authResult.Token);
                    }
                }

                return authResult;
            }

            return IdentityRequestDto.Failure(new string[] { "There was an error during registration." });
        }

        public async Task<IdentityRequestDto> Login(LoginDto loginDto)
        {
            var response = await _client.PostAsJsonAsync("identity/login", loginDto);
            var authResult = await response.Content.ReadFromJsonAsync<IdentityRequestDto>();

            if (authResult is not null)
            {
                if (authResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(authResult.Token))
                    {
                        await _localStorage.SetAsync("authToken", authResult.Token);
                        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResult.Token);
                        ((TokenAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(authResult.Token);
                    }
                }

                return authResult;
            }

            return IdentityRequestDto.Failure(new string[] { "There was an error during login." });
        }

        public async Task Logout()
        {
            await _localStorage.RemoveAsync("authToken");
            _client.DefaultRequestHeaders.Authorization = null;
            ((TokenAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
        }

        public async Task<IdentityRequestDto> UpdateUser(UpdateTebanUserDto userDto)
        {
            var response = await _client.PutAsJsonAsync($"identity/update/{userDto.Id}", userDto);
            var result = await response.Content.ReadFromJsonAsync<IdentityRequestDto>();

            if (result is not null)
            {
                return result;
            }

            return IdentityRequestDto.Failure(new string[] { "There was an error updating." });
        }

        public async Task<TebanUserDto> GetUserDetails()
        {
            var token = await _localStorage.GetAsync("authToken");
            var claims = JwtParser.ParseClaimsFromJwt(token);

            var user = new TebanUserDto
            {
                UserId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                Email = claims.First(c => c.Type == ClaimTypes.Email).Value,
                FirstName = claims.First(c => c.Type == ClaimTypes.GivenName).Value,
                LastName = claims.First(c => c.Type == ClaimTypes.Surname).Value,
                ProfilePhotoUrl = claims.FirstOrDefault(c => c.Type == "ProfilePhotoUrl") is null ? string.Empty : claims.First(c => c.Type == "ProfilePhotoUrl").Value,
                Joined = DateTime.Parse(claims.First(c => c.Type == "Joined").Value)
            };

            return user;
        }
    }
}
