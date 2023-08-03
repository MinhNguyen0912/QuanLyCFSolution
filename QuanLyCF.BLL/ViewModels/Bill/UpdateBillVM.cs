using QuanLyCF.DAL.Enums;

namespace QuanLyCF.BLL.ViewModels.Bill
{
    public class UpdateBillVM
    {
        public Guid IdBill { get; set; }
        public BillStatus Status { get; set; }
        public int Discount { get; set; }
        public Guid IdTable { get; set; }
        public string Note { get; set; }
    }
}
