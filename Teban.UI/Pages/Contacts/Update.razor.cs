using Microsoft.AspNetCore.Components;
using Teban.Api.Sdk;
using Teban.Contracts.ContractMapping.V1;
using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Responses.V1.Contacts;
using Teban.UI.Services;

namespace Teban.UI.Pages.Contacts;
public partial class Update
{
    [Inject]
    private IContactsApiService ContactsApiService { get; set; }
    [Inject]
    private IIdentityService IdentityService { get; set; }
    [Inject]
    private NavigationManager Navigation { get; set; }

    [Parameter]
    public int Id { get; set; }

    public ContactResponse? Contact { get; set; }

    private UpdateContactRequest? UpdateRequest { get; set; }

    private List<string> ErrorMessages { get; set; } = new List<string>();
    private bool ShowError { get; set; } = false;
    private bool DisableSubmit { get; set; } = false;
    private bool DisableDelete { get; set; } = false;

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Contact = await ContactsApiService.GetContactAsync(Id);
            UpdateRequest = Contact.MapToUpdateContactRequest();
        }
        catch (Exception exception)
        {
            ErrorMessages = new List<string> { exception.Message };
            ShowError = true;
            DisableSubmit = false;
        }
    }

    private async Task HandleSubmit()
    {
        if (UpdateRequest is not null)
        {
            DisableSubmit = true;
            ShowError = false;

            ContactResponse? response;

            try
            {
                response = await ContactsApiService.UpdateContactAsync(Id, UpdateRequest);
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

    private async Task HandleDelete()
    {
        DisableDelete = true;

        try
        {
            await ContactsApiService.DeleteContactAsync(Id);
            Navigation.NavigateTo("/");
        }
        catch (Exception exception)
        {
            ErrorMessages = new List<string> { exception.Message };
            ShowError = true;
            DisableSubmit = false;
        }
    }
}
