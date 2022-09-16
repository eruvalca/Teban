using Microsoft.AspNetCore.Identity;
using Teban.Domain.Entities;

namespace Teban.Infrastructure.Identity
{
    public class TebanUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? ProfilePhotoUrl { get; set; }
        public DateTime Joined { get; set; } = DateTime.UtcNow;

        public ICollection<Budget>? Budgets { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}
