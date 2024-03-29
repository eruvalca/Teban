﻿using FluentValidation;
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
        return await _context.SaveChangesAsync(cToken) > 0;
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
            .Include(x => x.CommunicationSchedule)
            .FirstOrDefaultAsync(x => x.ContactId == contact.ContactId, cancellationToken: cToken);

        if (existingContact is null)
        {
            return null;
        }

        if (contact.CommunicationSchedule is not null)
        {
            if (existingContact.CommunicationSchedule is not null)
            {
                await _communicationScheduleValidator.ValidateAndThrowAsync(contact.CommunicationSchedule, cToken);
                existingContact.CommunicationSchedule.Frequency = contact.CommunicationSchedule.Frequency;
                existingContact.CommunicationSchedule.StartDate = contact.CommunicationSchedule.StartDate;
                _context.CommunicationSchedules.Entry(existingContact.CommunicationSchedule).State = EntityState.Modified;
            }
            else
            {
                var newCommunicationSchedule = new CommunicationSchedule
                {
                    Frequency = contact.CommunicationSchedule.Frequency,
                    StartDate = contact.CommunicationSchedule.StartDate,
                    ContactId = existingContact.ContactId,
                    TebanUserId = contact.TebanUserId
                };
                _context.CommunicationSchedules.Add(newCommunicationSchedule);
                existingContact.CommunicationSchedule = newCommunicationSchedule;
            }
        }
        else
        {
            if (existingContact.CommunicationSchedule is not null)
            {
                _context.CommunicationSchedules.Remove(existingContact.CommunicationSchedule);
            }
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

        _context.Contacts.Remove(existingContact);
        var deleteResult = await _context.SaveChangesAsync(cToken);
        return deleteResult > 0;
    }

    public async Task<bool> ImportAsync(IEnumerable<Contact> contacts, CancellationToken cToken = default)
    {
        foreach (var contact in contacts)
        {
            await _contactValidator.ValidateAndThrowAsync(contact, cancellationToken: cToken);
        }

        await _context.Contacts.AddRangeAsync(contacts);
        return await _context.SaveChangesAsync(cToken) > 0;
    }

    public async Task<bool> BulkDeleteAsync(IEnumerable<int> contactIds, CancellationToken cToken = default)
    {
        var existingContacts = await _context.Contacts
            .Where(x => contactIds.Contains(x.ContactId))
            .ToListAsync(cancellationToken: cToken);

        if (!existingContacts.Any() || existingContacts.Count != contactIds.Count())
        {
            return false;
        }

        _context.Contacts.RemoveRange(existingContacts);
        return await _context.SaveChangesAsync(cToken) > 0;
    }
}
