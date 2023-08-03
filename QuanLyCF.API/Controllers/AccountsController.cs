using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels;

namespace QuanLyCF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public AccountsController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest request)
        {
            var result = await _userServices.Authenticate(request);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _userServices.Register(request);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost]
        [Route("validatetoken")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateToken([FromBody] string token)
        {
            var result = await _userServices.ValidateToken(token);
            return Ok(result);
        }

        [HttpPost]
        [Route("getrole")]
        [AllowAnonymous]
        public IActionResult GetRole([FromBody] string token)
        {
            var result = _userServices.GetRole(token);
            return Ok(result);
        }
        [HttpPost]
        [Route("getuserbyusername")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByUserName([FromBody] string userName)
        {
            var result = await _userServices.GetUserByUserName(userName);
            return Ok(result);
        }
    }
}
