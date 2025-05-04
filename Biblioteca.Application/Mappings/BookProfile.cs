using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Domain.Entities;
namespace Biblioteca.Application.Mappings;
public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookDto, Book>().ReverseMap();
    }
}
