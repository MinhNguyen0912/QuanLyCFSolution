using Microsoft.AspNetCore.Mvc;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.BillInfo;

namespace QuanLyCF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillInfosController : ControllerBase
    {
        private readonly IBillInfoServices _services;
        public BillInfosController(IBillInfoServices services)
        {
            _services = services;
        }
        [HttpGet]
        [Route("all/{IdBill}")]
        public async Task<IActionResult> GetAllByIdBill([FromRoute] Guid IdBill)
        {
            var list = await _services.GetAllByIdBill(IdBill);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BillInfoVM request)
        {
            var result = await _services.Add(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("update/{IdBillInfo}")]
        public async Task<IActionResult> UpdateCount([FromRoute] Guid IdBillInfo, [FromBody] int Count)
        {
            var result = await _services.UpdateCount(IdBillInfo, Count);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{IdBillInfo}")]
        public async Task<IActionResult> Delete([FromRoute] Guid IdBillInfo)
        {
            var result = await _services.Delete(IdBillInfo);
            return Ok(result);
        }
    }
}
