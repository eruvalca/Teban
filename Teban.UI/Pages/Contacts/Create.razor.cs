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

    private string TebanUserId { get; set; } = string.Empty;

    private List<string> ErrorMessages { get; set; } = new List<string>();
    private bool ShowError { get; set; } = false;
    private bool DisableSubmit { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        TebanUserId = await IdentityService.GetUserId();
    }

    private async Task HandleSubmit()
    {
        CreateRequest.TebanUserId = TebanUserId;

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

    private async Task SubmitImport(ImportContactsRequest importContactsRequest)
    {
        DisableSubmit = true;
        ShowError = false;

        ContactsResponse? response;

        try
        {
            response = await ContactsApiService.ImportContactsAsync(importContactsRequest);
            Navigation.NavigateTo("/contacts");
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
            var importContacts = ParseCsv(content);

            if (importContacts.Any())
            {
                var importContactsRequest = new ImportContactsRequest
                {
                    Contacts = importContacts
                };
                await SubmitImport(importContactsRequest);
            }
        }
    }

    private IEnumerable<CreateContactRequest> ParseCsv(string csvContent)
    {
        var importContacts = new List<CreateContactRequest>();
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            MissingFieldFound = null
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
                    FirstName = csv.GetField(0) ?? string.Empty,
                    MiddleName = csv.GetField(1) ?? string.Empty,
                    LastName = csv.GetField(2) ?? string.Empty,
                    Phone = csv.GetField(3) ?? string.Empty,
                    Email = csv.GetField(4) ?? string.Empty,
                    TebanUserId = TebanUserId
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
