using Teban.Application.Common;
using Teban.Contracts.Enums;

namespace Teban.Application.Models;
public class CommunicationSchedule : BaseAuditableEntity
{
    public int CommunicationScheduleId { get; set; }
    public required Frequency Frequency { get; set; }
    public required DateTime StartDate { get; set; }

    public string TebanUserId { get; set; }

    public int ContactId { get; set; }
}
