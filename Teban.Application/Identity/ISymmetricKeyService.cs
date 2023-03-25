using Microsoft.IdentityModel.Tokens;

namespace Teban.Application.Identity;
public interface ISymmetricKeyService
{
    SymmetricSecurityKey GetSymmetricKey();
}
