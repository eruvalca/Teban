using Teban.Contracts.Requests.V1.Identity;
using Teban.Contracts.Responses.V1.Identity;

namespace Teban.UI.Services;

public interface IIdentityService
{
    Task<RegisterResponse> Register(RegisterRequest request);

    Task<LoginResponse> Login(LoginRequest request);

    Task Logout();

    Task<string> GetUserId();
}