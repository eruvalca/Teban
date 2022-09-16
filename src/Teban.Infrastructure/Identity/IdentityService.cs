using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Teban.Application.Dtos.Identity;
using Teban.Application.Interfaces;
using Teban.Infrastructure.Services;

namespace Teban.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<TebanUser> _userManager;
        private readonly SymmetricKeyService _symmetricKeyService;

        public IdentityService(IConfiguration config,
            UserManager<TebanUser> userManager,
            SymmetricKeyService symmetricKeyService)
        {
            _config = config;
            _userManager = userManager;
            _symmetricKeyService = symmetricKeyService;
        }

        public async Task<(bool, string)> RegisterUserAsync(RegisterDto registerDto)
        {
            var user = new TebanUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                ProfilePhotoUrl = registerDto.ProfilePhotoUrl,
                Joined = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "General");
                return (true, user.Id);
            }

            if (result.Errors is not null)
            {
                return (false, result.Errors.First().Description);
            }

            return (false, "Something went wrong during registration.");
        }

        public async Task<(bool, string)> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                return (false, "User does not exist.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordValid)
            {
                return (false, "Invalid password.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginDto.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("ProfilePhotoUrl", user.ProfilePhotoUrl ?? string.Empty),
                new Claim("Joined", user.Joined.ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var symmetricKey = _symmetricKeyService.GetSymmetricKey();

            string issuer = _config["Teban:Issuer"];
            string audience = _config["Teban:Audience"];

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddDays(14),
                signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (true, tokenString);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<(bool, string)> UpdateUserAsync(UpdateTebanUserDto userDto)
        {
            var thisUser = await _userManager.Users.FirstAsync(u => u.Id == userDto.Id);

            thisUser.FirstName = userDto.FirstName;
            thisUser.LastName = userDto.LastName;
            thisUser.ProfilePhotoUrl = userDto.ProfilePhotoUrl;

            var result = await _userManager.UpdateAsync(thisUser);

            if (result.Succeeded)
            {
                return (true, thisUser.Id);
            }

            if (result.Errors is not null)
            {
                return (false, result.Errors.First().Description);
            }

            return (false, "Something went wrong updating user details.");
        }

        public async Task<(bool, IEnumerable<TebanUserDto>)> GetAllUsersAsync()
        {
            var allUsers = await _userManager.Users
                .Select(u => new TebanUserDto
                {
                    UserId = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ProfilePhotoUrl = u.ProfilePhotoUrl
                }).ToListAsync();

            if (allUsers.Any())
            {
                return (true, allUsers);
            }

            return (false, new List<TebanUserDto>());
        }
    }
}
