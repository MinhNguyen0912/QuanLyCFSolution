namespace QuanLyCF.BLL.ViewModels.BillInfo
{
    public class BillInfoVM
    {
        public Guid BillInfoId { get; set; }
        public Guid BillId { get; set; }
        public Guid FoodId { get; set; }
        public string FoodName { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
