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
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
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
        public async Task<IActionResult> ValidateToken([FromBody] string token)
        {
            var result = await _userServices.ValidateToken(token);
            return Ok(result);
        }

        [HttpPost]
        [Route("getrole")]
        public IActionResult GetRole([FromBody] string token)
        {
            var result = _userServices.GetRole(token);
            return Ok(result);
        }
        [HttpPost]
        [Route("getuserbyusername")]
        public async Task<IActionResult> GetUserByUserName([FromBody] string userName)
        {
            var result = await _userServices.GetUserByUserName(userName);
            return Ok(result);
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userServices.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("allrole")]
        public async Task<IActionResult> GetAllRole()
        {
            var result = await _userServices.GetAllRole();
            return Ok(result);
        }
        [HttpPut]
        [Route("update/{userName}")]
        public async Task<IActionResult> Update([FromRoute] string userName, [FromBody] string pw)
        {
            var result = await _userServices.Update(userName, pw);
            return Ok(result);
        }
        [HttpGet]
        [Route("search/{s}")]
        public async Task<IActionResult> Search([FromRoute] string s)
        {
            var list = await _userServices.Search(s);
            return Ok(list);

        }
    }
}
