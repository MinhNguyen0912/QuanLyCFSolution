using Blazored.SessionStorage;
using Newtonsoft.Json;
using QLCF.Repositories.Interfaces;
using QuanLyCF.BLL.ViewModels;
using QuanLyCF.BLL.ViewModels.Category;
using QuanLyCF.BLL.ViewModels.Food;
using QuanLyCF.DAL.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace QLCF.Repositories.Services
{
    public class UserRepos : IUserRepos
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _session;

        public UserRepos(HttpClient httpClient, ISessionStorageService session)
        {
            _httpClient = httpClient;
            _session = session;
        }
        public async Task<LoginResponse?> Authenticate(AuthenticateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Accounts/authenticate", request);
            var content = await result.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(content);
            if (!result.IsSuccessStatusCode)
            {
                return new LoginResponse() { Successfull = false, Error = "Sai ten dang nhap hoac mat khau" };
            }
            await _session.SetItemAsStringAsync("authToken", loginResponse.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);

            return loginResponse;
        }

        public async Task Logout()
        {
            await _session.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;

        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/accounts/register", request);
            if (result.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }
        public async Task<IDictionary<string, object>> ValidateToken(string token)
        {
            var result = await _httpClient.PostAsJsonAsync("api/accounts/validatetoken", token);
            var content = await result.Content.ReadAsStringAsync();
            var claims = JsonConvert.DeserializeObject<IDictionary<string, object>>(content);
            return claims;
        }
        public async Task<string> GetRole(string token)
        {
            var result = await _httpClient.PostAsJsonAsync("api/accounts/getrole", token);
            var content = await result.Content.ReadAsStringAsync();
            //var role = JsonConvert.DeserializeObject<string>(content);
            return content;
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            var result = await _httpClient.PostAsJsonAsync("api/accounts/getuserbyusername", userName);
            var content = await result.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
            return user;
        }
        public async Task<List<User>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/Accounts/all");
        }
        public async Task<List<Role>> GetAllRole()
        {
            return await _httpClient.GetFromJsonAsync<List<Role>>("api/Accounts/allrole");
        }
        public async Task<bool> Update(string userName, string pw)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Accounts/update/{userName}", pw);
            return result.IsSuccessStatusCode;
        }
        public async Task<List<User>> Search(string s)
        {
            return await _httpClient.GetFromJsonAsync<List<User>>($"api/Accounts/search/{s}");

        }
    }
}
