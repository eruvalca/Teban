using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Teban.Application.Dtos.Identity;
using Teban.UI.Services;

namespace Teban.UI.Pages.Identity
{
    [Authorize]
    public partial class Profile
    {
        [Inject]
        private IdentityClientService IdentityService { get; set; }
        [Inject]
        private NavigationManager Navigation { get; set; }

        private TebanUserDto User { get; set; } = new TebanUserDto();
        private UpdateTebanUserDto? UserDto { get; set; }
        private List<string> ServerMessages { get; set; } = new List<string>();
        private bool ShowServerErrors { get; set; } = false;
        private bool DisableSubmit { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            User = await IdentityService.GetUserDetails();

            UserDto = new()
            {
                Id = User.UserId,
                Email = User.Email,
                FirstName = User.FirstName,
                LastName = User.LastName,
                ProfilePhotoUrl = User.ProfilePhotoUrl
            };
        }

        private async Task HandleSubmit()
        {
            DisableSubmit = true;
            ShowServerErrors = false;

            var response = await IdentityService.UpdateUser(UserDto!);

            if (response.Succeeded)
            {
                await IdentityService.Logout();
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
                    ServerMessages = new List<string> { "There was an issue updating user profile." };
                    ShowServerErrors = true;
                    DisableSubmit = false;
                }
            }
        }
    }
}
