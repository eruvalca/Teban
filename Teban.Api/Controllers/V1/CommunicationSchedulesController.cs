using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teban.Api.Mapping;
using Teban.Application.Identity;
using Teban.Application.Services;
using Teban.Contracts.Requests.V1.CommunicationSchedules;
using Teban.Contracts.Responses.V1;
using Teban.Contracts.Responses.V1.CommunicationSchedules;

namespace Teban.Api.Controllers.V1;

[ApiVersion(1.0)]
[Authorize]
[ApiController]
public class CommunicationSchedulesController : ControllerBase
{
    private readonly ICommunicationScheduleService _scheduleService;
    private readonly ICurrentUserService _currentUserService;

    public CommunicationSchedulesController(ICommunicationScheduleService scheduleService, ICurrentUserService currentUserService)
    {
        _scheduleService = scheduleService;
        _currentUserService = currentUserService;
    }

    [HttpPost(ApiEndpoints.CommunicationSchedules.Create)]
    [ProducesResponseType(typeof(CommunicationScheduleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateCommunicationScheduleRequest request, CancellationToken token = default)
    {
        var schedule = request.MapToCommunicationSchedule();
        var createResult = await _scheduleService.CreateAsync(schedule, token);

        if (!createResult)
        {
            return BadRequest("Something went wrong. Please try again later.");
        }

        var scheduleResponse = schedule.MapToResponse();
        return CreatedAtAction(nameof(Get), new { id = scheduleResponse.CommunicationScheduleId }, scheduleResponse);
    }

    [HttpGet(ApiEndpoints.CommunicationSchedules.Get)]
    [ProducesResponseType(typeof(CommunicationScheduleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cToken = default)
    {
        var tebanUserId = _currentUserService.UserId;

        if (tebanUserId is null)
        {
            return BadRequest("There was a problem with the user info. Please log out and log back in.");
        }

        var schedule = await _scheduleService.GetByIdAsync(id, tebanUserId, cToken);

        if (schedule is null)
        {
            return NotFound();
        }

        var scheduleResponse = schedule.MapToResponse();
        return Ok(scheduleResponse);
    }

    [HttpGet(ApiEndpoints.CommunicationSchedules.GetAll)]
    [ProducesResponseType(typeof(CommunicationSchedulesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll(CancellationToken cToken = default)
    {
        var tebanUserId = _currentUserService.UserId;

        if (tebanUserId is null)
        {
            return BadRequest("There was a problem with the user info. Please log out and log back in.");
        }

        var schedules = await _scheduleService.GetAllAsync(tebanUserId, cToken);

        var schedulesResponse = schedules.MapToResponse();
        return Ok(schedulesResponse);
    }

    [HttpPut(ApiEndpoints.CommunicationSchedules.Update)]
    [ProducesResponseType(typeof(CommunicationScheduleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromRoute] int id,
        [FromBody] UpdateCommunicationScheduleRequest request, CancellationToken cToken = default)
    {
        var tebanUserId = _currentUserService.UserId;

        if (tebanUserId is null)
        {
            return BadRequest("There was a problem with the user info. Please log out and log back in.");
        }

        var schedule = request.MapToCommunicationSchedule(id);
        var updatedSchedule = await _scheduleService.UpdateAsync(schedule, tebanUserId, cToken);

        if (updatedSchedule is null)
        {
            return NotFound();
        }

        var updateResponse = updatedSchedule.MapToResponse();
        return Ok(updateResponse);
    }

    [HttpDelete(ApiEndpoints.CommunicationSchedules.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cToken = default)
    {
        var deleted = await _scheduleService.DeleteByIdAsync(id, cToken);

        return !deleted ? NotFound() : Ok();
    }
}
