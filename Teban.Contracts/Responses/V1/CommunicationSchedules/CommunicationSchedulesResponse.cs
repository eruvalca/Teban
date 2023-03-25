namespace Teban.Contracts.Responses.V1.CommunicationSchedules;
public class CommunicationSchedulesResponse
{
    public required IEnumerable<CommunicationScheduleResponse> Items { get; set; } = Enumerable.Empty<CommunicationScheduleResponse>();

}
