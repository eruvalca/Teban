using Microsoft.AspNetCore.Components;
using Refit;
using System.Net;
using Teban.Contracts.Helpers.V1;
using Teban.Contracts.Responses.V1.Contacts;
using Teban.UI.Services;

namespace Teban.UI.Pages;
public partial class Index
{
    [Inject]
    private IContactsApiService ContactsApiService { get; set; }
    [Inject]
    private IIdentityService IdentityService { get; set; }
    [Inject]
    private NavigationManager Navigation { get; set; }

    private DateTime Today { get; set; } = DateTime.Today;
    private ContactsResponse? Contacts { get; set; }
    private IEnumerable<ContactResponse>? BirthDayContacts { get; set; }
    private IEnumerable<ContactResponse>? ScheduledContacts { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Contacts = await ContactsApiService.GetContactsAsync();
            FilterContacts();
        }
        catch (ApiException exception)
        {
            if (exception.StatusCode == HttpStatusCode.Unauthorized)
            {
                await IdentityService.Logout();
                Navigation.NavigateTo("/login");
            }
        }
    }

    private void FilterContacts()
    {
        var todayBDayContacts = Contacts!.Items
            .Where(x => x.DateOfBirth?.Month == Today.Month
                    && x.DateOfBirth?.Day == Today.Day);

        var todayScheduledContacts = Contacts!.Items
            .Where(x => (x.StartDate != null && x.Frequency != null)
                    && ScheduleDateHelper.IsNextDate(x.Frequency,
                        ((DateTime)x.StartDate),
                        Today));

        BirthDayContacts = todayBDayContacts.Any() ? todayBDayContacts : Enumerable.Empty<ContactResponse>();
        ScheduledContacts = todayScheduledContacts.Any() ? todayScheduledContacts : Enumerable.Empty<ContactResponse>();
    }
}
