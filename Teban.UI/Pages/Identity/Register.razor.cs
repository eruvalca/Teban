using Microsoft.AspNetCore.Components;
using Refit;
using System.Text.Json;
using Teban.Contracts.Requests.V1.Identity;
using Teban.Contracts.Responses.V1.Identity;
using Teban.UI.Services;

namespace Teban.UI.Pages.Identity;
public partial class Register
{
    [Inject]
    private IIdentityService IdentityService { get; set; }
    [Inject]
    private NavigationManager Navigation { get; set; }

    private RegisterRequest RegisterRequest { get; set; } = new RegisterRequest
    {
        Email = string.Empty,
        Password = string.Empty,
        ConfirmPassword = string.Empty,
        FirstName = string.Empty,
        LastName = string.Empty
    };

    private List<string> ErrorMessages { get; set; } = new List<string>();
    private bool ShowError { get; set; } = false;
    private bool DisableSubmit { get; set; } = false;

    private async Task HandleSubmit()
    {
        DisableSubmit = true;
        ShowError = false;

        RegisterResponse response;

        try
        {
            response = await IdentityService.Register(RegisterRequest);

            if (response.Success)
            {
                Navigation.NavigateTo("/");
            }
            else
            {
                ErrorMessages = new List<string> { response.ErrorMessage };
                ShowError = true;
                DisableSubmit = false;
            }
        }
        catch (ApiException exception)
        {
            try
            {
                response = JsonSerializer.Deserialize<RegisterResponse>(exception.Content!, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                })!;
                ErrorMessages = new List<string> { response.ErrorMessage };
            }
            catch (Exception)
            {
                ErrorMessages = new List<string> { exception.Message };
            }

            ShowError = true;
            DisableSubmit = false;
        }
    }
}
