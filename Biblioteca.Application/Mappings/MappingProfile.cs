using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookDto, Book>().ReverseMap();

        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.BookIds,
                       opt => opt.MapFrom(src => src.Books.Select(b => b.Id)));

        CreateMap<AuthorDto, Author>()
            .ForMember(dest => dest.Books, opt => opt.Ignore()); 
    }
}
