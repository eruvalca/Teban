using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teban.UI.Shared.Contacts;
public partial class MultipleContactDelete
{
    [Parameter]
    public int SelectedContactsCount { get; set; }
    [Parameter]
    public int SelectableContactsCount { get; set; }
    [Parameter]
    public bool IsDeleteDisabled { get; set; }
    [Parameter]
    public EventCallback HandleCancelSelectedContacts { get; set; }
    [Parameter]
    public EventCallback HandleDeleteSelectedContacts { get; set; }
    [Parameter]
    public EventCallback<bool> HandleSelectAllContacts { get; set; }

    private bool _isSelectAll;
    private bool IsSelectAll
    {
        get { return _isSelectAll; }
        set
        {
            _isSelectAll = SelectedContactsCount < SelectableContactsCount;
            HandleSelectAllContacts.InvokeAsync(_isSelectAll);
        }
    }

    protected override void OnParametersSet()
    {
        _isSelectAll = SelectedContactsCount >= SelectableContactsCount;
    }
}
