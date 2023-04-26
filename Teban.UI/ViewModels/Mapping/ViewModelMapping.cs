using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Contracts.Responses.V1.Contacts;
using static Teban.UI.Pages.Contacts.AllContacts;

namespace Teban.UI.ViewModels.Mapping;
public static class ViewModelMapping
{
    public static ContactCardViewModel MapToContactCardViewModel(this ContactResponse contact)
    {
        return new ContactCardViewModel
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
            Frequency = contact.Frequency,
            StartDate = contact.StartDate,
            IsSelected = false
        };
    }
}
