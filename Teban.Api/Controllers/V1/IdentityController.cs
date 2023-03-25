using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Teban.Api.Mapping;
using Teban.Application.Identity;
using Teban.Contracts.Requests.V1.Identity;
using Teban.Contracts.Responses.V1.Identity;

namespace Teban.Api.Controllers.V1;

[ApiVersion(1.0)]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IValidator<RegisterRequest> _registerValidator;

    public IdentityController(IIdentityService identityService, IValidator<RegisterRequest> registerValidator)
    {
        _identityService = identityService;
        _registerValidator = registerValidator;
    }

    [HttpPost(ApiEndpoints.Identity.Register)]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
    {
        await _registerValidator.ValidateAndThrowAsync(registerRequest);
        var tebanUser = registerRequest.MapToTebanUser();
        (bool success, string idOrError) = await _identityService.RegisterUserAsync(tebanUser, registerRequest.Password);

        var response = new RegisterResponse();

        if (success)
        {
            response.Success = true;
            response.UserId = tebanUser.Id;
        }
        else
        {
            response.Success = false;
            response.ErrorMessage = idOrError;
        }

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPost(ApiEndpoints.Identity.LogIn)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LogInAsync([FromBody] LoginRequest loginRequest)
    {
        (bool success, string tokenOrError) = await _identityService.LoginUserAsync(loginRequest.Email, loginRequest.Password);

        var response = new LoginResponse();

        if (success)
        {
            response.Success = true;
            response.Token = tokenOrError;
        }
        else
        {
            response.Success = false;
            response.ErrorMessage = tokenOrError;
        }

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }
}
