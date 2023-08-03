using AutoMapper;
using QuanLyCF.BLL.ViewModels.Table;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.API.AutoMapperConfigurations
{
    public class TableMap : Profile
    {
        public TableMap()
        {
            CreateMap<Table, TableVM>().ReverseMap();

        }
    }
}
