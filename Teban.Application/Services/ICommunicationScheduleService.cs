using Teban.Application.Models;

namespace Teban.Application.Services;
public interface ICommunicationScheduleService
{
    Task<bool> CreateAsync(CommunicationSchedule schedule, CancellationToken token = default);
    Task<CommunicationSchedule?> GetByIdAsync(int id, string userId, CancellationToken token = default);
    Task<IEnumerable<CommunicationSchedule>> GetAllAsync(string userId, CancellationToken token = default);
    Task<CommunicationSchedule?> UpdateAsync(CommunicationSchedule schedule, string userId, CancellationToken token = default);
    Task<bool> DeleteByIdAsync(int id, CancellationToken token = default);
}
