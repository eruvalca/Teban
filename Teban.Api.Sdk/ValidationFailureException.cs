using Teban.Contracts.Responses.V1;

namespace Teban.Api.Sdk;
public class ValidationFailureException : Exception
{
    public ValidationFailureResponse ValidationResponse { get; set; }

    public ValidationFailureException(ValidationFailureResponse validationResponse)
    {
        ValidationResponse = validationResponse;
    }
}
