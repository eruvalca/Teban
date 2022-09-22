using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teban.Application.Dtos.Identity;
using Teban.Application.Dtos.Request;
using Teban.Application.Interfaces;

namespace Teban.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _identityService.RegisterUserAsync(registerDto);

            if (result.Item1)
            {
                //try to log user in
                var loginDto = new LoginDto
                {
                    Email = registerDto.Email,
                    Password = registerDto.Password
                };

                var loginResult = await _identityService.LoginUserAsync(loginDto);

                if (loginResult.Item1)
                {
                    //return token for login
                    var response = IdentityRequestResponseDto.Success(loginResult.Item2, result.Item2);
                    return Ok(response);
                }
                else
                {
                    //if login did not work just return register result
                    var response = IdentityRequestResponseDto.Success(string.Empty, result.Item2);
                    return Ok(response);
                }
            }
            else
            {
                var response = IdentityRequestResponseDto.Failure(new string[] { result.Item2 });
                return BadRequest(response);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _identityService.LoginUserAsync(loginDto);

            if (result.Item1)
            {
                var response = IdentityRequestResponseDto.Success(result.Item2, string.Empty);
                return Ok(response);
            }
            else
            {
                var response = IdentityRequestResponseDto.Failure(new string[] { result.Item2 });
                return BadRequest(response);
            }
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTebanUserDto updateDto)
        {
            if (id != updateDto.Id)
            {
                var response = IdentityRequestResponseDto.Failure(new string[] { "The given user id does not match the updated user id." });
                return BadRequest(response);
            }

            var result = await _identityService.UpdateUserAsync(updateDto);

            if (result.Item1)
            {
                var response = IdentityRequestResponseDto.Success(string.Empty, result.Item2);
                return Ok(response);
            }
            else
            {
                var response = IdentityRequestResponseDto.Failure(new string[] { result.Item2 });
                return BadRequest(response);
            }
        }
    }
}
