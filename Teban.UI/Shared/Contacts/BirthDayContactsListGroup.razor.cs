﻿using Microsoft.AspNetCore.Components;
using Teban.Contracts.Responses.V1.Contacts;

namespace Teban.UI.Shared.Contacts;
public partial class BirthDayContactsListGroup
{
    [Parameter]
    public IEnumerable<ContactResponse> Contacts { get; set; }
}
