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
    public class CategoryGroupsService
    {
        private readonly HttpClient _httpClient;

        public CategoryGroupsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestResponseDto<CategoryGroup>> GetCategoryGroup(int id)
        {
            RequestResponseDto<CategoryGroup>? response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<RequestResponseDto<CategoryGroup>>($"api/v1/categoryGroups/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<CategoryGroup>.Failure(new string[] { ex.Message });
            }

            if (response is null)
            {
                return RequestResponseDto<CategoryGroup>.Failure(new string[] { "There was an error retrieving the category group." });
            }

            return response;
        }

        public async Task<RequestResponseDto<CategoryGroup>> PostCategoryGroup(CategoryGroup categoryGroup)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync("api/v1/categoryGroups", categoryGroup);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<CategoryGroup>.Failure(new string[] { ex.Message });
            }

            var result = await response.Content.ReadFromJsonAsync<RequestResponseDto<CategoryGroup>>();

            if (result is null)
            {
                return RequestResponseDto<CategoryGroup>.Failure(new string[] { "There was an error creating the category group." });
            }

            return result;
        }

        public async Task<RequestResponseDto<int>> PutCategoryGroup(int id, CategoryGroup categoryGroup)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PutAsJsonAsync($"api/v1/categoryGroups/{id}", categoryGroup);
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error updating the category group." });
            }

            return RequestResponseDto<int>.Success(1);
        }

        public async Task<RequestResponseDto<int>> DeleteCategoryGroup(int id)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.DeleteAsync($"api/v1/categoryGroups/{id}");
            }
            catch (Exception ex)
            {
                return RequestResponseDto<int>.Failure(new string[] { ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                return RequestResponseDto<int>.Failure(new string[] { "There was an error deleting the category group." });
            }

            return RequestResponseDto<int>.Success(1);
        }
    }
}
