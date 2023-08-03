using QuanLyCF.DAL.Enums;

namespace QuanLyCF.DAL.Entities
{
    public class Table
    {
        public Guid IdTable { get; set; }
        public string Name { get; set; }
        public TableStatus Status { get; set; }
        public List<Bill> Bills { get; set; }
    }
}
