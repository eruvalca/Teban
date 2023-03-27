namespace Teban.Contracts.Responses.V1.CommunicationSchedules;
public class CommunicationScheduleResponse
{
    public int CommunicationScheduleId { get; set; }
    public required string Frequency { get; set; }
    public required DateTime StartDate { get; set; }
    public string TebanUserId { get; set; } = string.Empty;
    public int ContactId { get; set; }
}
