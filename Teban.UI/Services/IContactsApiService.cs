using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Responses.V1.Contacts;

namespace Teban.UI.Services;
public interface IContactsApiService
{
    Task<ContactResponse?> CreateContactAsync(CreateContactRequest request);

    Task<ContactResponse> GetContactAsync(int id);

    Task<ContactsResponse> GetContactsAsync();

    Task<ContactResponse> UpdateContactAsync(int id, UpdateContactRequest request);

    Task DeleteContactAsync(int id);

    Task<ContactsResponse> ImportContactsAsync(ImportContactsRequest request);
}
