using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Teban.Application.Dtos.Request;
using Teban.Domain.Entities;

namespace Teban.UI.Services
{
    public class BudgetsService
    {
        private readonly HttpClient _httpClient;

        public BudgetsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestResponseDto<IEnumerable<Budget>>> GetBudgets()
        {
            RequestResponseDto<IEnumerable<Budget>>? response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<RequestResponseDto<IEnumerable<Budget>>>("api/v1/budgets");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<IEnumerable<Budget>>.Failure(new string[] { ex.Message });
            }

            if (response is null)
            {
                return RequestResponseDto<IEnumerable<Budget>>.Failure(new string[] { "There was an error retrieving the user's budgets." });
            }

            return response;
        }

        public async Task<RequestResponseDto<Budget>> GetBudget(int id)
        {
            RequestResponseDto<Budget>? response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<RequestResponseDto<Budget>>($"api/v1/budgets/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<Budget>.Failure(new string[] { ex.Message });
            }

            if (response is null)
            {
                return RequestResponseDto<Budget>.Failure(new string[] { "There was an error retrieving the budget." });
            }

            return response;
        }

        public async Task<RequestResponseDto<Budget>> PostBudget(Budget budget)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync("api/v1/budgets", budget);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<Budget>.Failure(new string[] { ex.Message });
            }

            var result = await response.Content.ReadFromJsonAsync<RequestResponseDto<Budget>>();

            if (result is null)
            {
                return RequestResponseDto<Budget>.Failure(new string[] { "There was an error creating the budget." });
            }

            return result;
        }

        public async Task<RequestResponseDto<int>> PutBudget(int id, Budget budget)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PutAsJsonAsync($"api/v1/budgets/{id}", budget);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error updating the budget." });
            }

            return RequestResponseDto<int>.Success(1);
        }

        public async Task<RequestResponseDto<int>> DeleteBudget(int id)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.DeleteAsync($"api/v1/budgets/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error deleting the budget." });
            }

            return RequestResponseDto<int>.Success(1);
        }
    }
}
