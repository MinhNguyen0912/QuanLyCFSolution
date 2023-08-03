namespace QuanLyCF.DAL.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public List<Food> Foods { get; set; }
        public int Status { get; set; }//1-Xoa
    }
}
