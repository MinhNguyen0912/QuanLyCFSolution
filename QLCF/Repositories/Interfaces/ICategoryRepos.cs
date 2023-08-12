using QuanLyCF.BLL.ViewModels.Category;
using QuanLyCF.BLL.ViewModels.Table;

namespace QLCF.Repositories.Interfaces
{
    public interface ICategoryRepos
    {
        public Task<List<CategoryVM>> GetAll();
        public Task<bool> Add(string CategoryName);
        public Task<bool> Update(Guid CategoryId, CategoryVM request);
        public Task<bool> Delete(Guid CategoryId);
        public Task<CategoryVM> GetById(Guid categoryId);
        public Task<List<CategoryVM>> Search(string s);


    }
}
