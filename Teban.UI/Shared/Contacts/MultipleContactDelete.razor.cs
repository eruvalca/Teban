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
            HandleSelectAllContacts.InvokeAsync(value);
            _isSelectAll = value;
        }
    }
}
