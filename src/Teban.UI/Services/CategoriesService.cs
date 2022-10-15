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
    public class CategoriesService
    {
        private readonly HttpClient _httpClient;

        public CategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestResponseDto<Category>> GetCategory(int id)
        {
            RequestResponseDto<Category>? response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<RequestResponseDto<Category>>($"categories/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<Category>.Failure(new string[] { ex.Message });
            }

            if (response is null)
            {
                return RequestResponseDto<Category>.Failure(new string[] { "There was an error retrieving the category." });
            }

            return response;
        }

        public async Task<RequestResponseDto<Category>> PostCategory(Category category)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync("categories", category);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<Category>.Failure(new string[] { ex.Message });
            }

            var result = await response.Content.ReadFromJsonAsync<RequestResponseDto<Category>>();

            if (result is null)
            {
                return RequestResponseDto<Category>.Failure(new string[] { "There was an error creating the category." });
            }

            return result;
        }

        public async Task<RequestResponseDto<int>> PutCategory(int id, Category category)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PutAsJsonAsync($"categories/{id}", category);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error updating the category." });
            }

            return RequestResponseDto<int>.Success(1);
        }

        public async Task<RequestResponseDto<int>> DeleteCategory(int id)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.DeleteAsync($"categories/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error deleting the category." });
            }

            return RequestResponseDto<int>.Success(1);
        }
    }
}
