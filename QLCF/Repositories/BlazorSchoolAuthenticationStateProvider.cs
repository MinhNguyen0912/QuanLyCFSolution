using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QLCF.Repositories.Interfaces;
using QuanLyCF.BLL.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace QLCF.Repositories
{
    public class BlazorSchoolAuthenticationStateProvider: AuthenticationStateProvider, IDisposable
    {
        private readonly HttpClient _httpClient;
        //private readonly AuthenticationDataMemoryStorage _authenticationDataMemoryStorage;
        private readonly ISessionStorageService _session;
        public string Username { get; set; } = "";


        public BlazorSchoolAuthenticationStateProvider(HttpClient httpClient, ISessionStorageService session)
        {
            _httpClient = httpClient;
            _session = session;
            AuthenticationStateChanged += OnAuthenticationStateChanged;
        }

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if (authenticationState is not null)
            {
                Username = authenticationState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";
            }
            
        }

        public void Dispose()
        {
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            var token = await _session.GetItemAsStringAsync("authToken");

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                identity = new ClaimsIdentity(jwtSecurityToken.Claims, "Blazor School");
            }

            var principal = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(principal);
            var authenticationTask = await Task.FromResult(authenticationState);

            return authenticationTask;
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

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            return loginResponse;
        }

        public async Task Logout()
        {
            await _session.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        }
    }
}
