using QuanLyCF.BLL.ViewModels.Category;

namespace QuanLyCF.BLL.Services.Interfaces
{
    public interface ICategoryServices
    {
        public Task<List<CategoryVM>> GetAll();
        public Task<bool> Add(string CategoryName);
        public Task<bool> Update(Guid CategoryId, CategoryVM request);
        public Task<bool> Delete(Guid CategoryId);
        public Task<CategoryVM> GetById(Guid categoryId);

    }
}
