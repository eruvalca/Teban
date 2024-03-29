﻿using Teban.Application.Models;

namespace Teban.Application.Services;
public interface IContactService
{
    Task<bool> CreateAsync(Contact contact, CancellationToken token = default);
    Task<Contact?> GetByIdAsync(int id, string userId, CancellationToken token = default);
    Task<IEnumerable<Contact>> GetAllAsync(string userIds, CancellationToken token = default);
    Task<Contact?> UpdateAsync(Contact contact, string userId, CancellationToken token = default);
    Task<bool> DeleteByIdAsync(int id, CancellationToken token = default);
    Task<bool> ImportAsync(IEnumerable<Contact> contacts, CancellationToken cToken = default);
    Task<bool> BulkDeleteAsync(IEnumerable<int> contactIds, CancellationToken cToken = default);
}
