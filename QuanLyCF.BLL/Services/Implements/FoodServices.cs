using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.Category;
using QuanLyCF.BLL.ViewModels.Food;
using QuanLyCF.DAL.DBContext;
using QuanLyCF.DAL.Entities;
using QuanLyCF.DAL.Enums;

namespace QuanLyCF.BLL.Services.Implements
{
    public class FoodServices : IFoodServices
    {
        private readonly QuanLyCafeDBContext _context;
        private readonly IMapper _mapper;

        public FoodServices(IMapper mapper)
        {
            this._context = new QuanLyCafeDBContext();
            this._mapper = mapper;
        }

        public async Task<bool> Add(FoodVM request)
        {
            var food = new Food()
            {
                FoodId = new Guid(),
                Name = request.Name,
                CategoryId = request.CategoryId,
                Price = request.Price,
                Status = FoodStatus.Serve
            };
            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid FoodId)
        {
            var food = await _context.Foods.FindAsync(FoodId);
            food.Status = FoodStatus.NotServe;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FoodVM>> GetAll()
        {
            var list = await _context.Foods
                .Where(p => p.Status == FoodStatus.Serve)
                .ProjectTo<FoodVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }

        public async Task<List<FoodVM>> GetByCategoryId(Guid CategoryId)
        {
            var list = await _context.Foods
                .Where(p => p.Status == FoodStatus.Serve && p.CategoryId == CategoryId)
                .ProjectTo<FoodVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }

        public async Task<FoodVM> GetById(Guid FoodId)
        {
            var obj = await _context.Foods.FindAsync(FoodId);
            return _mapper.Map<FoodVM>(obj);
        }

        public async Task<bool> Update(Guid FoodId, FoodVM request)
        {
            var obj = await _context.Foods.FindAsync(FoodId);
            obj.Name = request.Name;
            obj.CategoryId = request.CategoryId;
            obj.Status = request.Status;
            obj.Price = request.Price;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<FoodVM>> Search(string s)
        {
            var list = await _context.Foods
                .Where(p => p.Status == FoodStatus.Serve && p.Name.Contains(s))
                .ProjectTo<FoodVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }
    }
}
