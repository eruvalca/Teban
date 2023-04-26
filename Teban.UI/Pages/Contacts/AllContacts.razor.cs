﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Refit;
using System.Net;
using System.Text;
using Teban.Contracts.Responses.V1.Contacts;
using Teban.UI.Services;
using Teban.UI.ViewModels;
using Teban.UI.ViewModels.Mapping;

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
    private List<ContactCardViewModel> SelectableContacts { get; set; } = new List<ContactCardViewModel>();
    private int SelectedContactsCount { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Contacts = await ContactsApiService.GetContactsAsync();
            SelectableContacts = Contacts.Items.Select(x => x.MapToContactCardViewModel())
                .OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
                .ToList();
            SelectedContactsCount = SelectableContacts.Count(x => x.IsSelected);
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

    private void HandleSelectChange(int contactIndex)
    {
        SelectableContacts[contactIndex].IsSelected = !SelectableContacts[contactIndex].IsSelected;
        SelectedContactsCount = SelectableContacts.Count(x => x.IsSelected);
    }

    private void HandleCancelSelectedContacts()
    {
        SelectableContacts.ForEach(x => x.IsSelected = false);
        SelectedContactsCount = SelectableContacts.Count(x => x.IsSelected);
    }
}
