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
    public class AccountTransactionsService
    {
        private readonly HttpClient _httpClient;

        public AccountTransactionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestResponseDto<AccountTransaction>> GetAccountTransaction(int id)
        {
            RequestResponseDto<AccountTransaction>? response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<RequestResponseDto<AccountTransaction>>($"api/v1/accountTransactions/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<AccountTransaction>.Failure(new string[] { ex.Message });
            }

            if (response is null)
            {
                return RequestResponseDto<AccountTransaction>.Failure(new string[] { "There was an error retrieving the transaction." });
            }

            return response;
        }

        public async Task<RequestResponseDto<AccountTransaction>> PostAccountTransaction(AccountTransaction accountTransaction)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync("api/v1/accountTransactions", accountTransaction);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<AccountTransaction>.Failure(new string[] { ex.Message });
            }

            var result = await response.Content.ReadFromJsonAsync<RequestResponseDto<AccountTransaction>>();

            if (result is null)
            {
                return RequestResponseDto<AccountTransaction>.Failure(new string[] { "There was an error creating the transaction." });
            }

            return result;
        }

        public async Task<RequestResponseDto<int>> PutAccountTransaction(int id, AccountTransaction accountTransaction)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PutAsJsonAsync($"api/v1/accountTransactions/{id}", accountTransaction);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error updating the transaction." });
            }

            return RequestResponseDto<int>.Success(1);
        }

        public async Task<RequestResponseDto<int>> DeleteAccountTransaction(int id)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.DeleteAsync($"api/v1/accountTransactions/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error deleting the transaction." });
            }

            return RequestResponseDto<int>.Success(1);
        }
    }
}
