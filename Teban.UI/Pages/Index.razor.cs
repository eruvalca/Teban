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

    private IEnumerable<ContactResponse>? Next7BirthDayContacts { get; set; }
    private IEnumerable<(ContactResponse contact, DateTime scheduledDate)>? Next7ScheduledContacts { get; set; }

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
                Navigation.NavigateTo("user/login");
            }
        }
    }

    private void FilterContacts()
    {
        var todayBDayContacts = Contacts!.Items
            .Where(x => ContactDateHelper.IsBirthDate(x.DateOfBirth, Today));

        var todayScheduledContacts = Contacts!.Items
            .Where(x => ContactDateHelper.IsNextDate(x.Frequency,
                        x.StartDate,
                        Today));

        var next7BDayContacts = Contacts!.Items
            .Where(x => ContactDateHelper.IsBirthDateWithin7Days(x.DateOfBirth, Today));

        var next7ScheduledContacts = ContactDateHelper.GetContactsScheduledWithin7Days(Contacts!.Items, Today);

        BirthDayContacts = todayBDayContacts.Any()
            ? todayBDayContacts
            : Enumerable.Empty<ContactResponse>();
        ScheduledContacts = todayScheduledContacts.Any()
            ? todayScheduledContacts
            : Enumerable.Empty<ContactResponse>();
        Next7BirthDayContacts = next7BDayContacts.Any()
            ? next7BDayContacts
            : Enumerable.Empty<ContactResponse>();
        Next7ScheduledContacts = next7ScheduledContacts.Any()
            ? next7ScheduledContacts
            : Enumerable.Empty<(ContactResponse, DateTime)>();
    }
}
