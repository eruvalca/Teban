using Refit;
using Teban.Contracts.Requests.V1.Identity;
using Teban.Contracts.Responses.V1.Identity;

namespace Teban.Api.Sdk;
public interface IIdentityApi
{
    [Post(ApiEndpoints.Identity.Register)]
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);

    [Post(ApiEndpoints.Identity.LogIn)]
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
