namespace QuanLyCF.DAL.Entities
{
    public class BillInfo
    {
        public Guid BillInfoId { get; set; }
        public Guid BillId { get; set; }
        public Guid FoodId { get; set; }
        public int Count { get; set; }
        public Bill Bill { get; set; }
        public Food Food { get; set; }
    }
}
