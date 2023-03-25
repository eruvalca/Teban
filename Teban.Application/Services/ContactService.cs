using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Teban.Application.Models;
using Teban.Application.Persistence;

namespace Teban.Application.Services;
public class ContactService : IContactService
{
    private readonly IValidator<Contact> _contactValidator;
    private readonly IValidator<CommunicationSchedule> _communicationScheduleValidator;
    private readonly ApplicationDbContext _context;

    public ContactService(IValidator<Contact> contactValidator, ApplicationDbContext context, IValidator<CommunicationSchedule> communicationScheduleValidator)
    {
        _contactValidator = contactValidator;
        _context = context;
        _communicationScheduleValidator = communicationScheduleValidator;
    }

    public async Task<bool> CreateAsync(Contact contact, CancellationToken cToken = default)
    {
        await _contactValidator.ValidateAndThrowAsync(contact, cancellationToken: cToken);
        _context.Contacts.Add(contact);
        var createResult = await _context.SaveChangesAsync(cToken);

        if (createResult <= 0)
        {
            return false;
        }

        if (contact.CommunicationSchedule is not null)
        {
            contact.CommunicationSchedule.TebanUserId = contact.TebanUserId;
            contact.CommunicationSchedule.ContactId = contact.CommunicationSchedule.ContactId;
            await _communicationScheduleValidator.ValidateAndThrowAsync(contact.CommunicationSchedule, cToken);
            _context.CommunicationSchedules.Add(contact.CommunicationSchedule);

            createResult = await _context.SaveChangesAsync(cToken);
        }

        return createResult > 0;
    }

    public async Task<Contact?> GetByIdAsync(int id, string userId, CancellationToken cToken = default)
    {
        return await _context.Contacts
            .Where(x => x.ContactId == id
                && x.TebanUserId == userId)
            .Include(x => x.CommunicationSchedule)
            .FirstOrDefaultAsync(cancellationToken: cToken);
    }

    public async Task<IEnumerable<Contact>> GetAllAsync(string userId, CancellationToken cToken = default)
    {
        return await _context.Contacts
            .Where(x => x.TebanUserId == userId)
            .Include(x => x.CommunicationSchedule)
            .ToListAsync(cancellationToken: cToken);
    }

    public async Task<Contact?> UpdateAsync(Contact contact, string userId, CancellationToken cToken = default)
    {
        await _contactValidator.ValidateAndThrowAsync(contact, cancellationToken: cToken);
        var existingContact = await _context.Contacts
            .FindAsync(new object[] { contact.ContactId }, cancellationToken: cToken);

        if (existingContact is null)
        {
            return null;
        }

        if (contact.CommunicationSchedule is not null)
        {
            await _communicationScheduleValidator.ValidateAndThrowAsync(contact.CommunicationSchedule, cToken);
            var existingCommunicationSchedules = _context.CommunicationSchedules
                .Where(x => x.ContactId == contact.ContactId);

            if (await existingCommunicationSchedules.AnyAsync(cancellationToken: cToken))
            {
                _context.CommunicationSchedules.RemoveRange(existingCommunicationSchedules);
            }

            _context.CommunicationSchedules.Add(contact.CommunicationSchedule);
        }

        existingContact.FirstName = contact.FirstName;
        existingContact.MiddleName = contact.MiddleName;
        existingContact.LastName = contact.LastName;
        existingContact.Phone = contact.Phone;
        existingContact.Email = contact.Email;
        existingContact.PhotoUrl = contact.PhotoUrl;
        existingContact.DateOfBirth = contact.DateOfBirth;
        existingContact.TebanUserId = contact.TebanUserId;

        _context.Contacts.Entry(existingContact).State = EntityState.Modified;
        var updateResult = await _context.SaveChangesAsync(cToken);

        return updateResult > 0 ? contact : null;
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cToken = default)
    {
        var existingContact = await _context.Contacts
            .FindAsync(new object[] { id }, cancellationToken: cToken);

        if (existingContact is null)
        {
            return false;
        }

        var contactCommunicationSchedules = _context.CommunicationSchedules
                .Where(x => x.ContactId == id);

        if (await contactCommunicationSchedules.AnyAsync(cancellationToken: cToken))
        {
            _context.CommunicationSchedules.RemoveRange(contactCommunicationSchedules);
        }

        _context.Contacts.Remove(existingContact);
        var deleteResult = await _context.SaveChangesAsync(cToken);
        return deleteResult > 0;
    }
}
