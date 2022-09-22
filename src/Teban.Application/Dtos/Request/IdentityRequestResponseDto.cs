namespace Teban.Application.Dtos.Request
{
    public class IdentityRequestResponseDto
    {
        internal IdentityRequestResponseDto(bool succeeded, IEnumerable<string> errors, string token, string userId)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Token = token;
            UserId = userId;
        }

        public bool Succeeded { get; set; }
        public string? Token { get; set; }
        public string? UserId { get; set; }
        public string[]? Errors { get; set; }

        public static IdentityRequestResponseDto Success(string token, string userId)
        {
            return new IdentityRequestResponseDto(true, Array.Empty<string>(), token, userId);
        }

        public static IdentityRequestResponseDto Failure(IEnumerable<string> errors)
        {
            return new IdentityRequestResponseDto(false, errors, string.Empty, string.Empty);
        }

        public IdentityRequestResponseDto()
        {
        }
    }
}
