using Refit;
using Teban.Contracts.Requests.V1.CommunicationSchedules;
using Teban.Contracts.Responses.V1.CommunicationSchedules;

namespace Teban.Api.Sdk;

[Headers("Authorization: Bearer")]
public interface ICommunicationSchedulesApi
{
    [Post(ApiEndpoints.CommunicationSchedules.Create)]
    Task<CommunicationScheduleResponse> CreateCommunicationScheduleAsync(CreateCommunicationScheduleRequest request);

    [Get(ApiEndpoints.CommunicationSchedules.Get)]
    Task<CommunicationScheduleResponse> GetCommunicationScheduleAsync(int id);

    [Get(ApiEndpoints.CommunicationSchedules.GetAll)]
    Task<CommunicationSchedulesResponse> GetCommunicationSchedulesAsync();

    [Put(ApiEndpoints.CommunicationSchedules.Update)]
    Task<CommunicationScheduleResponse> UpdateCommunicationScheduleAsync(int id, UpdateCommunicationScheduleRequest request);

    [Delete(ApiEndpoints.CommunicationSchedules.Delete)]
    Task DeleteCommunicationScheduleAsync(int id);
}
