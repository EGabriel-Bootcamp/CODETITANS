using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Mauser
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>();
          


        }


    }
}
