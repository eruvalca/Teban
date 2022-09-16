using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Teban.Infrastructure.Services
{
    public class SymmetricKeyService
    {
        private readonly IConfiguration _config;

        public SymmetricKeyService(IConfiguration config)
        {
            _config = config;
        }

        public SymmetricSecurityKey GetSymmetricKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Teban:SymmetricKey"]));
        }
    }
}
