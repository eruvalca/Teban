using Teban.Application.Common;
using Teban.Contracts.Enums;

namespace Teban.Application.Models;
public class CommunicationSchedule : BaseAuditableEntity
{
    public int CommunicationScheduleId { get; set; }
    public required Frequency Frequency { get; set; }
    public required DateTime StartDate { get; set; }

    public required string TebanUserId { get; set; }
    public required TebanUser TebanUser { get; set; }

    public int ContactId { get; set; }
    public required Contact Contact { get; set; }
}
