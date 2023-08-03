using QuanLyCF.BLL.ViewModels.BillInfo;

namespace QuanLyCF.BLL.Services.Interfaces
{
    public interface IBillInfoServices
    {
        public Task<List<BillInfoVM>> GetAllByIdBill(Guid IdBill);
        public Task<bool> Add(BillInfoVM request);
        public Task<bool> UpdateCount(Guid IdBillInfo, int Count);
        public Task<bool> Delete(Guid IdBillInfo);

    }
}
