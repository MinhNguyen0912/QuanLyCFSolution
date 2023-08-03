using Microsoft.AspNetCore.Mvc;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.Bill;

namespace QuanLyCF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillServices _services;
        public BillsController(IBillServices services)
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
        [Route("allnotdone")]
        public async Task<IActionResult> GetAllNotDone()
        {
            var list = await _services.GetAllNotDone();
            return Ok(list);
        }
        [HttpGet]
        [Route("alldone")]
        public async Task<IActionResult> GetAllDone()
        {
            var list = await _services.GetAllDone();
            return Ok(list);
        }
        [HttpGet]
        [Route("totalprice/{billId}")]
        public async Task<IActionResult> GetTotalPrice([FromRoute] Guid billId)
        {
            var price = await _services.GetTotalPrice(billId);
            return Ok(price);
        }
        [HttpGet]
        [Route("getbyid/{billId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid billId)
        {
            var obj = await _services.GetById(billId);
            return Ok(obj);
        }
        [HttpGet]
        [Route("getbytableid/{tableId}")]
        public async Task<IActionResult> GetByTableId([FromRoute] Guid tableId)
        {
            var obj = await _services.GetByTableId(tableId);
            return Ok(obj);
        }
        [HttpGet]
        [Route("getallinrangetime/{dateStart}/{dateEnd}")]
        public async Task<IActionResult> GetAllInRangeTime([FromRoute] string dateStart, [FromRoute] string dateEnd)
        {
            var list = await _services.GetAllInRangeTime(dateStart, dateEnd);
            return Ok(list);
        }
        [HttpGet]
        [Route("getalldoneinrangetime/{dateStart}/{dateEnd}")]
        public async Task<IActionResult> GetAllDoneInRangeTime([FromRoute] DateTime dateStart, [FromRoute] DateTime dateEnd)
        {
            var list = await _services.GetAllDoneInRangeTime(dateStart, dateEnd);
            return Ok(list);
        }
        [HttpGet]
        [Route("getallnotdoneinrangetime/{dateStart}/{dateEnd}")]
        public async Task<IActionResult> GetAllNotDoneInRangeTime([FromRoute] DateTime dateStart, [FromRoute] DateTime dateEnd)
        {
            var list = await _services.GetAllNotDoneInRangeTime(dateStart, dateEnd);
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddBillVM request)
        {
            var result = await _services.Add(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("update/{idBill}")]
        public async Task<IActionResult> Update([FromRoute] Guid idBill, [FromBody] UpdateBillVM request)
        {
            var result = await _services.Update(idBill, request);
            return Ok(result);
        }
        [HttpPut]
        [Route("addfood/{idBill}/{idFood}")]
        public async Task<IActionResult> AddFood([FromRoute] Guid idBill,[FromRoute] Guid idFood,[FromBody] int count)
        {
            var result = await _services.AddFood(idBill, idFood,count);
            return Ok(result);
        }
        [HttpPut]
        [Route("updatenote/{idBill}")]
        public async Task<IActionResult> Update([FromRoute] Guid idBill,[FromBody] string note)
        {
            var result = await _services.UpdateNote(idBill, note);
            return Ok(result);
        }
        [HttpPut]
        [Route("pay/{idBill}/{solveBy}")]
        public async Task<IActionResult> Pay([FromRoute] Guid idBill, [FromRoute] Guid solveBy, [FromBody] int discount)
        {
            var result = await _services.Pay(idBill, solveBy, discount);
            return Ok(result);
        }
    }
}
