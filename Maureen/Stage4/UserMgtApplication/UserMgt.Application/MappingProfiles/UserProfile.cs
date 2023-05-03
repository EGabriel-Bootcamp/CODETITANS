using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Features.Commands.CreateUser;
using UserMgt.Application.Features.Commands.EditUser;
using UserMgt.Application.Features.Queries.GetAllUsers;
using UserMgt.Domain;

namespace UserMgt.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, EditUserDto>().ReverseMap();
        }
    }
}
