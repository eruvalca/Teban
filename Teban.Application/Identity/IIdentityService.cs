using Teban.Application.Models;

namespace Teban.Application.Identity;
public interface IIdentityService
{
    Task<(bool, string)> RegisterUserAsync(TebanUser tebanUser, string password);
    Task<(bool, string)> LoginUserAsync(string email, string password);
}
