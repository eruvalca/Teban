using Microsoft.AspNetCore.Identity;

namespace Teban.Application.Models;
public class TebanUser : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public ICollection<Contact>? Contacts { get; set; }
}
