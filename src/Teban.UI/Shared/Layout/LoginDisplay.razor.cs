using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Teban.Application.Dtos.Identity;
using Teban.UI.Services;

namespace Teban.UI.Shared.Layout
{
    public partial class LoginDisplay
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private IdentityClientService IdentityService { get; set; }

        private TebanUserDto? User { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var isAuthenticated = (await AuthenticationState)?.User?.Identity?.IsAuthenticated;

            if (isAuthenticated is not null && isAuthenticated == true)
            {
                User = await IdentityService.GetUserDetails();
            }

            StateHasChanged();
            await base.OnParametersSetAsync();
        }

        private async Task BeginSignOut(MouseEventArgs args)
        {
            await IdentityService.Logout();
            Navigation.NavigateTo("/");
        }
    }
}
