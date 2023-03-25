using Teban.Contracts.Enums;

namespace Teban.Contracts.Requests.V1.CommunicationSchedules;
public class CreateCommunicationScheduleRequest
{
    public required Frequency Frequency { get; set; }
    public required DateTime StartDate { get; set; }
    public string TebanUserId { get; set; } = string.Empty;
    public int ContactId { get; set; }
}
