using Newtonsoft.Json;
using QLCF.Repositories.Interfaces;
using QuanLyCF.BLL.ViewModels.Bill;
using QuanLyCF.DAL.Entities;
using System.Net.Http.Json;

namespace QLCF.Repositories.Services
{
    public class BillRepos : IBillRepos
    {
        private readonly HttpClient _httpClient;
        public BillRepos(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Guid> Add(AddBillVM request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Bills", request);
            var content = await result.Content.ReadAsStringAsync();
            var billId = JsonConvert.DeserializeObject<Guid>(content);

            return billId;
        }
        public async Task<bool> AddFood(Guid IdBill, Guid IdFood, int count)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Bills/addfood/{IdBill}/{IdFood}", count);
            return result.IsSuccessStatusCode;
        }
        public async Task<List<BillVM>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<BillVM>>($"api/BillInfos/all");
        }

        public async Task<List<BillVM>> GetAllDone()
        {
            return await _httpClient.GetFromJsonAsync<List<BillVM>>($"api/Bills/alldone");
        }

        public async Task<List<BillVM>> GetAllDoneInRangeTime(DateTime dateStart, DateTime dateEnd)
        {
            return await _httpClient.GetFromJsonAsync<List<BillVM>>($"api/Bills/getalldoneinrangetime/{dateStart}/{dateEnd}");
        }

        public async Task<List<BillVM>> GetAllInRangeTime(string dateStart, string dateEnd)
        {
            return await _httpClient.GetFromJsonAsync<List<BillVM>>($"api/Bills/getallinrangetime/{dateStart}/{dateEnd}");
        }

        public async Task<List<BillVM>> GetAllNotDone()
        {
            return await _httpClient.GetFromJsonAsync<List<BillVM>>($"api/Bills/allnotdone");
        }

        public async Task<List<BillVM>> GetAllNotDoneInRangeTime(DateTime dateStart, DateTime dateEnd)
        {
            return await _httpClient.GetFromJsonAsync<List<BillVM>>($"api/Bills/getallnotdoneinrangetime/{dateStart}/{dateEnd}");
        }

        public async Task<BillVM> GetById(Guid IdBill)
        {
            return await _httpClient.GetFromJsonAsync<BillVM>($"api/Bills/getbyid/{IdBill}");
        }
        public async Task<BillVM> GetByTableId(Guid IdTable)
        {
            return await _httpClient.GetFromJsonAsync<BillVM>($"api/Bills/getbytableid/{IdTable}");
        }

        public async Task<double> GetTotalPrice(Guid IdBill)
        {
            return await _httpClient.GetFromJsonAsync<double>($"api/Bills/totalprice/{IdBill}");
        }

        public async Task<bool> Pay(Guid IdBill, Guid SolveBy, int discount)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Bills/pay/{IdBill}/{SolveBy}", discount);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Update(Guid IdBill, UpdateBillVM request)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Bills/update/{IdBill}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateNote(Guid IdBill, string note)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Bills/updatenote/{IdBill}", note);
            return result.IsSuccessStatusCode;
        }
    }
}
