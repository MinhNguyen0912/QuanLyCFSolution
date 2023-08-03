using QuanLyCF.DAL.Enums;

namespace QuanLyCF.DAL.Entities
{
    public class Food
    {
        public Guid FoodId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public double Price { get; set; }
        public FoodStatus Status { get; set; }
        public Category Category { get; set; }
        public List<BillInfo> BillInfos { get; set; }
    }
}
