namespace Teban.Application.Dtos.Request
{
    public class IdentityRequestDto
    {
        internal IdentityRequestDto(bool succeeded, IEnumerable<string> errors, string token, string userId)
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

        public static IdentityRequestDto Success(string token, string userId)
        {
            return new IdentityRequestDto(true, Array.Empty<string>(), token, userId);
        }

        public static IdentityRequestDto Failure(IEnumerable<string> errors)
        {
            return new IdentityRequestDto(false, errors, string.Empty, string.Empty);
        }

        public IdentityRequestDto()
        {
        }
    }
}
