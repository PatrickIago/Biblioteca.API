using Biblioteca.Application.DTO;
namespace Biblioteca.Application.Contract;
public interface IBookService
{
    Task<List<BookDto>> Get();
    Task<BookDto> Get(int id);
    Task<bool> Update(BookDto book);
    Task<BookDto> Create(BookDto book);
    Task<bool?> Delete(int id);
}
