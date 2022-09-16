using Microsoft.AspNetCore.Components;
using Teban.Application.Dtos.Identity;
using Teban.UI.Services;

namespace Teban.UI.Pages.Identity
{
    public partial class Login
    {
        [Inject]
        private IdentityClientService IdentityService { get; set; }
        [Inject]
        private NavigationManager Navigation { get; set; }

        private LoginDto LoginDto { get; set; } = new LoginDto();
        private List<string> ServerMessages { get; set; } = new List<string>();
        private bool ShowServerErrors { get; set; } = false;
        private bool DisableSubmit { get; set; } = false;

        private async Task HandleSubmit()
        {
            DisableSubmit = true;
            ShowServerErrors = false;

            var response = await IdentityService.Login(LoginDto);

            if (response.Succeeded)
            {
                Navigation.NavigateTo("/");
            }
            else
            {
                if (response.Errors is not null)
                {
                    ServerMessages = response.Errors.ToList();
                    ShowServerErrors = true;
                    DisableSubmit = false;
                }
                else
                {
                    ServerMessages = new List<string> { "There was an issue logging in." };
                    ShowServerErrors = true;
                    DisableSubmit = false;
                }
            }
        }
    }
}
