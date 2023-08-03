using QLCF.Repositories.Interfaces;
using QuanLyCF.BLL.ViewModels.Food;
using System.Net.Http.Json;

namespace QLCF.Repositories.Services
{
    public class FoodRepos : IFoodRepos
    {
        private readonly HttpClient _httpClient;
        public FoodRepos(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> Add(FoodVM request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Foods", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid FoodId)
        {
            var result = await _httpClient.DeleteAsync($"api/Foods/delete/{FoodId}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<FoodVM>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<FoodVM>>("api/Foods/all");
        }
        public async Task<List<FoodVM>> Search(string s)
        {
            return await _httpClient.GetFromJsonAsync<List<FoodVM>>($"api/Foods/search/{s}");
        }

        public async Task<List<FoodVM>> GetByCategoryId(Guid CategoryId)
        {
            return await _httpClient.GetFromJsonAsync<List<FoodVM>>($"api/Foods/byCategoryId/{CategoryId}");
        }

        public async Task<FoodVM> GetById(Guid FoodId)
        {
            return await _httpClient.GetFromJsonAsync<FoodVM>($"api/Foods/byId/{FoodId}");
        }

        public async Task<bool> Update(Guid FoodId, FoodVM request)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Foods/update/{FoodId}", request);
            return result.IsSuccessStatusCode;
        }
    }
}
