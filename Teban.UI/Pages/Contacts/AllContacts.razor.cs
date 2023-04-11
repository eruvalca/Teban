using Microsoft.AspNetCore.Components;
using Refit;
using System.Net;
using Teban.Contracts.Responses.V1.Contacts;
using Teban.UI.Services;

namespace Teban.UI.Pages.Contacts;
public partial class AllContacts
{
    [Inject]
    private IContactsApiService ContactsApiService { get; set; }
    [Inject]
    private IIdentityService IdentityService { get; set; }
    [Inject]
    private NavigationManager Navigation { get; set; }

    private ContactsResponse? Contacts { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Contacts = await ContactsApiService.GetContactsAsync();
        }
        catch (ApiException exception)
        {
            if (exception.StatusCode == HttpStatusCode.Unauthorized)
            {
                await IdentityService.Logout();
                Navigation.NavigateTo("user/login");
            }
        }
    }
}
