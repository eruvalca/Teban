using Teban.Application.Common;

namespace Teban.Application.Models;
public class CommunicationSchedule : BaseAuditableEntity
{
    public int CommunicationScheduleId { get; set; }
    public required string Frequency { get; set; }
    public required DateTime StartDate { get; set; }

    public string TebanUserId { get; set; }

    public int ContactId { get; set; }
}
