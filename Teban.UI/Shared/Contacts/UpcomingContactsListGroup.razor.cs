using Microsoft.AspNetCore.Components;
using Teban.Contracts.Responses.V1.Contacts;

namespace Teban.UI.Shared.Contacts;
public partial class UpcomingContactsListGroup
{
    [Parameter]
    public IEnumerable<(ContactResponse contact, DateTime scheduledDate)> Contacts { get; set; }
}
