using QuanLyCF.BLL.ViewModels.Table;

namespace QLCF.Repositories.Interfaces
{
    public interface ITableRepos
    {
        public Task<List<TableVM>> GetAll();
        public Task<TableVM> GetById(Guid TableId);
        public Task<bool> Add(TableVM request);
        public Task<bool> Update(Guid TableId, TableVM request);
        public Task<bool> Delete(Guid TableId);
    }
}
