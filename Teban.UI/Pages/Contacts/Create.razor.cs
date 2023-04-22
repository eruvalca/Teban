using CsvHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Globalization;
using System.Text;
using Teban.Api.Sdk;
using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Responses.V1.Contacts;
using Teban.UI.Services;

namespace Teban.UI.Pages.Contacts;
public partial class Create
{
    [Inject]
    private IContactsApiService ContactsApiService { get; set; }
    [Inject]
    private IIdentityService IdentityService { get; set; }
    [Inject]
    private NavigationManager Navigation { get; set; }

    private CreateContactRequest CreateRequest { get; set; } = new CreateContactRequest
    {
        FirstName = string.Empty,
        MiddleName = string.Empty,
        LastName = string.Empty
    };

    private List<string> ErrorMessages { get; set; } = new List<string>();
    private bool ShowError { get; set; } = false;
    private bool DisableSubmit { get; set; } = false;

    private async Task HandleSubmit()
    {
        CreateRequest.TebanUserId = await IdentityService.GetUserId();

        DisableSubmit = true;
        ShowError = false;

        ContactResponse? response;

        try
        {
            response = await ContactsApiService.CreateContactAsync(CreateRequest);
            Navigation.NavigateTo("/");
        }
        catch (ValidationFailureException validationException)
        {
            ErrorMessages = validationException.ValidationResponse
                .Errors
                .Select(x => x.Message)
                .ToList();

            ShowError = true;
            DisableSubmit = false;
        }
        catch (Exception exception)
        {
            ErrorMessages = new List<string> { exception.Message };
            ShowError = true;
            DisableSubmit = false;
        }
    }

    private async Task HandleFileSelected(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file != null)
        {
            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffer);
            var content = Encoding.UTF8.GetString(buffer);
            ParseCsv(content);
        }
    }

    private IEnumerable<CreateContactRequest> ParseCsv(string csvContent)
    {
        var importContacts = new List<CreateContactRequest>();
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        using var reader = new StringReader(csvContent);
        using (var csv = new CsvReader(reader, config))
        {
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var record = new CreateContactRequest
                {
                    FirstName = csv.GetField("First Name") ?? string.Empty,
                    MiddleName = csv.GetField("Middle Name") ?? string.Empty,
                    LastName = csv.GetField("Last Name") ?? string.Empty,
                    Phone = csv.GetField("Mobile Phone") ?? string.Empty,
                    Email = csv.GetField("E-mail Address") ?? string.Empty
                };

                if (!string.IsNullOrEmpty(record.FirstName) && !string.IsNullOrEmpty(record.FirstName))
                {
                    var bDay = csv.GetField("Birthday");
                    if (!string.IsNullOrEmpty(bDay))
                    {
                        if (DateTime.TryParse(bDay, out DateTime bDayDate))
                        {
                            record.DateOfBirth = bDayDate;
                        }
                    }

                    importContacts.Add(record);
                }
            }
        }

        return importContacts.Any() ? importContacts : Enumerable.Empty<CreateContactRequest>();
    }
}
