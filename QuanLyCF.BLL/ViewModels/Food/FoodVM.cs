using QuanLyCF.DAL.Enums;

namespace QuanLyCF.BLL.ViewModels.Food
{
    public class FoodVM
    {
        public Guid FoodId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public double Price { get; set; }
        public FoodStatus Status { get; set; }
    }
}
