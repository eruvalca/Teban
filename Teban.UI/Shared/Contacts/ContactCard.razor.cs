using Microsoft.AspNetCore.Components;
using Teban.Contracts.Responses.V1.Contacts;
using Teban.UI.ViewModels;

namespace Teban.UI.Shared.Contacts;
public partial class ContactCard
{
    [Parameter]
    public ContactCardViewModel Contact { get; set; }
    [Parameter]
    public int ContactIndex { get; set; }
    [Parameter]
    public EventCallback<int> HandleSelectChange { get; set; }

    private bool IsSelected { get; set; }

    protected override void OnParametersSet()
    {
        IsSelected = Contact.IsSelected;
    }
}
