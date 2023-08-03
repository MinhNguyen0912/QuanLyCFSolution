using Microsoft.AspNetCore.Mvc;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.Table;

namespace QuanLyCF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableServices _services;
        public TablesController(ITableServices services)
        {
            _services = services;
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _services.GetAll();
            return Ok(list);
        }
        [HttpGet]
        [Route("byId/{tableId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid tableId)
        {
            var obj = await _services.GetById(tableId);
            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TableVM request)
        {
            var result = await _services.Add(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("update/{tableId}")]
        public async Task<IActionResult> Update([FromRoute] Guid tableId, [FromBody] TableVM request)
        {
            var result = await _services.Update(tableId, request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{tableId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid tableId)
        {
            var result = await _services.Delete(tableId);
            return Ok(result);
        }
    }
}
