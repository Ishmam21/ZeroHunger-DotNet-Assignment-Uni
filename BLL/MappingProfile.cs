using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;

namespace BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<CollectRequest,CollectRequestModel>().ReverseMap();
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}