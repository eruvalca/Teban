using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teban.Api.Mapping;
using Teban.Application.Identity;
using Teban.Application.Services;
using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Responses.V1;
using Teban.Contracts.Responses.V1.Contacts;

namespace Teban.Api.Controllers.V1;

[ApiVersion(1.0)]
[Authorize]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly ICurrentUserService _currentUserService;

    public ContactsController(IContactService contactService, ICurrentUserService currentUserService)
    {
        _contactService = contactService;
        _currentUserService = currentUserService;
    }

    [HttpPost(ApiEndpoints.Contacts.Create)]
    [ProducesResponseType(typeof(ContactResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateContactRequest request, CancellationToken ctoken = default)
    {
        var contact = request.MapToContact();
        var createResult = await _contactService.CreateAsync(contact, ctoken);

        if (!createResult)
        {
            return BadRequest("Something went wrong. Please try again later.");
        }

        var contactResponse = contact.MapToResponse();
        return CreatedAtAction(nameof(Get), new { id = contactResponse.ContactId }, contactResponse);
    }

    [HttpGet(ApiEndpoints.Contacts.Get)]
    [ProducesResponseType(typeof(ContactResponse), StatusCodes.Status200OK)]
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

        var contact = await _contactService.GetByIdAsync(id, tebanUserId, cToken);

        if (contact is null)
        {
            return NotFound();
        }

        var contactResponse = contact.MapToResponse();
        return Ok(contactResponse);
    }

    [HttpGet(ApiEndpoints.Contacts.GetAll)]
    [ProducesResponseType(typeof(ContactsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll(CancellationToken cToken = default)
    {
        var tebanUserId = _currentUserService.UserId;

        if (tebanUserId is null)
        {
            return BadRequest("There was a problem with the user info. Please log out and log back in.");
        }

        var contacts = await _contactService.GetAllAsync(tebanUserId, cToken);

        var contactsResponse = contacts.MapToResponse();
        return Ok(contactsResponse);
    }

    [HttpPut(ApiEndpoints.Contacts.Update)]
    [ProducesResponseType(typeof(ContactResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromRoute] int id,
        [FromBody] UpdateContactRequest request, CancellationToken cToken = default)
    {
        var tebanUserId = _currentUserService.UserId;

        if (tebanUserId is null)
        {
            return BadRequest("There was a problem with the user info. Please log out and log back in.");
        }

        var contact = request.MapToContact(id);
        var updatedContact = await _contactService.UpdateAsync(contact, tebanUserId, cToken);

        if (updatedContact is null)
        {
            return NotFound();
        }

        var updateResponse = updatedContact.MapToResponse();
        return Ok(updateResponse);
    }

    [HttpDelete(ApiEndpoints.Contacts.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cToken = default)
    {
        var deleted = await _contactService.DeleteByIdAsync(id, cToken);

        return !deleted ? NotFound() : Ok();
    }
}
