using AutoMapper;
using QuanLyCF.BLL.ViewModels.Category;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.API.AutoMapperConfigurations
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Category, CategoryVM>().ReverseMap();
        }
    }
}
