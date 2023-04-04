using AutoMapper;
using Jolib.Dtos;
using Jolib.Entities;

namespace Jolib
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PublisherDto, Publisher>();
            CreateMap<AuthorDto, Author>();
            CreateMap<BookDto, Book>();


        }


    }
}
