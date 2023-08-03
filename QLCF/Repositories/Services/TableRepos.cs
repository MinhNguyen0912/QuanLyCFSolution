using QLCF.Repositories.Interfaces;
using QuanLyCF.BLL.ViewModels.Table;
using System.Net.Http.Json;

namespace QLCF.Repositories.Services
{
    public class TableRepos : ITableRepos
    {
        private readonly HttpClient _httpClient;
        public TableRepos(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> Add(TableVM request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Tables", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid TableId)
        {
            var result = await _httpClient.DeleteAsync($"api/Tables/delete/{TableId}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<TableVM>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<TableVM>>($"api/Tables/all");
        }

        public async Task<TableVM> GetById(Guid TableId)
        {
            return await _httpClient.GetFromJsonAsync<TableVM>($"api/Tables/byId/{TableId}");
        }

        public async Task<bool> Update(Guid TableId, TableVM request)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Tables/update/{TableId}", request);
            return result.IsSuccessStatusCode;
        }
    }
}
