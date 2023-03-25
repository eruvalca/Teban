using Refit;
using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Responses.V1.Contacts;

namespace Teban.Api.Sdk;

[Headers("Authorization: Bearer")]
public interface IContactsApi
{
    [Post(ApiEndpoints.Contacts.Create)]
    Task<ContactResponse> CreateContactAsync(CreateContactRequest request);

    [Get(ApiEndpoints.Contacts.Get)]
    Task<ContactResponse> GetContactAsync(int id);

    [Get(ApiEndpoints.Contacts.GetAll)]
    Task<ContactsResponse> GetContactsAsync();

    [Put(ApiEndpoints.Contacts.Update)]
    Task<ContactResponse> UpdateContactAsync(int id, UpdateContactRequest request);

    [Delete(ApiEndpoints.Contacts.Delete)]
    Task DeleteContactAsync(int id);
}
