using QuanLyCF.DAL.Enums;

namespace QuanLyCF.DAL.Entities
{
    public class Bill
    {
        public Guid IdBill { get; set; }
        public DateTime DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public BillStatus Status { get; set; }
        public int Discount { get; set; }
        public Guid IdTable { get; set; }
        public Table Table { get; set; }
        public string Note { get; set; }
        public Guid CreateBy { get; set; }
        public Guid? SolveBy { get; set; }
        public List<BillInfo> BillInfos { get; set; }
    }
}
