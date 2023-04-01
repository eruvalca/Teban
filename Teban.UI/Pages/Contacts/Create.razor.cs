using Microsoft.AspNetCore.Components;
using Teban.Api.Sdk;
using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Responses.V1.Contacts;
using Teban.UI.Services;

namespace Teban.UI.Pages.Contacts;
public partial class Create
{
    [Inject]
    private IContactsApiService ContactsApiService { get; set; }
    [Inject]
    private IIdentityService IdentityService { get; set; }
    [Inject]
    private NavigationManager Navigation { get; set; }

    private CreateContactRequest CreateRequest { get; set; } = new CreateContactRequest
    {
        FirstName = string.Empty,
        MiddleName = string.Empty,
        LastName = string.Empty
    };

    private List<string> ErrorMessages { get; set; } = new List<string>();
    private bool ShowError { get; set; } = false;
    private bool DisableSubmit { get; set; } = false;

    private async Task HandleSubmit()
    {
        CreateRequest.TebanUserId = await IdentityService.GetUserId();

        DisableSubmit = true;
        ShowError = false;

        ContactResponse? response;

        try
        {
            response = await ContactsApiService.CreateContactAsync(CreateRequest);
            Navigation.NavigateTo("/");
        }
        catch (ValidationFailureException validationException)
        {
            ErrorMessages = validationException.ValidationResponse
                .Errors
                .Select(x => x.Message)
                .ToList();

            ShowError = true;
            DisableSubmit = false;
        }
        catch (Exception exception)
        {
            ErrorMessages = new List<string> { exception.Message };
            ShowError = true;
            DisableSubmit = false;
        }
    }
}
