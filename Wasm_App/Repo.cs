using QuanLyCF.BLL.ViewModels;
using QuanLyCF.BLL.ViewModels.Food;
using System.Net.Http;
using System.Net.Http.Json;

namespace Wasm_App
{
    public class Repo
    {
        private readonly HttpClient _httpClient;

        public Repo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task GetItem()
        {
            var request = new AuthenticateRequest()
            {
                UserName = "admin",
                Password="Abc@123"
            };
            //var result = await _httpClient.PostAsJsonAsync("https://localhost:7085/api/Accounts/authenticate", request);
            //var content = await result.Content.ReadAsStringAsync();
            var result = await _httpClient.GetFromJsonAsync<List<FoodVM>>("api/Foods/all"); 
            if (result == null)
            {
                return;
            }
        }
    }
}
