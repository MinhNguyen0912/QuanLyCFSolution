using Microsoft.AspNetCore.Mvc;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.Food;

namespace QuanLyCF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly IFoodServices _services;
        public FoodsController(IFoodServices services)
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
        [Route("byId/{foodId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid foodId)
        {
            var obj = await _services.GetById(foodId);
            return Ok(obj);
        }
        [HttpGet]
        [Route("byCategoryId/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId([FromRoute] Guid categoryId)
        {
            var list = await _services.GetByCategoryId(categoryId);
            return Ok(list);
        }
        [HttpGet]
        [Route("search/{s}")]
        public async Task<IActionResult> Search([FromRoute] string s)
        {
            var list = await _services.Search(s);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FoodVM request)
        {
            var result = await _services.Add(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("update/{foodId}")]
        public async Task<IActionResult> Update([FromRoute] Guid foodId, [FromBody] FoodVM request)
        {
            var result = await _services.Update(foodId, request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{foodId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid foodId)
        {
            var result = await _services.Delete(foodId);
            return Ok(result);
        }
    }
}
