using Microsoft.AspNetCore.Components;
using Refit;
using System.Text.Json;
using Teban.Api.Sdk;
using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Responses.V1;
using Teban.Contracts.Responses.V1.Contacts;
using Teban.UI.Services;

namespace Teban.UI.Pages.Contacts;
public partial class Create
{
    [Inject]
    private IContactsApi ContactsApi { get; set; }
    [Inject]
    private IIdentityService IdentityService { get; set; }
    [Inject]
    private NavigationManager Navigation { get; set; }

    private CreateContactRequest CreateRequest { get; set; } = new CreateContactRequest
    {
        FirstName = string.Empty,
        MiddleName = string.Empty,
        LastName = string.Empty,
        DateOfBirth = DateTime.Today
    };

    private List<string> ErrorMessages { get; set; } = new List<string>();
    private bool ShowError { get; set; } = false;
    private bool DisableSubmit { get; set; } = false;

    private async Task HandleSubmit()
    {
        CreateRequest.TebanUserId = await IdentityService.GetUserId();

        DisableSubmit = true;
        ShowError = false;

        ContactResponse response;

        try
        {
            response = await ContactsApi.CreateContactAsync(CreateRequest);
            Navigation.NavigateTo("/");
        }
        catch (ApiException exception)
        {
            try
            {
                ValidationFailureResponse validationFailureResponse = JsonSerializer.Deserialize<ValidationFailureResponse>(exception.Content!, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                })!;

                ErrorMessages = validationFailureResponse is not null && validationFailureResponse.Errors.Any()
                    ? validationFailureResponse.Errors
                        .Select(x => x.Message)
                        .ToList()
                    : new List<string> { "Something went wrong. Please try again later." };
            }
            catch (Exception)
            {
                ErrorMessages = new List<string> { exception.Message };
            }

            ShowError = true;
            DisableSubmit = false;
        }
    }
}
