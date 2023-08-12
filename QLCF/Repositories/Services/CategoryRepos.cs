using QLCF.Repositories.Interfaces;
using QuanLyCF.BLL.ViewModels.Category;
using QuanLyCF.BLL.ViewModels.Food;
using System.Net.Http.Json;

namespace QLCF.Repositories.Services
{
    public class CategoryRepos : ICategoryRepos
    {
        private readonly HttpClient _httpClient;
        public CategoryRepos(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> Add(string CategoryName)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Categories", CategoryName);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid CategoryId)
        {
            var result = await _httpClient.DeleteAsync($"api/Categories/delete/{CategoryId}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<CategoryVM>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryVM>>($"api/Categories/all");
        }

        public async Task<CategoryVM> GetById(Guid categoryId)
        {
            return await _httpClient.GetFromJsonAsync<CategoryVM>($"api/Categories/getbyid/{categoryId}");
        }

        public async Task<bool> Update(Guid CategoryId, CategoryVM request)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Categories/update/{CategoryId}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<List<CategoryVM>> Search(string s)
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryVM>>($"api/Categories/search/{s}");
        }
    }
}
