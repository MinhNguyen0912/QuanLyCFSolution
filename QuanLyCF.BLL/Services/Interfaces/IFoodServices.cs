using QuanLyCF.BLL.ViewModels.Food;

namespace QuanLyCF.BLL.Services.Interfaces
{
    public interface IFoodServices
    {
        public Task<List<FoodVM>> GetAll();
        public Task<FoodVM> GetById(Guid FoodId);
        public Task<List<FoodVM>> GetByCategoryId(Guid CategoryId);
        public Task<bool> Add(FoodVM request);
        public Task<bool> Update(Guid FoodId, FoodVM request);
        public Task<bool> Delete(Guid FoodId);
        public Task<List<FoodVM>> Search(string s);

    }
}
