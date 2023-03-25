using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Teban.Application.Models;
using Teban.Application.Options;

namespace Teban.Application.Identity;
public class IdentityService : IIdentityService
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly UserManager<TebanUser> _userManager;
    private readonly ISymmetricKeyService _symmetricKeyService;

    public IdentityService(UserManager<TebanUser> userManager,
        ISymmetricKeyService symmetricKeyService,
        IOptions<IdentityServiceOptions> options)
    {
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _userManager = userManager;
        _symmetricKeyService = symmetricKeyService;
    }

    public async Task<(bool, string)> RegisterUserAsync(TebanUser tebanUser, string password)
    {
        var result = await _userManager.CreateAsync(tebanUser, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(tebanUser, "General");
            return (true, tebanUser.Id);
        }

        return result.Errors is not null
            ? ((bool, string))(false, result.Errors.First().Description)
            : (false, "Something went wrong during registration.");
    }

    public async Task<(bool, string)> LoginUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return (false, "User does not exist.");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

        if (!isPasswordValid)
        {
            return (false, "Invalid password.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var symmetricKey = _symmetricKeyService.GetSymmetricKey();

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
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

        return user is null
            ? throw new Exception("This user does not exist.")
            : user.UserName!;
    }

    public async Task<(bool, string)> UpdateUserAsync(TebanUser user)
    {
        var thisUser = await _userManager.Users.FirstAsync(u => u.Id == user.Id);

        thisUser.FirstName = user.FirstName;
        thisUser.LastName = user.LastName;

        var result = await _userManager.UpdateAsync(thisUser);

        if (result.Succeeded)
        {
            return (true, thisUser.Id);
        }

        return result.Errors is not null
            ? ((bool, string))(false, result.Errors.First().Description)
            : (false, "Something went wrong updating user details.");
    }

    public async Task<(bool, IEnumerable<TebanUser>)> GetAllUsersAsync()
    {
        var allUsers = await _userManager.Users.ToListAsync();

        return allUsers.Any()
            ? ((bool, IEnumerable<TebanUser>))(true, allUsers)
            : (false, new List<TebanUser>());
    }
}
