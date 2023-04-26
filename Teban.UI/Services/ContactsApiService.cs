using Refit;
using System.Text.Json;
using Teban.Api.Sdk;
using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Responses.V1;
using Teban.Contracts.Responses.V1.Contacts;

namespace Teban.UI.Services;
public class ContactsApiService : IContactsApiService
{
    private readonly IContactsApi _contactsApi;

    public ContactsApiService(IContactsApi contactsApi)
    {
        _contactsApi = contactsApi;
    }

    public async Task<ContactResponse?> CreateContactAsync(CreateContactRequest request)
    {
        ContactResponse response;

        try
        {
            response = await _contactsApi.CreateContactAsync(request);
        }
        catch (ApiException apiException)
        {
            var validationResponse = JsonSerializer.Deserialize<ValidationFailureResponse>(apiException.Content!, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            })!;

            if (validationResponse.Errors.Any())
            {
                throw new ValidationFailureException(validationResponse);
            }

            throw;
        }

        return response;
    }

    public async Task<ContactResponse> GetContactAsync(int id)
    {
        return await _contactsApi.GetContactAsync(id);
    }

    public async Task<ContactsResponse> GetContactsAsync()
    {
        return await _contactsApi.GetContactsAsync();
    }

    public async Task<ContactResponse> UpdateContactAsync(int id, UpdateContactRequest request)
    {
        ContactResponse response;

        try
        {
            response = await _contactsApi.UpdateContactAsync(id, request);
        }
        catch (ApiException apiException)
        {
            var validationResponse = JsonSerializer.Deserialize<ValidationFailureResponse>(apiException.Content!, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            })!;

            if (validationResponse.Errors.Any())
            {
                throw new ValidationFailureException(validationResponse);
            }

            throw;
        }

        return response;
    }

    public async Task DeleteContactAsync(int id)
    {
        await _contactsApi.DeleteContactAsync(id);
    }

    public async Task<ContactsResponse> ImportContactsAsync(ImportContactsRequest request)
    {
        ContactsResponse response;

        try
        {
            response = await _contactsApi.ImportContactsAsync(request);
        }
        catch (ApiException apiException)
        {
            var validationResponse = JsonSerializer.Deserialize<ValidationFailureResponse>(apiException.Content!, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            })!;

            if (validationResponse.Errors.Any())
            {
                throw new ValidationFailureException(validationResponse);
            }

            throw;
        }

        return response;
    }

    public async Task BulkDeleteContactsAsync(IEnumerable<int> ids)
    {
        await _contactsApi.BulkDeleteContactsAsync(ids);
    }
}
