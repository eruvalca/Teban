using Teban.Application.Models;
using Teban.Contracts.Requests.V1.CommunicationSchedules;
using Teban.Contracts.Requests.V1.Contacts;
using Teban.Contracts.Requests.V1.Identity;
using Teban.Contracts.Responses.V1.CommunicationSchedules;
using Teban.Contracts.Responses.V1.Contacts;

namespace Teban.Api.Mapping;

public static class ContractMapping
{
    public static TebanUser MapToTebanUser(this RegisterRequest registerRequest)
    {
        return new TebanUser
        {
            Email = registerRequest.Email,
            UserName = registerRequest.Email,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName
        };
    }

    public static Contact MapToContact(this CreateContactRequest createContactRequest)
    {
        return new Contact
        {
            FirstName = createContactRequest.FirstName,
            MiddleName = createContactRequest.MiddleName,
            LastName = createContactRequest.LastName,
            Phone = createContactRequest.Phone,
            Email = createContactRequest.Email,
            PhotoUrl = createContactRequest.PhotoUrl,
            DateOfBirth = createContactRequest.DateOfBirth,
            TebanUserId = createContactRequest.TebanUserId,
            CommunicationSchedule =
            createContactRequest.Frequency is not null && createContactRequest.StartDate is not null
            ? new CommunicationSchedule
            {
                Frequency = createContactRequest.Frequency,
                StartDate = (DateTime)createContactRequest.StartDate,
                TebanUserId = createContactRequest.TebanUserId
            }
            : null
        };
    }

    public static ContactResponse MapToResponse(this Contact contact)
    {
        return new ContactResponse
        {
            ContactId = contact.ContactId,
            FirstName = contact.FirstName,
            MiddleName = contact.MiddleName,
            LastName = contact.LastName,
            Phone = contact.Phone,
            Email = contact.Email,
            PhotoUrl = contact.PhotoUrl,
            DateOfBirth = contact.DateOfBirth,
            TebanUserId = contact.TebanUserId,
            Frequency = contact.CommunicationSchedule?.Frequency,
            StartDate = contact.CommunicationSchedule?.StartDate
        };
    }

    public static ContactsResponse MapToResponse(this IEnumerable<Contact> contacts)
    {
        return new ContactsResponse
        {
            Items = contacts.Select(MapToResponse)
        };
    }

    public static Contact MapToContact(this UpdateContactRequest updateContactRequest, int id)
    {
        return new Contact
        {
            ContactId = id,
            FirstName = updateContactRequest.FirstName,
            MiddleName = updateContactRequest.MiddleName,
            LastName = updateContactRequest.LastName,
            Phone = updateContactRequest.Phone,
            Email = updateContactRequest.Email,
            PhotoUrl = updateContactRequest.PhotoUrl,
            DateOfBirth = updateContactRequest.DateOfBirth,
            TebanUserId = updateContactRequest.TebanUserId,
            CommunicationSchedule =
            updateContactRequest.Frequency is not null && updateContactRequest.StartDate is not null
            ? new CommunicationSchedule
            {
                Frequency = updateContactRequest.Frequency,
                StartDate = (DateTime)updateContactRequest.StartDate,
                TebanUserId = updateContactRequest.TebanUserId,
                ContactId = id
            }
            : null
        };
    }

    public static CommunicationSchedule MapToCommunicationSchedule(this CreateCommunicationScheduleRequest request)
    {
        return new CommunicationSchedule
        {
            Frequency = request.Frequency,
            StartDate = request.StartDate,
            TebanUserId = request.TebanUserId,
            ContactId = request.ContactId
        };
    }

    public static CommunicationScheduleResponse MapToResponse(this CommunicationSchedule schedule)
    {
        return new CommunicationScheduleResponse
        {
            CommunicationScheduleId = schedule.CommunicationScheduleId,
            Frequency = schedule.Frequency,
            StartDate = schedule.StartDate,
            TebanUserId = schedule.TebanUserId,
            ContactId = schedule.ContactId
        };
    }

    public static CommunicationSchedulesResponse MapToResponse(this IEnumerable<CommunicationSchedule> schedules)
    {
        return new CommunicationSchedulesResponse
        {
            Items = schedules.Select(MapToResponse)
        };
    }

    public static CommunicationSchedule MapToCommunicationSchedule(this UpdateCommunicationScheduleRequest updateScheduleRequest, int id)
    {
        return new CommunicationSchedule
        {
            CommunicationScheduleId = id,
            Frequency = updateScheduleRequest.Frequency,
            StartDate = updateScheduleRequest.StartDate,
            TebanUserId = updateScheduleRequest.TebanUserId,
            ContactId = updateScheduleRequest.ContactId
        };
    }

    public static IEnumerable<Contact> MapToContacts(this ImportContactsRequest importContactRequest)
    {
        return importContactRequest.Contacts.Select(MapToContact);
    }
}
