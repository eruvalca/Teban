namespace Teban.Contracts.Responses.V1.Contacts;
public class ContactsResponse
{
    public required IEnumerable<ContactResponse> Items { get; set; } = Enumerable.Empty<ContactResponse>();
}
