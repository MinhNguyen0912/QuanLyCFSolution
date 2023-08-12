using Microsoft.AspNetCore.Mvc;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.Category;

namespace QuanLyCF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _services;
        public CategoriesController(ICategoryServices services)
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
        [Route("getbyid/{categoryId}")]
        public async Task<IActionResult> GetById([FromRoute]Guid categoryId)
        {
            var list = await _services.GetById(categoryId);
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
        public async Task<IActionResult> Add([FromBody] string categoryName)
        {
            var result = await _services.Add(categoryName);
            return Ok(result);
        }
        [HttpPut]
        [Route("update/{categoryId}")]
        public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] CategoryVM request)
        {
            var result = await _services.Update(categoryId, request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid categoryId)
        {
            var result = await _services.Delete(categoryId);
            return Ok(result);
        }
    }
}
