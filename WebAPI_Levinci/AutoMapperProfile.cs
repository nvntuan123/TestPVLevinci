using AutoMapper;
using WebAPI_Levinci.Dtos;
using WebAPI_Levinci.Models;

namespace WebAPI_Levinci
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users?, GetUserDto>();
            CreateMap<AddUserDto, Users?>();
            CreateMap<UpdateUserDto, Users?>();
        }
    }
}
