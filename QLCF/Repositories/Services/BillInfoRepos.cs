using QLCF.Repositories.Interfaces;
using QuanLyCF.BLL.ViewModels.BillInfo;
using System.Net.Http.Json;

namespace QLCF.Repositories.Services
{
    public class BillInfoRepos : IBillInfoRepos
    {
        private readonly HttpClient _httpClient;
        public BillInfoRepos(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> Add(BillInfoVM request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/BillInfos", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid IdBillInfo)
        {
            var result = await _httpClient.DeleteAsync($"api/BillInfos/delete/{IdBillInfo}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<BillInfoVM>> GetAllByIdBill(Guid IdBill)
        {
            return await _httpClient.GetFromJsonAsync<List<BillInfoVM>>($"api/BillInfos/all/{IdBill}");
        }

        public async Task<bool> UpdateCount(Guid IdBillInfo, int Count)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/BillInfos/update/{IdBillInfo}", Count);
            return result.IsSuccessStatusCode;
        }
    }
}
