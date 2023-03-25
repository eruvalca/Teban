using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

namespace Teban.UI.Shared.Identity;
public partial class LoginDisplay
{
    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationState { get; set; }

    [Inject]
    private NavigationManager Navigation { get; set; }

    private bool IsAuthenticated { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        IsAuthenticated = (await AuthenticationState).User.Identity!.IsAuthenticated;

        StateHasChanged();
        await base.OnParametersSetAsync();
    }

    private async Task BeginSignOut(MouseEventArgs args)
    {
        Navigation.NavigateTo("/");
    }
}
