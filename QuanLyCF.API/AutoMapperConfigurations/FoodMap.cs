using AutoMapper;
using QuanLyCF.BLL.ViewModels.Food;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.API.AutoMapperConfigurations
{
    public class FoodMap : Profile
    {
        public FoodMap()
        {
            CreateMap<Food, FoodVM>().ReverseMap();
        }
    }
}
