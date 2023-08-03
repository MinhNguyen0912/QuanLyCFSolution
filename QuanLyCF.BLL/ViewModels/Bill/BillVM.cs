using QuanLyCF.DAL.Enums;

namespace QuanLyCF.BLL.ViewModels.Bill
{
    public class BillVM
    {
        public Guid IdBill { get; set; }
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }
        public BillStatus Status { get; set; }
        public int Discount { get; set; }
        public Guid IdTable { get; set; }
        public string Note { get; set; }
    }
}
