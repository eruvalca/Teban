namespace Teban.Contracts.Requests.V1.Identity;
public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
