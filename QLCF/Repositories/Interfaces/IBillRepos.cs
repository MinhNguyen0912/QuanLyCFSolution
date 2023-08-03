
using QuanLyCF.BLL.ViewModels.Bill;

namespace QLCF.Repositories.Interfaces
{
    public interface IBillRepos
    {
        public Task<List<BillVM>> GetAll();
        public Task<List<BillVM>> GetAllNotDone();
        public Task<List<BillVM>> GetAllDone();
        public Task<double> GetTotalPrice(Guid IdBill);
        public Task<BillVM> GetById(Guid IdBill);
        public Task<BillVM> GetByTableId(Guid IdTable);
        public Task<List<BillVM>> GetAllInRangeTime(string dateStart, string dateEnd);
        public Task<List<BillVM>> GetAllNotDoneInRangeTime(DateTime dateStart, DateTime dateEnd);
        public Task<List<BillVM>> GetAllDoneInRangeTime(DateTime dateStart, DateTime dateEnd);
        public Task<Guid> Add(AddBillVM request);
        public Task<bool> AddFood(Guid IdBill, Guid IdFood, int count);
        public Task<bool> Update(Guid IdBill, UpdateBillVM request);
        public Task<bool> UpdateNote(Guid IdBill, string note);
        public Task<bool> Pay(Guid IdBill, Guid SolveBy, int discount);
    }
}
