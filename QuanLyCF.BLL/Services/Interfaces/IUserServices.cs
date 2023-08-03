using QuanLyCF.BLL.ViewModels;
using QuanLyCF.DAL.Entities;
using System.Security.Claims;

namespace QuanLyCF.BLL.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<LoginResponse> Authenticate(AuthenticateRequest request);
        public Task<bool> Register(RegisterRequest request);
        public Task<IDictionary<string, object>> ValidateToken(string token);
        public Task<User> GetUserByUserName(string userName);
        public string GetRole(string token);


    }
}
