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
    public class TransactionEntriesService
    {
        private readonly HttpClient _httpClient;

        public TransactionEntriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestResponseDto<TransactionEntry>> GetTransactionEntry(int id)
        {
            RequestResponseDto<TransactionEntry>? response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<RequestResponseDto<TransactionEntry>>($"api/v1/transactionEntries/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<TransactionEntry>.Failure(new string[] { ex.Message });
            }

            if (response is null)
            {
                return RequestResponseDto<TransactionEntry>.Failure(new string[] { "There was an error retrieving the entry." });
            }

            return response;
        }

        public async Task<RequestResponseDto<TransactionEntry>> PostTransactionEntry(TransactionEntry transactionEntry)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync("api/v1/transactionEntries", transactionEntry);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<TransactionEntry>.Failure(new string[] { ex.Message });
            }

            var result = await response.Content.ReadFromJsonAsync<RequestResponseDto<TransactionEntry>>();

            if (result is null)
            {
                return RequestResponseDto<TransactionEntry>.Failure(new string[] { "There was an error creating the entry." });
            }

            return result;
        }

        public async Task<RequestResponseDto<int>> PutTransactionEntry(int id, TransactionEntry transactionEntry)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PutAsJsonAsync($"api/v1/transactionEntries/{id}", transactionEntry);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error updating the entry." });
            }

            return RequestResponseDto<int>.Success(1);
        }

        public async Task<RequestResponseDto<int>> DeleteTransactionEntry(int id)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.DeleteAsync($"api/v1/transactionEntries/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error deleting the entry." });
            }

            return RequestResponseDto<int>.Success(1);
        }
    }
}
