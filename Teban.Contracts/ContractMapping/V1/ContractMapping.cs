using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Responses.V1.Contacts;

namespace Teban.Contracts.ContractMapping.V1;
public static class ContractMapping
{
    public static UpdateContactRequest MapToUpdateContactRequest(this ContactResponse contact)
    {
        return new UpdateContactRequest
        {
            FirstName = contact.FirstName,
            MiddleName = contact.MiddleName,
            LastName = contact.LastName,
            Phone = contact.Phone,
            Email = contact.Email,
            PhotoUrl = contact.PhotoUrl,
            DateOfBirth = contact.DateOfBirth,
            TebanUserId = contact.TebanUserId,
            Frequency = contact.Frequency,
            StartDate = contact.StartDate
        };
    }
}
