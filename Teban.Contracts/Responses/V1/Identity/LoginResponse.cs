namespace Teban.Contracts.Responses.V1.Identity;
public class LoginResponse
{
    public bool Success { get; set; }
    public string Token { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}
