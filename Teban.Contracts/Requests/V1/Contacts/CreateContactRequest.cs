namespace Teban.Contracts.Requests.V1.Contacts;
public class CreateContactRequest
{
    public required string FirstName { get; set; }
    public string MiddleName { get; set; } = string.Empty;
    public required string LastName { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }

    public string TebanUserId { get; set; } = string.Empty;
    public string? Frequency { get; set; }
    public DateTime? StartDate { get; set; }
}
