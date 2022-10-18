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
    public class AccountsService
    {
        private readonly HttpClient _httpClient;

        public AccountsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestResponseDto<IEnumerable<Account>>> GetAccountsByBudget(int id)
        {
            RequestResponseDto<IEnumerable<Account>>? response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<RequestResponseDto<IEnumerable<Account>>>($"accounts/budget/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<IEnumerable<Account>>.Failure(new string[] { ex.Message });
            }

            if (response is null)
            {
                return RequestResponseDto<IEnumerable<Account>>.Failure(new string[] { "There was an error retrieving the accounts." });
            }

            return response;
        }

        public async Task<RequestResponseDto<Account>> GetAccount(int id)
        {
            RequestResponseDto<Account>? response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<RequestResponseDto<Account>>($"accounts/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<Account>.Failure(new string[] { ex.Message });
            }

            if (response is null)
            {
                return RequestResponseDto<Account>.Failure(new string[] { "There was an error retrieving the account." });
            }

            return response;
        }

        public async Task<RequestResponseDto<Account>> PostAccount(Account account)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync("accounts", account);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<Account>.Failure(new string[] { ex.Message });
            }

            var result = await response.Content.ReadFromJsonAsync<RequestResponseDto<Account>>();

            if (result is null)
            {
                return RequestResponseDto<Account>.Failure(new string[] { "There was an error creating the account." });
            }

            return result;
        }

        public async Task<RequestResponseDto<int>> PutAccount(int id, Account account)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PutAsJsonAsync($"accounts/{id}", account);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error updating the account." });
            }

            return RequestResponseDto<int>.Success(1);
        }

        public async Task<RequestResponseDto<int>> DeleteAccount(int id)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.DeleteAsync($"accounts/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error deleting the account." });
            }

            return RequestResponseDto<int>.Success(1);
        }
    }
}
