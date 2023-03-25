using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Teban.Application.Models;
using Teban.Application.Persistence;

namespace Teban.Application.Services;
public class CommunicationScheduleService : ICommunicationScheduleService
{
    private readonly IValidator<CommunicationSchedule> _scheduleValidator;
    private readonly ApplicationDbContext _context;

    public CommunicationScheduleService(IValidator<CommunicationSchedule> scheduleValidator, ApplicationDbContext context)
    {
        _scheduleValidator = scheduleValidator;
        _context = context;
    }

    public async Task<bool> CreateAsync(CommunicationSchedule schedule, CancellationToken token = default)
    {
        await _scheduleValidator.ValidateAndThrowAsync(schedule, token);
        _context.CommunicationSchedules.Add(schedule);
        var createResult = await _context.SaveChangesAsync(token);

        return createResult >= 0;
    }

    public async Task<CommunicationSchedule?> GetByIdAsync(int id, string userId, CancellationToken token = default)
    {
        return await _context.CommunicationSchedules
            .Where(x => x.CommunicationScheduleId == id
                && x.TebanUserId == userId)
            .FirstOrDefaultAsync(token);
    }

    public async Task<IEnumerable<CommunicationSchedule>> GetAllAsync(string userId, CancellationToken token = default)
    {
        return await _context.CommunicationSchedules
            .Where(x => x.TebanUserId == userId)
            .ToListAsync(token);
    }

    public async Task<CommunicationSchedule?> UpdateAsync(CommunicationSchedule schedule, string userId, CancellationToken token = default)
    {
        await _scheduleValidator.ValidateAndThrowAsync(schedule, token);
        var existingSchedule = await _context.CommunicationSchedules
            .FindAsync(new object[] { schedule.CommunicationScheduleId }, token);

        if (existingSchedule is null)
        {
            return null;
        }

        existingSchedule.StartDate = schedule.StartDate;
        existingSchedule.Frequency = schedule.Frequency;
        existingSchedule.ContactId = schedule.ContactId;
        existingSchedule.TebanUserId = userId;

        _context.CommunicationSchedules.Entry(existingSchedule).State = EntityState.Modified;
        var updateResult = await _context.SaveChangesAsync(token);

        return updateResult > 0 ? schedule : null;
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken token = default)
    {
        var existingSchedule = await _context.CommunicationSchedules
            .FindAsync(new object[] { id }, token);

        if (existingSchedule is null)
        {
            return false;
        }

        _context.CommunicationSchedules.Remove(existingSchedule);
        var deleteResult = await _context.SaveChangesAsync(token);
        return deleteResult > 0;
    }
}
