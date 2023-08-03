using AutoMapper;
using QuanLyCF.BLL.ViewModels.Bill;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.API.AutoMapperConfigurations
{
    public class BillMap : Profile
    {
        public BillMap()
        {
            CreateMap<Bill, BillVM>().ReverseMap();
        }
    }
}
