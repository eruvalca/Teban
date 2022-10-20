using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Teban.Application.Dtos.Request;
using Teban.Domain.Entities;

namespace Teban.UI.Services
{
    public class MonthlyCategoryBudgetsService
    {
        private readonly HttpClient _httpClient;

        public MonthlyCategoryBudgetsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestResponseDto<MonthlyCategoryBudget>> GetMonthlyCategoryBudget(int id)
        {
            RequestResponseDto<MonthlyCategoryBudget>? response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<RequestResponseDto<MonthlyCategoryBudget>>($"monthlyCategoryBudgets/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<MonthlyCategoryBudget>.Failure(new string[] { ex.Message });
            }

            if (response is null)
            {
                return RequestResponseDto<MonthlyCategoryBudget>.Failure(new string[] { "There was an error retrieving the monthly category budget." });
            }

            return response;
        }

        public async Task<RequestResponseDto<MonthlyCategoryBudget>> PostMonthlyCategoryBudget(MonthlyCategoryBudget monthlyCategoryBudget)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync("monthlyCategoryBudgets", monthlyCategoryBudget);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<MonthlyCategoryBudget>.Failure(new string[] { ex.Message });
            }

            var result = await response.Content.ReadFromJsonAsync<RequestResponseDto<MonthlyCategoryBudget>>();

            if (result is null)
            {
                return RequestResponseDto<MonthlyCategoryBudget>.Failure(new string[] { "There was an error creating the monthly category budget." });
            }

            return result;
        }

        public async Task<RequestResponseDto<int>> PutMonthlyCategoryBudget(int id, MonthlyCategoryBudget monthlyCategoryBudget)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PutAsJsonAsync($"monthlyCategoryBudgets/{id}", monthlyCategoryBudget);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error updating the monthly category budget." });
            }

            return RequestResponseDto<int>.Success(1);
        }

        public async Task<RequestResponseDto<int>> DeleteMonthlyCategoryBudget(int id)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.DeleteAsync($"monthlyCategoryBudgets/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error deleting the monthly category budget." });
            }

            return RequestResponseDto<int>.Success(1);
        }
    }
}
