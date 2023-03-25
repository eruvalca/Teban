namespace Teban.Contracts.Responses.V1.Identity;
public class RegisterResponse
{
    public bool Success { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}
