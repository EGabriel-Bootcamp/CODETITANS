using AutoMapper;
using CA_Domain;

namespace CA_Presentation
{
	public class MappingConfig : Profile
	{
        public MappingConfig()
        {
			CreateMap<User, UserDTO>().ReverseMap();
			CreateMap<UserDTO, UserCreateDTO>().ReverseMap();
			CreateMap<UserCreateDTO, User>().ReverseMap();
			
			var map = CreateMap<UserUpdateDTO, User>().ReverseMap();
			//map.ForAllMembers(x => x.Ignore());
			//map.ForMember(dest => dest.FirstName, opt => opt.MapFrom(dest => dest.);
			//map.ForMember(dest => dest.LastName, dest => dest.Ignore());
			//map.ForMember(dest => dest.Gender, dest => dest.Ignore());
			//map.ForMember(dest => dest.DateOfBirth, dest => dest.Ignore());
			//map.ForMember(dest => dest.DateOfBirth, dest => dest.Ignore());


			//map.ForMember(dest => dest.MarritalStatus, opt => opt.MapFrom(src => src.MarritalStatus));
			//map.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));

			CreateMap<User, UserUpdateDTO>();

			CreateMap<UserDTO, UserUpdateDTO>().ReverseMap();
		}
	}
}
