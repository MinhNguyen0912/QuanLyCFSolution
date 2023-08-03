using QuanLyCF.DAL.Enums;

namespace QuanLyCF.BLL.ViewModels.Table
{
    public class TableVM
    {
        public Guid IdTable { get; set; }
        public string Name { get; set; }
        public TableStatus Status { get; set; }
    }
}
