namespace Teban.Application.Dtos.Identity
{
    public class TebanUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? ProfilePhotoUrl { get; set; }
        public DateTime Joined { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
