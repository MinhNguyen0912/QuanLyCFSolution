using QuanLyCF.BLL.ViewModels.Table;

namespace QuanLyCF.BLL.Services.Interfaces
{
    public interface ITableServices
    {
        public Task<List<TableVM>> GetAll();
        public Task<TableVM> GetById(Guid TableId);
        public Task<bool> Add();
        public Task<bool> Update(Guid TableId, TableVM request);
        public Task<bool> Delete(Guid TableId);
        public Task<List<TableVM>> Search(string s);

    }
}
