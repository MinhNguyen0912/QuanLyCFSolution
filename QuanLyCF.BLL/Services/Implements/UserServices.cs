using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels;
using QuanLyCF.DAL.DBContext;
using QuanLyCF.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace QuanLyCF.BLL.Services.Implements
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly QuanLyCafeDBContext _context;
        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _roleManager = roleManager;
            this._context = new QuanLyCafeDBContext();

        }
        public UserServices()
        {

        }

        public async Task<List<User>> GetAll()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<List<Role>> GetAllRole()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<bool> Update(string userName, string pw)
        {
            var user = await _userManager.FindByNameAsync(userName);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, pw);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
            //var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var passwordValidator = new PasswordValidator<User>();

            //var resultValidatePw = await passwordValidator.ValidateAsync(_userManager, user, pw);
            //if (resultValidatePw.Succeeded)
            //{
            //    var result = await _userManager.ResetPasswordAsync(user, resetToken,pw);
            //    return result.Succeeded;
            //}
            //return false;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }
        public async Task<LoginResponse> Authenticate(AuthenticateRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
            if (!result.Succeeded)
            {
                return null;
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim("Name",value:user.UserName),
                new Claim("Role",string.Join(";",roles)),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);


            return new LoginResponse()
            {
                Successfull = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

        }
        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new User()
            {
                Id = new Guid(),
                UserName = request.UserName,
                Email = request.Email,
                RoleId = request.RoleId
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var userRole = new IdentityUserRole<Guid>() { RoleId =  request.RoleId ,UserId = user.Id};
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();

            }
            return result.Succeeded;
        }
        public async Task<IDictionary<string, object>> ValidateToken(string token)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _config["Jwt:Audience"].ToLower();
            validationParameters.ValidIssuer = _config["Jwt:Issuer"].ToLower();
            validationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var principal = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, validationParameters);
            return principal.Claims;


            //return principal;

        }
        public string GetRole(string token)
        {

            string secret = _config["Jwt:Key"];
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return claims.Claims.First(p=>p.Type==ClaimTypes.Role).Value;
        }
        public async Task<List<User>> Search(string s)
        {
            var users = await _userManager.Users.ToListAsync();
            var result = users.Where(p=>p.UserName.Contains(s)).ToList();
            return result;
        }
    }
}
