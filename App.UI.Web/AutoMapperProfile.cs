using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Domain.Entities;
using AutoMapper;

namespace App.UI.Web
{
    public class AutoMapperProfile : Profile
    {  
        public AutoMapperProfile() {




            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();
    
            CreateMap<User, ReadUserDto>();
            CreateMap<ReadUserDto, User>();
        }






    }
}
