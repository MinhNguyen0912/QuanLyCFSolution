using AutoMapper;
using QuanLyCF.BLL.ViewModels.BillInfo;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.API.AutoMapperConfigurations
{
    public class BillInfoMap : Profile
    {
        public BillInfoMap()
        {
            CreateMap<BillInfo, BillInfoVM>()
                .ForMember(p => p.FoodName, opt => opt.MapFrom(p => p.Food.Name))
                .ForMember(p => p.Price, opt => opt.MapFrom(p => p.Food.Price))
                .ReverseMap();

        }
    }
}
