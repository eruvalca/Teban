using Teban.Application.Dtos.Identity;

namespace Teban.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool, string)> RegisterUserAsync(RegisterDto registerDto);

        Task<(bool, string)> LoginUserAsync(LoginDto loginDto);

        Task<string> GetUserNameAsync(string userId);

        Task<(bool, string)> UpdateUserAsync(UpdateTebanUserDto userDto);

        Task<(bool, IEnumerable<TebanUserDto>)> GetAllUsersAsync();
    }
}
