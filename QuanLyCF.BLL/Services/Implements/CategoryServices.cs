using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.Category;
using QuanLyCF.DAL.DBContext;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.BLL.Services.Implements
{
    public class CategoryServices : ICategoryServices
    {
        private readonly QuanLyCafeDBContext _context;
        private readonly IMapper _mapper;

        public CategoryServices(IMapper mapper)
        {
            this._context = new QuanLyCafeDBContext();
            this._mapper = mapper;
        }
        public async Task<bool> Add(string CategoryName)
        {
            var obj = new Category()
            {
                CategoryId = new Guid(),
                Name = CategoryName,
                Status = 0
            };
            await _context.Categories.AddAsync(obj);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid CategoryId)
        {
            var obj = await _context.Categories.FindAsync(CategoryId);
            obj.Status = 1;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryVM>> GetAll()
        {
            var list = await _context.Categories
                .Where(p => p.Status == 0)
                .ProjectTo<CategoryVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }

        public async Task<CategoryVM> GetById(Guid categoryId)
        {
            var obj = await _context.Categories
                .FindAsync(categoryId);
            return _mapper.Map<CategoryVM>(obj);
        }

        public async Task<bool> Update(Guid CategoryId, CategoryVM request)
        {
            var obj = await _context.Categories.FindAsync(CategoryId);
            obj.Name = request.Name;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
